namespace Core.Entities;

public class Beer
{
	public Guid                       Id             { get; set; }
	public string                     Name           { get; set; } = null!;
	public decimal                    AlcoholDegree  { get; set; }
	public decimal                    PriceHtva      { get; set; }
	public Guid                       BreweryId      { get; set; }
	public Brewery                    Brewery        { get; set; } = null!;
	public ICollection<WholesaleBeer> WholesaleBeers { get; set; } = new List<WholesaleBeer>();
}