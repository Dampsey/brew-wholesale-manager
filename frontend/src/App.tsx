import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Home from './pages/home';
import BeersPage from './pages/BeersPage';
import AttachPage from './pages/AttachPage';
import StockPage from './pages/StockPage';
import QuotePage from './pages/QuotePage';

export default function App() {
  return (
    <BrowserRouter>
      <header className="border-b">
        <nav className="mx-auto flex max-w-5xl items-center gap-4 p-4">
          <div className="font-bold">HyBrew Manager</div>
          <NavLink to="/" end className={({ isActive }) => linkCls(isActive)}>
            Accueil
          </NavLink>
          <NavLink to="/beers" className={({ isActive }) => linkCls(isActive)}>
            Bières
          </NavLink>
          <NavLink to="/attach" className={({ isActive }) => linkCls(isActive)}>
            Associer
          </NavLink>
          <NavLink to="/stock" className={({ isActive }) => linkCls(isActive)}>
            Stocks
          </NavLink>
          <NavLink to="/quote" className={({ isActive }) => linkCls(isActive)}>
            Devis
          </NavLink>
        </nav>
      </header>

      <main className="mx-auto max-w-5xl p-6">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/beers" element={<BeersPage />} />
          <Route path="/attach" element={<AttachPage />} />
          <Route path="/stock" element={<StockPage />} />
          <Route path="/quote" element={<QuotePage />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

function linkCls(active: boolean) {
  return `rounded-lg px-3 py-1 ${active ? 'bg-gray-900 text-white' : 'hover:bg-gray-100'}`;
}
