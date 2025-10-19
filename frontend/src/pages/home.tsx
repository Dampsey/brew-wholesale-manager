import { Link } from 'react-router-dom';

export default function Home() {
  const cards = [
    {
      title: 'Bières',
      desc: 'Consulter la liste des bières',
      to: '/beers',
      cta: 'Voir les bières',
    },
    {
      title: 'Associer',
      desc: 'Associer une bière à un grossiste',
      to: '/attach',
      cta: 'Associer une bière',
    },
    {
      title: 'Stocks',
      desc: 'Consulter et mettre à jour les stocks d’un grossiste',
      to: '/stock',
      cta: 'Gérer les stocks',
    },
    {
      title: 'Devis',
      desc: 'Composer un devis par grossiste',
      to: '/quote',
      cta: 'Faire un devis',
    },
  ];

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-semibold">Bienvenue</h1>
        <p className="text-gray-600">Gérez vos bières, associations grossistes, stocks et devis</p>
      </div>

      <div className="grid gap-4 sm:grid-cols-2">
        {cards.map((c) => (
          <div key={c.to} className="rounded-2xl border p-5 shadow-sm">
            <div className="mb-3 flex items-center gap-3">
              <h2 className="text-lg font-semibold">{c.title}</h2>
            </div>
            <p className="mb-4 text-sm text-gray-600">{c.desc}</p>
            <Link
              to={c.to}
              className="inline-flex items-center gap-2 rounded-lg border px-3 py-2 hover:bg-gray-50"
            >
              {c.cta} <span aria-hidden>→</span>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}
