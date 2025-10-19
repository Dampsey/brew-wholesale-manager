namespace Api.Dto;

public sealed class CreateBeerRequest
{
	public Guid    BreweryId     { get; set; }
	public string? Name          { get; set; }
	public decimal AlcoholDegree { get; set; }
	public decimal PriceHtva     { get; set; }
}