import { useEffect, useMemo, useState } from 'react';
import { getWholesalers } from '../api/lookups';
import { getStock } from '../api/wholesalers';
import { requestQuote, type QuoteResponse } from '../api/quote';
import type { IdName } from '../types/lookup';

type Item = { beerId: string; quantity: number };
type StockItem = {
  beerId: string;
  beer: string;
  brewery: string;
  stock: number;
  salePrice: number;
};

export default function QuoteForm() {
  const [wholesalers, setWholesalers] = useState<IdName[]>([]);
  const [wsId, setWsId] = useState('');
  const [catalog, setCatalog] = useState<StockItem[]>([]);
  const [breweryFilterName, setBreweryFilterName] = useState('');

  const [items, setItems] = useState<Item[]>([{ beerId: '', quantity: 1 }]);
  const [result, setResult] = useState<QuoteResponse | null>(null);
  const [msg, setMsg] = useState<string | null>(null);

  useEffect(() => {
    getWholesalers().then(setWholesalers);
  }, []);

  useEffect(() => {
    setCatalog([]);
    setResult(null);
    setMsg(null);
    setItems([{ beerId: '', quantity: 1 }]);
    setBreweryFilterName('');
    if (!wsId) return;
    getStock(wsId)
      .then(setCatalog)
      .catch((e) => setMsg(e?.message ?? 'Erreur'));
  }, [wsId]);

  const filteredCatalog = useMemo(() => {
    const src = breweryFilterName
      ? catalog.filter((c) => c.brewery === breweryFilterName)
      : catalog;
    return [...src].sort(
      (a, b) => Number(b.stock > 0) - Number(a.stock > 0) || a.beer.localeCompare(b.beer),
    );
  }, [catalog, breweryFilterName]);

  const selectedIds = useMemo(() => new Set(items.map((i) => i.beerId).filter(Boolean)), [items]);

  const getStockOf = (beerId: string) => catalog.find((c) => c.beerId === beerId)?.stock ?? 0;

  const quantitiesOk = items.every(
    (i) => i.beerId && i.quantity > 0 && i.quantity <= getStockOf(i.beerId),
  );

  const canSubmit = !!wsId && items.length > 0 && quantitiesOk;

  function setItem(idx: number, patch: Partial<Item>) {
    setItems((list) => list.map((it, i) => (i === idx ? { ...it, ...patch } : it)));
  }
  function addLine() {
    setItems((list) => [...list, { beerId: '', quantity: 1 }]);
  }
  function removeLine(idx: number) {
    setItems((list) => list.filter((_, i) => i !== idx));
  }

  function handleSelectBeer(idx: number, beerId: string) {
    const stock = getStockOf(beerId);
    setItem(idx, {
      beerId,
      quantity: Math.min(Math.max(1, items[idx]?.quantity ?? 1), stock || 1),
    });
  }

  function handleQtyChange(idx: number, raw: string) {
    const currentBeerId = items[idx]?.beerId;
    const max = currentBeerId ? getStockOf(currentBeerId) : Infinity;
    const val = Math.max(1, Math.min(parseInt(raw || '1', 10), max));
    setItem(idx, { quantity: val });
  }

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setMsg(null);
    setResult(null);
    const ids = items.map((i) => i.beerId);
    new Set(ids).size !== ids.length;
    const res = await requestQuote(wsId, { items });
    setResult(res);
  }

  return (
    <div className="space-y-4 rounded-2xl border p-4">
      <h2 className="text-lg font-semibold">Demander un devis</h2>

      <form onSubmit={submit} className="space-y-3">
        <select
          className="w-full rounded border px-3 py-2"
          value={wsId}
          onChange={(e) => setWsId(e.target.value)}
        >
          <option value="">Choisissez un grossiste</option>
          {wholesalers.map((w) => (
            <option key={w.id} value={w.id}>
              {w.name}
            </option>
          ))}
        </select>

        {wsId && (
          <div className="space-y-3">
            {items.map((it, idx) => {
              const options = filteredCatalog.filter(
                (c) => c.beerId === it.beerId || !selectedIds.has(c.beerId),
              );
              const currentStock = getStockOf(it.beerId);
              const ratio = it.beerId ? Math.min(1, it.quantity / Math.max(1, currentStock)) : 0;

              return (
                <div key={idx} className="space-y-2 rounded-lg border p-3">
                  <div className="grid gap-2 md:grid-cols-[1fr_140px_40px]">
                    <select
                      className="rounded border px-3 py-2"
                      value={it.beerId}
                      onChange={(e) => handleSelectBeer(idx, e.target.value)}
                      disabled={filteredCatalog.length === 0}
                    >
                      <option value="">Bières vendues par ce grossiste</option>
                      {options.map((c) => (
                        <option key={c.beerId} value={c.beerId} disabled={c.stock === 0}>
                          {c.beer} — {c.brewery}{' '}
                          {c.stock === 0 ? ' (rupture)' : ` (stock: ${c.stock})`}
                        </option>
                      ))}
                    </select>

                    <input
                      type="number"
                      min={1}
                      max={it.beerId ? currentStock : undefined}
                      className="rounded border px-3 py-2"
                      value={it.quantity}
                      onChange={(e) => handleQtyChange(idx, e.target.value)}
                      disabled={!it.beerId}
                      title={it.beerId ? `Stock max: ${currentStock}` : 'Choisir une bière'}
                    />

                    <button
                      type="button"
                      className="rounded border px-2 py-1"
                      onClick={() => removeLine(idx)}
                      disabled={items.length === 1}
                      title="Retirer cette ligne"
                    >
                      −
                    </button>
                  </div>

                  {it.beerId && (
                    <div className="flex items-center justify-between text-sm">
                      <span className="text-gray-600">Stock: {currentStock}</span>
                      <div className="ml-3 h-2 w-47 rounded bg-gray-200">
                        <div
                          className={`h-2 rounded ${it.quantity > currentStock ? 'bg-red-500' : 'bg-gray-800'}`}
                          style={{ width: `${Math.min(100, ratio * 100)}%` }}
                        />
                      </div>
                    </div>
                  )}
                </div>
              );
            })}

            <button type="button" className="rounded border px-3 py-1" onClick={addLine}>
              + Ajouter une ligne
            </button>
          </div>
        )}

        <button className="rounded-lg border px-3 py-2 disabled:opacity-50" disabled={!canSubmit}>
          Calculer
        </button>
      </form>

      {msg && <p className="text-sm text-red-600">{msg}</p>}

      {result && (
        <div className="space-y-2 rounded-xl border p-3">
          <div className="flex items-center justify-between">
            <div>
              <div className="font-medium">{result.wholesaler}</div>
              <div className="text-sm text-gray-500">{result.totalQuantity} bières au total</div>
            </div>
            <div className="text-right">
              <div className="text-sm text-gray-500">
                Sous-total: {result.subtotal.toFixed(2)} €
              </div>
              {result.discountRate > 0 && (
                <div className="text-sm text-green-700">
                  Remise: {(result.discountRate * 100).toFixed(0)}%
                </div>
              )}
              <div className="text-lg font-semibold">Total: {result.total.toFixed(2)} € HT</div>
            </div>
          </div>

          <div className="overflow-x-auto rounded-lg border">
            <table className="min-w-full text-sm">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-3 py-2 text-left">Bière</th>
                  <th className="px-3 py-2 text-left">Brasserie</th>
                  <th className="px-3 py-2 text-right">Quantité</th>
                  <th className="px-3 py-2 text-right">Prix unitaire</th>
                  <th className="px-3 py-2 text-right">Total</th>
                </tr>
              </thead>
              <tbody>
                {result.lines.map((l) => (
                  <tr key={l.beerId} className="odd:bg-white even:bg-gray-50">
                    <td className="px-3 py-2">{l.beerName}</td>
                    <td className="px-3 py-2">
                      {catalog.find((c) => c.beerId === l.beerId)?.brewery ?? '—'}
                    </td>
                    <td className="px-3 py-2 text-right">{l.quantity}</td>
                    <td className="px-3 py-2 text-right">{l.unitPrice.toFixed(2)} €</td>
                    <td className="px-3 py-2 text-right">{l.lineTotal.toFixed(2)} €</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
}
