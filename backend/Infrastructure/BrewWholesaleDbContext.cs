using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BrewWholesaleDbContext : DbContext
{
    public BrewWholesaleDbContext(DbContextOptions<BrewWholesaleDbContext> options) : base(options) { }
}