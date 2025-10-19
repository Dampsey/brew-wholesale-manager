import { useEffect, useMemo, useState } from 'react';
import { getWholesalers } from '../api/lookups';
import { getStock, updateStock } from '../api/wholesalers';
import type { IdName } from '../types/lookup';

type StockItem = {
  beerId: string;
  beer: string;
  brewery: string;
  stock: number;
  salePrice: number;
};

export default function UpdateStockForm() {
  const [wholesalers, setWholesalers] = useState<IdName[]>([]);
  const [wholesalerId, setWholesalerId] = useState('');

  const [catalog, setCatalog] = useState<StockItem[]>([]);

  const [q, setQ] = useState('');
  const [beerId, setBeerId] = useState('');
  const [stock, setStock] = useState<number>(0);
  const [msg, setMsg] = useState<string | null>(null);

  useEffect(() => {
    getWholesalers().then(setWholesalers);
  }, []);

  useEffect(() => {
    setCatalog([]);
    setBeerId('');
    setStock(0);
    setQ('');
    setMsg(null);
    if (!wholesalerId) return;
    getStock(wholesalerId)
      .then(setCatalog)
      .catch(() => setMsg('Erreur'));
  }, [wholesalerId]);

  const filteredCatalog = useMemo(() => {
    const term = q.toLowerCase();
    return catalog.filter((c) => c.beer.toLowerCase().includes(term));
  }, [catalog, q]);

  function onSelectBeer(id: string) {
    setBeerId(id);
    const found = catalog.find((c) => c.beerId === id);
    if (found) setStock(found.stock);
  }

  useEffect(() => {
    if (!wholesalerId) return;
    const selectedStillVisible = filteredCatalog.some((b) => b.beerId === beerId);
    if (!selectedStillVisible && filteredCatalog.length === 1) {
      onSelectBeer(filteredCatalog[0].beerId);
    }
  }, [filteredCatalog, wholesalerId, beerId]);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setMsg(null);
    try {
      if (!wholesalerId || !beerId) throw new Error();
      await updateStock(wholesalerId, beerId, stock);
      setMsg('Stock mis à jour');
    } catch {
      setMsg('Erreur');
    }
  }

  return (
    <form onSubmit={submit} className="space-y-3 rounded-xl border p-4">
      <h2 className="text-lg font-semibold">Mettre à jour le stock</h2>

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

        <input
          className="rounded border px-3 py-2"
          placeholder="Filtrer bières…"
          value={q}
          onChange={(e) => setQ(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === 'Enter' && filteredCatalog.length > 0) {
              e.preventDefault();
              onSelectBeer(filteredCatalog[0].beerId);
            }
          }}
          disabled={!wholesalerId || catalog.length === 0}
        />

        <select
          className="rounded border px-3 py-2"
          value={beerId}
          onChange={(e) => onSelectBeer(e.target.value)}
          disabled={!wholesalerId || catalog.length === 0}
        >
          <option value="">{wholesalerId ? 'Bières vendues par ce grossiste' : ''}</option>
          {filteredCatalog.map((b) => (
            <option key={b.beerId} value={b.beerId}>
              {b.beer} — {b.brewery}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label htmlFor="stock" className="mb-1 block text-sm font-medium">
          Stock (unités)
        </label>
        <input
          id="stock"
          type="number"
          min={0}
          className="w-full rounded border px-3 py-2"
          placeholder="Nouveau stock"
          value={stock}
          onChange={(e) => setStock(parseInt(e.target.value || '0', 10))}
          disabled={!beerId}
        />
      </div>

      <button className="rounded-lg border px-3 py-2 disabled:opacity-50" disabled={!beerId}>
        Mettre à jour
      </button>

      {msg && <p className="text-sm text-gray-600">{msg}</p>}
    </form>
  );
}
