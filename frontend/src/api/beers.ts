import { http } from "./http";
import type { BeerDto } from "../types/beer";

export function getBeers(params?: { name?: string }) {
  const query = params?.name ? `?name=${encodeURIComponent(params.name)}` : "";
  return http<BeerDto[]>(`/beers${query}`);
}
