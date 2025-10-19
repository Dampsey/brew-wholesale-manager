namespace Api.Dto;

public sealed class AttachBeerRequest
{
	public Guid    BeerId    { get; set; }
	public int     Stock     { get; set; }
	public decimal SalePrice { get; set; }
}

public sealed class UpdateStockRequest
{
	public int Stock { get; set; }
}