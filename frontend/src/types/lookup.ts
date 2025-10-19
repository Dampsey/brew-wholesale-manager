export type IdName = { id: string; name: string };

export type StockItem = {
  beerId: string;
  beer: string;
  brewery: string;
  stock: number;
  salePrice: number;
};

export type BeerDto = {
  id: string;
  name: string;
  alcoholDegree: number;
  priceHtva: number;
  brewery: IdName;
  wholesalers: { wholesalerId: string; name: string; stock: number; salePrice: number }[];
};
