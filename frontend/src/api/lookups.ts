import { http } from '../api/http';
import type { IdName } from '../types/lookup';

export const getBreweries = () => http<IdName[]>('/lookups/breweries');
export const getWholesalers = () => http<IdName[]>('/lookups/wholesalers');
