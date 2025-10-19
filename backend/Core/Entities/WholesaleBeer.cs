namespace Core.Entities;

public class WholesaleBeer
{
	public Guid       WholesalerId { get; set; }
	public Wholesaler Wholesaler   { get; set; } = null!;
	public Guid       BeerId       { get; set; }
	public Beer       Beer         { get; set; } = null!;
	public int        Stock        { get; set; }
	public decimal    SalePrice    { get; set; }
}