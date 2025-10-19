namespace Core.Entities;

public class Wholesaler
{
	public Guid                       Id             { get; set; }
	public string                     Name           { get; set; } = null!;
	public ICollection<WholesaleBeer> WholesaleBeers { get; set; } = new List<WholesaleBeer>();
}