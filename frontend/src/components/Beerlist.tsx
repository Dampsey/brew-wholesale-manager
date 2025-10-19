import { useEffect, useState } from 'react';
import { http } from '../api/http';
import type { BeerDto } from '../types/beer';

type Props = { refreshKey?: number };

export default function BeerList({ refreshKey = 0 }: Props) {
  const [beers, setBeers] = useState<BeerDto[]>([]);
  const [q, setQ] = useState('');
  const [err, setErr] = useState<string | null>(null);

  useEffect(() => {
    const ctrl = new AbortController();
    const query = q ? `?name=${encodeURIComponent(q)}` : '';

    setErr(null);
    http<BeerDto[]>(`/beers${query}`, { signal: ctrl.signal })
      .then(setBeers)
      .catch(() => {});

    return () => ctrl.abort();
  }, [q, refreshKey]);

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-semibold">Bières disponibles</h1>
      </div>

      <input
        className="w-full md:w-80 rounded-lg border px-3 py-2"
        placeholder="Filtrer par nom…"
        value={q}
        onChange={(e) => setQ(e.target.value)}
      />

      {err && (
        <div className="rounded-lg border border-red-200 bg-red-50 p-3 text-sm text-red-700">
          {err}
        </div>
      )}

      <div className="overflow-x-auto rounded-2xl border">
        <table className="min-w-full text-sm">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-4 py-3 text-left">Bière</th>
              <th className="px-4 py-3 text-left">Brasserie</th>
              <th className="px-4 py-3 text-left">Degré</th>
              <th className="px-4 py-3 text-left">Prix (HT)</th>
              <th className="px-4 py-3 text-left">Grossistes</th>
            </tr>
          </thead>
          <tbody>
            {beers.map((b) => (
              <tr key={b.id} className="odd:bg-white even:bg-gray-50">
                <td className="px-4 py-3">{b.name}</td>
                <td className="px-4 py-3">{b.brewery?.name}</td>
                <td className="px-4 py-3">{b.alcoholDegree?.toFixed?.(1)}%</td>
                <td className="px-4 py-3">{b.priceHtva?.toFixed?.(2)} €</td>
                <td className="px-4 py-3">
                  <div className="flex flex-wrap gap-2">
                    {b.wholesalers?.map((w) => (
                      <span
                        key={w.wholesalerId}
                        className="inline-flex items-center gap-2 rounded-full border px-2 py-1"
                      >
                        <span className="font-medium">{w.name}</span>
                        <span className="text-xs text-gray-500">stock {w.stock}</span>
                        <span className="text-xs">• {w.salePrice.toFixed(2)} €</span>
                      </span>
                    ))}
                    {(!b.wholesalers || b.wholesalers.length === 0) && (
                      <span className="text-gray-500">—</span>
                    )}
                  </div>
                </td>
              </tr>
            ))}
            {beers.length === 0 && !err && (
              <tr>
                <td className="px-4 py-6 text-center text-gray-500" colSpan={5}>
                  Aucune bière trouvée
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
