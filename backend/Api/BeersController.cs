using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

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
        
        var result = beers.Select(b => new {
            b.Id,
            b.Name,
            b.AlcoholDegree,
            b.PriceHtva,
            Brewery = new { b.Brewery.Id, b.Brewery.Name },
            Wholesalers = b.WholesaleBeers.Select(wb => new {
                wb.WholesalerId,
                wb.Wholesaler.Name,
                wb.Stock,
                wb.SalePrice
            })
        });

        return Ok(result);
    }
}