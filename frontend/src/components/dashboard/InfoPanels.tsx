
import { ChevronDown, Shirt, Baby } from "lucide-react";
import { cn } from "@/lib/utils";

const productStats = [
  { label: "Low Stock Items", value: "3", warning: true },
  { label: "All Item Group", value: "39" },
  { label: "All Items", value: "190" },
  { label: "Unconfirmed Items", value: "121", warning: true },
];

const topItems = [
  {
    icon: Shirt,
    name: "Hantswoly Cotton Casual",
    count: "171",
    unit: "pcs",
    bgColor: "bg-[#ffeddb]",
    iconColor: "text-[#ff8c00]",
  },
  {
    icon: Baby,
    name: "Cutiepie Rompers-special",
    count: "45",
    unit: "sets",
    bgColor: "bg-[#e6e6ff]",
    iconColor: "text-[#5050ff]",
  },
];

export const InfoPanels = () => {
  return (
    <div className="grid grid-cols-2 gap-6 mb-6">
      {/* Product Details Panel */}
      <div className="bg-white rounded-lg p-6 shadow-sm">
        <div className="flex justify-between items-center pb-4 border-b mb-4">
          <h3 className="font-semibold text-sm">PRODUCT DETAILS</h3>
        </div>

        <div className="flex justify-between">
          <div className="space-y-4 w-1/2">
            {productStats.map((stat) => (
              <div key={stat.label} className="flex justify-between">
                <span className={cn("text-sm", stat.warning && "text-[#f44336]")}>
                  {stat.label}
                </span>
                <span className="font-semibold">{stat.value}</span>
              </div>
            ))}
          </div>

          <div className="relative w-[120px] h-[120px]">
            <div className="w-full h-full rounded-full bg-[conic-gradient(#4CAF50_0%_71%,#eee_71%_100%)]">
              <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-[80px] h-[80px] bg-white rounded-full flex items-center justify-center text-lg font-bold">
                71%
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Top Selling Items Panel */}
      <div className="bg-white rounded-lg p-6 shadow-sm">
        <div className="flex justify-between items-center pb-4 border-b mb-4">
          <h3 className="font-semibold text-sm">TOP SELLING ITEMS</h3>
          <button className="flex items-center gap-2 text-sm text-gray-600">
            Previous Year
            <ChevronDown className="w-4 h-4" />
          </button>
        </div>

        <div className="grid grid-cols-2 gap-4">
          {topItems.map((item) => (
            <div key={item.name} className="bg-gray-50 rounded-lg p-4 text-center">
              <div className={cn("w-20 h-20 mx-auto rounded-lg flex items-center justify-center mb-3", item.bgColor)}>
                <item.icon className={cn("w-6 h-6", item.iconColor)} />
              </div>
              <h4 className="text-sm mb-2">{item.name}</h4>
              <div className="text-xl font-bold">{item.count}</div>
              <div className="text-sm text-gray-600">{item.unit}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};
