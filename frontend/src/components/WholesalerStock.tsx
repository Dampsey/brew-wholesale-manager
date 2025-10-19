import { useEffect, useState } from 'react';
import { getWholesalers } from '../api/lookups';
import { getStock } from '../api/wholesalers';
import type { IdName, StockItem } from '../types/lookup';

export default function WholesalerStock() {
  const [wholesalers, setWholesalers] = useState<IdName[]>([]);
  const [wholesalerId, setWholesalerId] = useState('');
  const [items, setItems] = useState<StockItem[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getWholesalers().then(setWholesalers);
  }, []);

  useEffect(() => {
    if (!wholesalerId) {
      setItems([]);
      return;
    }
    setLoading(true);
    getStock(wholesalerId)
      .then(setItems)
      .finally(() => setLoading(false));
  }, [wholesalerId]);

  return (
    <div className="space-y-3 rounded-xl border p-4">
      <h2 className="text-lg font-semibold">Stocks d’un grossiste</h2>

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

      <div className="overflow-x-auto rounded-2xl border">
        <table className="min-w-full text-sm">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-4 py-3 text-left">Bière</th>
              <th className="px-4 py-3 text-left">Brasserie</th>
              <th className="px-4 py-3 text-right">Stock</th>
              <th className="px-4 py-3 text-right">Prix HT</th>
            </tr>
          </thead>
          <tbody>
            {items.map((x) => (
              <tr key={x.beerId} className="odd:bg-white even:bg-gray-50">
                <td className="px-4 py-3">{x.beer}</td>
                <td className="px-4 py-3">{x.brewery}</td>
                <td className="px-4 py-3 text-right">{x.stock}</td>
                <td className="px-4 py-3 text-right">{x.salePrice.toFixed(2)} €</td>
              </tr>
            ))}
            {!loading && items.length === 0 && (
              <tr>
                <td className="px-4 py-6 text-center text-gray-500" colSpan={4}>
                  —
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
