import { useState } from 'react';
import BeerList from '../components/Beerlist';
import AddBeerForm from '../components/AddBeerForm';

export default function BeersPage() {
  const [open, setOpen] = useState(false);
  const [refreshKey, setRefreshKey] = useState(0);

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-semibold">Bières</h1>
        <button className="rounded-lg border px-3 py-2" onClick={() => setOpen(true)}>
          Ajouter une bière
        </button>
      </div>

      <BeerList refreshKey={refreshKey} />

      {open && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40">
          <div className="w-full max-w-lg rounded-2xl bg-white p-6 shadow-xl">
            <div className="mb-4 flex items-center justify-between">
              <h2 className="text-lg font-semibold">Créer une nouvelle bière</h2>
              <button
                className="rounded-md px-2 py-1 hover:bg-gray-100"
                onClick={() => setOpen(false)}
              >
                ✕
              </button>
            </div>

            <AddBeerForm
              onCreated={() => {
                setOpen(false);
                setRefreshKey((k) => k + 1);
              }}
            />
          </div>
        </div>
      )}
    </div>
  );
}
