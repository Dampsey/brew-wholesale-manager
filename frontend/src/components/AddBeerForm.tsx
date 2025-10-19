import { useEffect, useState } from 'react';
import { getBreweries } from '../api/lookups';
import { createBeer } from '../api/beers';
import type { IdName } from '../types/lookup';

export default function AddBeerForm({ onCreated }: { onCreated?: () => void }) {
  const [breweries, setBreweries] = useState<IdName[]>([]);
  const [breweryId, setBreweryId] = useState('');
  const [name, setName] = useState('');
  const [alc, setAlc] = useState<number>(5.0);
  const [price, setPrice] = useState<number>(2.2);
  const [msg, setMsg] = useState<string | null>(null);

  useEffect(() => {
    getBreweries().then(setBreweries);
  }, []);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setMsg(null);
    try {
      await createBeer({
        breweryId,
        name,
        alcoholDegree: alc,
        priceHtva: price,
      });
      setMsg('Bière créée');
      setName('');
      onCreated?.();
    } catch (err: any) {
      setMsg(err?.message ?? 'Erreur');
    }
  }

  return (
    <form onSubmit={submit} className="space-y-3 rounded-xl border p-4">
      <h2 className="text-lg font-semibold">Créer une nouvelle bière</h2>
      <div className="grid gap-3 md:grid-cols-2">
        <select
          className="rounded border px-3 py-2"
          required
          value={breweryId}
          onChange={(e) => setBreweryId(e.target.value)}
        >
          <option value="">Choisissez une brasserie</option>
          {breweries.map((b) => (
            <option key={b.id} value={b.id}>
              {b.name}
            </option>
          ))}
        </select>
        <input
          className="rounded border px-3 py-2"
          placeholder="Nom de la bière"
          required
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </div>
      <div className="grid gap-3 md:grid-cols-2">
        <div>
          <label htmlFor="alc" className="mb-1 block text-sm font-medium">
            Degré d’alcool (% vol)
          </label>
          <input
            type="number"
            step="0.1"
            className="rounded border px-3 py-2"
            placeholder="Degré d’alcool"
            value={alc}
            onChange={(e) => setAlc(parseFloat(e.target.value || '0'))}
          />
        </div>
        <div>
          <label htmlFor="price" className="mb-1 block text-sm font-medium">
            Prix HTVA (€)
          </label>
          <input
            type="number"
            step="0.01"
            className="rounded border px-3 py-2"
            placeholder="Prix HTVA"
            value={price}
            onChange={(e) => setPrice(parseFloat(e.target.value || '0'))}
          />
        </div>
      </div>
      <button className="rounded-lg border px-3 py-2">Créer</button>
      {msg && <p className="text-sm text-gray-600">{msg}</p>}
    </form>
  );
}
