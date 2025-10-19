import { http } from '../api/http';
import type { StockItem } from '../types/lookup';

export function attachBeer(
  wholesalerId: string,
  payload: { beerId: string; stock: number; salePrice: number },
) {
  return http(`/wholesalers/${wholesalerId}/beers`, {
    method: 'POST',
    body: JSON.stringify(payload),
  });
}

export function updateStock(wholesalerId: string, beerId: string, stock: number) {
  return http(`/wholesalers/${wholesalerId}/beers/${beerId}/stock`, {
    method: 'PATCH',
    body: JSON.stringify({ stock }),
  });
}

export function getStock(wholesalerId: string) {
  return http<StockItem[]>(`/wholesalers/${wholesalerId}/stock`);
}
