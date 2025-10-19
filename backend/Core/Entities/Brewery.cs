namespace Core.Entities;

public class Brewery
{
	public Guid              Id    { get; set; }
	public string            Name  { get; set; } = null!;
	public ICollection<Beer> Beers { get; set; } = new List<Beer>();
}