import { http } from './http';
import type { BeerDto } from '../types/lookup';

export function getBeers(name?: string) {
  const q = name ? `?name=${encodeURIComponent(name)}` : '';
  return http<BeerDto[]>(`/beers${q}`);
}

export function createBeer(payload: {
  breweryId: string;
  name: string;
  alcoholDegree: number;
  priceHtva: number;
}) {
  return http(`/beers`, {
    method: 'POST',
    body: JSON.stringify(payload),
  });
}
