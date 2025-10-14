namespace Core.Entities;

public class Beer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal AlcoholDegree { get; set; }   // ex: 6.6
    public decimal PriceHtva { get; set; }       // ex: 2.20
    public Guid BreweryId { get; set; }
    public Brewery Brewery { get; set; } = null!;
    public ICollection<WholesaleBeer> WholesaleBeers { get; set; } = new List<WholesaleBeer>();
}