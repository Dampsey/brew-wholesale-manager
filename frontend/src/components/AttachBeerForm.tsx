import { useEffect, useMemo, useState } from 'react';
import { getBreweries, getWholesalers } from '../api/lookups';
import { getBeers } from '../api/beers';
import { getStock, attachBeer } from '../api/wholesalers';
import type { IdName, BeerDto } from '../types/lookup';

type StockItem = {
  beerId: string;
  beer: string;
  brewery: string;
  stock: number;
  salePrice: number;
};

export default function AttachBeerForm() {
  const [wholesalers, setWholesalers] = useState<IdName[]>([]);
  const [breweries, setBreweries] = useState<IdName[]>([]);
  const [beers, setBeers] = useState<BeerDto[]>([]);

  const [wholesalerId, setWholesalerId] = useState('');
  const [breweryId, setBreweryId] = useState('');
  const [beerId, setBeerId] = useState('');
  const [stock, setStock] = useState<number>(10);
  const [price, setPrice] = useState<number>(2.5);
  const [msg, setMsg] = useState<string | null>(null);

  const [existing, setExisting] = useState<Set<string>>(new Set());

  useEffect(() => {
    getWholesalers().then(setWholesalers);
    getBreweries().then(setBreweries);
    getBeers().then(setBeers);
  }, []);

  useEffect(() => {
    setBeerId('');
    setBreweryId('');
    setExisting(new Set());
    if (!wholesalerId) return;
    getStock(wholesalerId)
      .then((rows: StockItem[]) => setExisting(new Set(rows.map((r) => r.beerId))))
      .catch(() => setExisting(new Set()));
  }, [wholesalerId]);

  const availableBeersAll = useMemo(
    () => beers.filter((b) => !existing.has(b.id)),
    [beers, existing],
  );

  const breweryOptions = useMemo(() => {
    const allowed = new Set(availableBeersAll.map((b) => b.brewery.id));
    return breweries.filter((br) => allowed.has(br.id));
  }, [breweries, availableBeersAll]);

  useEffect(() => {
    if (breweryId && !breweryOptions.some((br) => br.id === breweryId)) {
      setBreweryId('');
    }
  }, [breweryOptions, breweryId]);

  const availableBeers = useMemo(
    () => availableBeersAll.filter((b) => !breweryId || b.brewery.id === breweryId),
    [availableBeersAll, breweryId],
  );

  useEffect(() => {
    if (beerId && !availableBeers.some((b) => b.id === beerId)) {
      setBeerId('');
    }
  }, [availableBeers, beerId]);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setMsg(null);
    try {
      if (!wholesalerId || !beerId) throw new Error();
      await attachBeer(wholesalerId, { beerId, stock, salePrice: price });
      setMsg('Association créée');
      setExisting((prev) => new Set([...prev, beerId]));
      setBeerId('');
    } catch {
      setMsg('Erreur');
    }
  }

  return (
    <form onSubmit={submit} className="space-y-3 rounded-xl border p-4">
      <h2 className="text-lg font-semibold">Associer une bière à un grossiste</h2>

      <div className="grid gap-3 md:grid-cols-3">
        <select
          className="rounded border px-3 py-2"
          value={wholesalerId}
          onChange={(e) => setWholesalerId(e.target.value)}
        >
          <option value="">Choisissez un grossiste</option>
          {wholesalers.map((w) => (
            <option key={w.id} value={w.id}>
              {w.name}
            </option>
          ))}
        </select>

        <select
          className="rounded border px-3 py-2"
          value={breweryId}
          onChange={(e) => setBreweryId(e.target.value)}
          disabled={!wholesalerId}
        >
          <option value="">Brasserie (facultatif)</option>
          {breweryOptions.map((b) => (
            <option key={b.id} value={b.id}>
              {b.name}
            </option>
          ))}
        </select>

        <select
          className="rounded border px-3 py-2"
          value={beerId}
          onChange={(e) => setBeerId(e.target.value)}
          disabled={!wholesalerId}
        >
          <option value="">
            {wholesalerId ? 'Choisissez une bière' : 'Sélectionnez d’abord un grossiste'}
          </option>
          {availableBeers.map((b) => (
            <option key={b.id} value={b.id}>
              {b.name}
            </option>
          ))}
        </select>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <label htmlFor="stock" className="mb-1 block text-sm font-medium">
            Stock (unités)
          </label>
          <input
            id="stock"
            type="number"
            min={0}
            className="w-full rounded border px-3 py-2"
            value={stock}
            onChange={(e) => setStock(parseInt(e.target.value || '0', 10))}
          />
        </div>

        <div>
          <label htmlFor="price" className="mb-1 block text-sm font-medium">
            Prix HTVA (€)
          </label>
          <input
            id="price"
            type="number"
            min={0}
            step="0.01"
            className="w-full rounded border px-3 py-2"
            value={price}
            onChange={(e) => setPrice(parseFloat(e.target.value || '0'))}
          />
        </div>
      </div>

      <button
        className="rounded-lg border px-3 py-2 disabled:opacity-50"
        disabled={!wholesalerId || !beerId}
      >
        Associer
      </button>

      {msg && <p className="text-sm text-gray-600">{msg}</p>}
    </form>
  );
}
