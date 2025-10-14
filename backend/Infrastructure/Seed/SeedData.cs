using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public static class SeedData
{
    public static async Task EnsureAsync(BrewWholesaleDbContext db)
    {
        if (await db.Breweries.AnyAsync()) return;

        var brewery = new Brewery { Id = Guid.NewGuid(), Name = "Abbaye de Leffe" };
        var beer = new Beer { Id = Guid.NewGuid(), Name = "Leffe Blonde", AlcoholDegree = 6.6m, PriceHtva = 2.20m, Brewery = brewery };
        var wholesaler = new Wholesaler { Id = Guid.NewGuid(), Name = "HygieDrinks" };
        var wb = new WholesaleBeer { Beer = beer, Wholesaler = wholesaler, Stock = 10, SalePrice = 2.20m };

        db.AddRange(brewery, beer, wholesaler, wb);
        await db.SaveChangesAsync();
    }
}