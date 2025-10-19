using Api.Dto;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WholesalersController(BrewWholesaleDbContext db) : ControllerBase
{
	[HttpPost("{wholesalerId:guid}/beers")]
	public async Task<IActionResult> AttachBeer(Guid wholesalerId, [FromBody] AttachBeerRequest body,
		CancellationToken ct)
	{
		if (body.Stock < 0) return BadRequest("Stock invalide");
		if (body.SalePrice < 0) return BadRequest("Prix invalide");

		var ws = await db.Wholesalers.FindAsync([wholesalerId], ct);
		if (ws is null) return NotFound("Grossiste introuvable");

		var beer = await db.Beers.FindAsync([body.BeerId], ct);
		if (beer is null) return NotFound("Bière introuvable");

		var exists =
			await db.WholesaleBeers.AnyAsync(x => x.WholesalerId == wholesalerId && x.BeerId == body.BeerId, ct);
		if (exists) return Conflict("Cette bière est déjà associée à ce grossiste");

		db.WholesaleBeers.Add(new Core.Entities.WholesaleBeer
		{
			WholesalerId = wholesalerId,
			BeerId = body.BeerId,
			Stock = body.Stock,
			SalePrice = body.SalePrice
		});
		await db.SaveChangesAsync(ct);

		return CreatedAtAction(nameof(GetStock), new { wholesalerId },
			new { wholesalerId, body.BeerId, body.Stock, body.SalePrice });
	}

	[HttpPatch("{wholesalerId:guid}/beers/{beerId:guid}/stock")]
	public async Task<IActionResult> UpdateStock(Guid wholesalerId, Guid beerId, [FromBody] UpdateStockRequest body,
		CancellationToken ct)
	{
		if (body.Stock < 0) return BadRequest("Stock invalide");

		var wb = await db.WholesaleBeers.FirstOrDefaultAsync(x => x.WholesalerId == wholesalerId && x.BeerId == beerId,
			ct);
		if (wb is null) return NotFound("Association grossiste/bière introuvable");

		wb.Stock = body.Stock;
		await db.SaveChangesAsync(ct);

		return NoContent();
	}

	[HttpGet("{wholesalerId:guid}/stock")]
	public async Task<IActionResult> GetStock(Guid wholesalerId, CancellationToken ct)
	{
		var exists = await db.Wholesalers.AnyAsync(w => w.Id == wholesalerId, ct);
		if (!exists) return NotFound();

		var items = await db.WholesaleBeers
			.Include(x => x.Beer).ThenInclude(b => b.Brewery)
			.Where(x => x.WholesalerId == wholesalerId)
			.Select(x => new
			{
				x.BeerId,
				Beer = x.Beer.Name,
				Brewery = x.Beer.Brewery.Name,
				x.Stock,
				x.SalePrice
			})
			.OrderBy(x => x.Beer)
			.ToListAsync(ct);

		return Ok(items);
	}

	[HttpPost("{wholesalerId:guid}/quote")]
	public async Task<ActionResult<QuoteResponse>> Quote(Guid wholesalerId, [FromBody] QuoteRequest body,
		CancellationToken ct)
	{
		if (body?.Items is null || body.Items.Count == 0)
			return BadRequest("La commande ne peut pas être vide");

		var wholesaler = await db.Wholesalers.AsNoTracking().FirstOrDefaultAsync(w => w.Id == wholesalerId, ct);
		if (wholesaler is null) return NotFound("Le grossiste doit exister");

		var dup = body.Items.GroupBy(i => i.BeerId).FirstOrDefault(g => g.Count() > 1);
		if (dup is not null) return BadRequest("Pas de doublons dans la commande");

		var beerIds = body.Items.Select(i => i.BeerId).ToList();

		var offers = await db.WholesaleBeers
			.AsNoTracking()
			.Include(wb => wb.Beer)
			.Where(wb => wb.WholesalerId == wholesalerId && beerIds.Contains(wb.BeerId))
			.ToListAsync(ct);

		if (offers.Count != beerIds.Count)
			return BadRequest("La bière doit être vendue par le grossiste");

		foreach (var i in body.Items)
		{
			var of = offers.First(o => o.BeerId == i.BeerId);
			if (i.Quantity <= 0) return BadRequest("Quantité invalide");
			if (of.Stock < i.Quantity) return BadRequest($"Stock insuffisant pour {of.Beer.Name}");
		}

		var lines = body.Items
			.Join(offers, i => i.BeerId, o => o.BeerId,
				(i, o) => new QuoteLineResult(
					o.BeerId, o.Beer.Name, i.Quantity, o.SalePrice, o.SalePrice * i.Quantity))
			.ToList();

		var totalQty = lines.Sum(l => l.Quantity);
		var subtotal = lines.Sum(l => l.LineTotal);
		var discount = totalQty >= 20 ? 0.20m : totalQty >= 10 ? 0.10m : 0m;
		var total = decimal.Round(subtotal * (1 - discount), 2);

		return Ok(new QuoteResponse(wholesaler.Name, totalQty, subtotal, discount, total, lines));
	}
}