import WholesalerStock from '../components/WholesalerStock';
import UpdateStockForm from '../components/UpdateStockForm';
export default function StockPage() {
  return (
    <div className="space-y-6">
      <WholesalerStock />
      <UpdateStockForm />
    </div>
  );
}
