import { http } from './http';

export type QuoteLine = { beerId: string; quantity: number };
export type QuoteRequest = { items: QuoteLine[] };

export type QuoteResponse = {
  wholesaler: string;
  totalQuantity: number;
  subtotal: number;
  discountRate: number;
  total: number;
  lines: {
    beerId: string;
    beerName: string;
    quantity: number;
    unitPrice: number;
    lineTotal: number;
  }[];
};

export function requestQuote(wholesalerId: string, payload: QuoteRequest) {
  return http<QuoteResponse>(`/wholesalers/${wholesalerId}/quote`, {
    method: 'POST',
    body: JSON.stringify(payload),
  });
}
