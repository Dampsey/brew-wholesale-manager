export type BreweryDto = { id: string; name: string };
export type WholesalerLineDto = {
  wholesalerId: string;
  name: string;
  stock: number;
  salePrice: number;
};
export type BeerDto = {
  id: string;
  name: string;
  alcoholDegree: number;
  priceHtva: number;
  brewery: BreweryDto;
  wholesalers: WholesalerLineDto[];
};
