using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BrewWholesaleDbContext(DbContextOptions<BrewWholesaleDbContext> options) : DbContext(options)
{
    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<Brewery> Breweries => Set<Brewery>();
    public DbSet<Wholesaler> Wholesalers => Set<Wholesaler>();
    public DbSet<WholesaleBeer> WholesaleBeers => Set<WholesaleBeer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WholesaleBeer>()
            .HasKey(x => new { x.WholesalerId, x.BeerId });
    }
}