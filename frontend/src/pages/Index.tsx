
import { Layout } from "@/components/Layout";
import { MetricsRow } from "@/components/dashboard/MetricsRow";
import { InfoPanels } from "@/components/dashboard/InfoPanels";
import { OrdersPanels } from "@/components/dashboard/OrdersPanels";

const Index = () => {
  return (
    <Layout>
      <div className="py-6">
        <div className="flex justify-between mb-6">
          <h2 className="text-lg font-semibold text-gray-800">Sales Activity</h2>
          <h2 className="text-lg font-semibold text-gray-800">Inventory Summary</h2>
        </div>
        
        <MetricsRow />
        <InfoPanels />
        <OrdersPanels />
      </div>
    </Layout>
  );
};

export default Index;
