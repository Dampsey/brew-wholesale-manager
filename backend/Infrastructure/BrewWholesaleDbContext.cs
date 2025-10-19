using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BrewWholesaleDbContext(DbContextOptions<BrewWholesaleDbContext> options) : DbContext(options)
{
	public DbSet<Beer>          Beers          => Set<Beer>();
	public DbSet<Brewery>       Breweries      => Set<Brewery>();
	public DbSet<Wholesaler>    Wholesalers    => Set<Wholesaler>();
	public DbSet<WholesaleBeer> WholesaleBeers => Set<WholesaleBeer>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<WholesaleBeer>()
			.HasKey(x => new { x.WholesalerId, x.BeerId });

		var brLeffe = Guid.Parse("11111111-1111-1111-1111-111111111111");
		var brChimay = Guid.Parse("22222222-2222-2222-2222-222222222222");
		var brOrval = Guid.Parse("33333333-3333-3333-3333-333333333333");
		var brWestmalle = Guid.Parse("44444444-4444-4444-4444-444444444444");
		var brDuvel = Guid.Parse("55555555-5555-5555-5555-555555555555");
		var brSenne = Guid.Parse("66666666-6666-6666-6666-666666666666");
		var brHuyghe = Guid.Parse("77777777-7777-7777-7777-777777777777");
		var brCantillon = Guid.Parse("88888888-8888-8888-8888-888888888888");

		var wA = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
		var wB = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
		var wC = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
		var wD = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

		var bLeffe = Guid.Parse("00000000-0000-0000-0000-000000000001");
		var bChimay = Guid.Parse("00000000-0000-0000-0000-000000000002");
		var bOrval = Guid.Parse("00000000-0000-0000-0000-000000000003");
		var bWest = Guid.Parse("00000000-0000-0000-0000-000000000004");
		var bDuvel = Guid.Parse("00000000-0000-0000-0000-000000000005");
		var bZinne = Guid.Parse("00000000-0000-0000-0000-000000000006");
		var bDelir = Guid.Parse("00000000-0000-0000-0000-000000000007");
		var bGueuze = Guid.Parse("00000000-0000-0000-0000-000000000008");
		var bSpec = Guid.Parse("00000000-0000-0000-0000-000000000009");
		var bFrites = Guid.Parse("00000000-0000-0000-0000-000000000010");
		var bMarol = Guid.Parse("00000000-0000-0000-0000-000000000011");

		modelBuilder.Entity<Brewery>().HasData(
			new Brewery { Id = brLeffe, Name = "Abbaye de Leffe" },
			new Brewery { Id = brChimay, Name = "Abbaye de Scourmont (Chimay)" },
			new Brewery { Id = brOrval, Name = "Abbaye d'Orval" },
			new Brewery { Id = brWestmalle, Name = "Abbaye de Westmalle" },
			new Brewery { Id = brDuvel, Name = "Duvel Moortgat" },
			new Brewery { Id = brSenne, Name = "Brasserie de la Senne" },
			new Brewery { Id = brHuyghe, Name = "Brasserie Huyghe (Delirium)" },
			new Brewery { Id = brCantillon, Name = "Brasserie Cantillon" }
		);

		modelBuilder.Entity<Wholesaler>().HasData(
			new Wholesaler { Id = wA, Name = "HygieDrinks" },
			new Wholesaler { Id = wB, Name = "BelgoDistrib" },
			new Wholesaler { Id = wC, Name = "BruxDrinks" },
			new Wholesaler { Id = wD, Name = "WallonBoissons" }
		);

		modelBuilder.Entity<Beer>().HasData(
			new Beer
			{
				Id = bLeffe, Name = "Leffe Blonde", AlcoholDegree = 6.6m, PriceHtva = 2.20m, BreweryId = brLeffe
			},
			new Beer
			{
				Id = bChimay, Name = "Chimay Bleue", AlcoholDegree = 9.0m, PriceHtva = 2.80m, BreweryId = brChimay
			},
			new Beer { Id = bOrval, Name = "Orval", AlcoholDegree = 6.2m, PriceHtva = 2.60m, BreweryId = brOrval },
			new Beer
			{
				Id = bWest, Name = "Westmalle Triple", AlcoholDegree = 9.5m, PriceHtva = 3.10m, BreweryId = brWestmalle
			},
			new Beer { Id = bDuvel, Name = "Duvel", AlcoholDegree = 8.5m, PriceHtva = 2.90m, BreweryId = brDuvel },
			new Beer { Id = bZinne, Name = "Zinnebir", AlcoholDegree = 6.0m, PriceHtva = 2.40m, BreweryId = brSenne },
			new Beer
			{
				Id = bDelir, Name = "Delirium Tremens", AlcoholDegree = 8.5m, PriceHtva = 3.20m, BreweryId = brHuyghe
			},
			new Beer
			{
				Id = bGueuze, Name = "Cantillon Gueuze", AlcoholDegree = 5.0m, PriceHtva = 4.50m,
				BreweryId = brCantillon
			},
			new Beer
			{
				Id = bSpec, Name = "Speculoos Stout", AlcoholDegree = 7.0m, PriceHtva = 2.70m, BreweryId = brSenne
			},
			new Beer
			{
				Id = bFrites, Name = "Frites IPA", AlcoholDegree = 5.8m, PriceHtva = 2.30m, BreweryId = brSenne
			},
			new Beer
			{
				Id = bMarol, Name = "La Blonde des Marolles", AlcoholDegree = 6.2m, PriceHtva = 2.10m,
				BreweryId = brSenne
			}
		);

		modelBuilder.Entity<WholesaleBeer>().HasData(
			new WholesaleBeer { WholesalerId = wA, BeerId = bLeffe, Stock = 50, SalePrice = 2.25m },
			new WholesaleBeer { WholesalerId = wB, BeerId = bLeffe, Stock = 120, SalePrice = 2.30m },
			new WholesaleBeer { WholesalerId = wC, BeerId = bLeffe, Stock = 0, SalePrice = 2.20m },
			new WholesaleBeer { WholesalerId = wB, BeerId = bChimay, Stock = 35, SalePrice = 2.95m },
			new WholesaleBeer { WholesalerId = wD, BeerId = bChimay, Stock = 12, SalePrice = 3.05m },
			new WholesaleBeer { WholesalerId = wA, BeerId = bOrval, Stock = 8, SalePrice = 2.80m },
			new WholesaleBeer { WholesalerId = wC, BeerId = bOrval, Stock = 22, SalePrice = 2.85m },
			new WholesaleBeer { WholesalerId = wD, BeerId = bWest, Stock = 18, SalePrice = 3.30m },
			new WholesaleBeer { WholesalerId = wA, BeerId = bWest, Stock = 2, SalePrice = 3.25m },
			new WholesaleBeer { WholesalerId = wA, BeerId = bDuvel, Stock = 200, SalePrice = 3.05m },
			new WholesaleBeer { WholesalerId = wC, BeerId = bDuvel, Stock = 15, SalePrice = 3.10m },
			new WholesaleBeer { WholesalerId = wB, BeerId = bZinne, Stock = 40, SalePrice = 2.55m },
			new WholesaleBeer { WholesalerId = wC, BeerId = bDelir, Stock = 25, SalePrice = 3.40m },
			new WholesaleBeer { WholesalerId = wD, BeerId = bDelir, Stock = 10, SalePrice = 3.50m },
			new WholesaleBeer { WholesalerId = wB, BeerId = bGueuze, Stock = 6, SalePrice = 4.90m },
			new WholesaleBeer { WholesalerId = wA, BeerId = bSpec, Stock = 11, SalePrice = 2.90m },
			new WholesaleBeer { WholesalerId = wD, BeerId = bFrites, Stock = 9, SalePrice = 2.45m },
			new WholesaleBeer { WholesalerId = wC, BeerId = bFrites, Stock = 21, SalePrice = 2.55m }
		);
	}
}