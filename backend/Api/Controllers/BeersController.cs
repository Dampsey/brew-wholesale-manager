using Api.Dto;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeersController(BrewWholesaleDbContext db) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] string? name)
	{
		var q = db.Beers
			.Include(b => b.Brewery)
			.Include(b => b.WholesaleBeers).ThenInclude(wb => wb.Wholesaler)
			.AsQueryable();

		if (!string.IsNullOrWhiteSpace(name))
		{
			var n = name.Trim().ToLowerInvariant();
			q = q.Where(b => b.Name.ToLower().Contains(n));
		}

		var beers = await q.ToListAsync();

		var result = beers.Select(b => new
		{
			b.Id,
			b.Name,
			b.AlcoholDegree,
			b.PriceHtva,
			Brewery = new { b.Brewery.Id, b.Brewery.Name },
			Wholesalers = b.WholesaleBeers.Select(wb => new
			{
				wb.WholesalerId,
				wb.Wholesaler.Name,
				wb.Stock,
				wb.SalePrice
			})
		});

		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
	{
		var b = await db.Beers.AsNoTracking()
			.Include(x => x.Brewery)
			.Include(x => x.WholesaleBeers).ThenInclude(wb => wb.Wholesaler)
			.FirstOrDefaultAsync(x => x.Id == id, ct);

		if (b is null) return NotFound();

		return Ok(new
		{
			b.Id, b.Name, b.AlcoholDegree, b.PriceHtva,
			Brewery = new { b.Brewery.Id, b.Brewery.Name },
			Wholesalers = b.WholesaleBeers.Select(wb => new
			{
				wb.WholesalerId, wb.Wholesaler.Name, wb.Stock, wb.SalePrice
			})
		});
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateBeerRequest body, CancellationToken ct)
	{
		if (body is null) return BadRequest("Champs requis");
		if (string.IsNullOrWhiteSpace(body.Name)) return BadRequest("Nom requis");
		if (body.AlcoholDegree < 0) return BadRequest("Degré invalide");
		if (body.PriceHtva < 0) return BadRequest("Prix invalide");

		var brewery = await db.Breweries.FindAsync([body.BreweryId], ct);
		if (brewery is null) return NotFound("Brasserie introuvable");

		var name = body.Name.Trim();
		var existsSameName = await db.Beers
			.AnyAsync(b => b.BreweryId == body.BreweryId && b.Name.ToLower() == name.ToLower(), ct);
		if (existsSameName) return Conflict("Une bière avec ce nom existe déjà pour cette brasserie");

		var beer = new Core.Entities.Beer
		{
			Id = Guid.NewGuid(),
			Name = name,
			AlcoholDegree = body.AlcoholDegree,
			PriceHtva = body.PriceHtva,
			BreweryId = body.BreweryId
		};

		db.Beers.Add(beer);
		await db.SaveChangesAsync(ct);

		return CreatedAtAction(nameof(GetById), new { id = beer.Id }, new
		{
			beer.Id, beer.Name, beer.AlcoholDegree, beer.PriceHtva,
			Brewery = new { brewery.Id, brewery.Name },
			Wholesalers = Array.Empty<object>()
		});
	}
}