
import { ChevronDown } from "lucide-react";

export const OrdersPanels = () => {
  return (
    <div className="grid grid-cols-2 gap-6">
      {/* Purchase Order Panel */}
      <div className="bg-white rounded-lg p-6 shadow-sm">
        <div className="flex justify-between items-center pb-4 border-b mb-4">
          <h3 className="font-semibold text-sm">PURCHASE ORDER</h3>
          <button className="flex items-center gap-2 text-sm text-gray-600">
            This Month
            <ChevronDown className="w-4 h-4" />
          </button>
        </div>

        <div>
          <div className="text-sm text-gray-600">Quantity Ordered</div>
          <div className="text-2xl font-bold text-[#2196F3] mt-2">652.00</div>
        </div>
      </div>

      {/* Sales Order Panel */}
      <div className="bg-white rounded-lg p-6 shadow-sm">
        <div className="flex justify-between items-center pb-4 border-b mb-4">
          <h3 className="font-semibold text-sm">SALES ORDER</h3>
        </div>

        <table className="w-full">
          <thead>
            <tr className="text-sm text-gray-800">
              <th className="text-left py-2">Channel</th>
              <th className="text-left py-2">Draft</th>
              <th className="text-left py-2">Confirmed</th>
              <th className="text-left py-2">Packed</th>
              <th className="text-left py-2">Shipped</th>
            </tr>
          </thead>
          <tbody>
            <tr className="text-sm text-gray-600">
              <td className="py-2">Direct sales</td>
              <td className="py-2">0</td>
              <td className="py-2">50</td>
              <td className="py-2">0</td>
              <td className="py-2">0</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};
