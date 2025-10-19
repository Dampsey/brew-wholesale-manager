namespace Api.Dto;

public sealed record QuoteLine(Guid BeerId, int Quantity);

public sealed record QuoteRequest(List<QuoteLine> Items);

public sealed record QuoteLineResult(
	Guid BeerId,
	string BeerName,
	int Quantity,
	decimal UnitPrice,
	decimal LineTotal);

public sealed record QuoteResponse(
	string Wholesaler,
	int TotalQuantity,
	decimal Subtotal,
	decimal DiscountRate,
	decimal Total,
	List<QuoteLineResult> Lines);