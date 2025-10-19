using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController(BrewWholesaleDbContext db) : ControllerBase
{
	[HttpGet("breweries")]
	public async Task<IActionResult> Breweries(CancellationToken ct) =>
		Ok(await db.Breweries.Select(b => new { b.Id, b.Name }).OrderBy(b => b.Name).ToListAsync(ct));

	[HttpGet("wholesalers")]
	public async Task<IActionResult> Wholesalers(CancellationToken ct) =>
		Ok(await db.Wholesalers.Select(w => new { w.Id, w.Name }).OrderBy(w => w.Name).ToListAsync(ct));
}