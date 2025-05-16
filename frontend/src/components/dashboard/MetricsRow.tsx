
import { Circle } from "lucide-react";
import { cn } from "@/lib/utils";

const metrics = [
  { value: "228", label: "TO BE PACKED", color: "text-[#2196F3]" },
  { value: "6", label: "TO BE SHIPPED", color: "text-[#f44336]" },
  { value: "10", label: "TO BE DELIVERED", color: "text-[#4CAF50]" },
  { value: "474", label: "TO BE INVOICED", color: "text-[#2196F3]" },
];

const summaryMetrics = [
  { label: "QUANTITY IN HAND", value: "10458..." },
  { label: "QUANTITY TO BE RECEIVED", value: "168" },
];

export const MetricsRow = () => {
  return (
    <div className="grid grid-cols-5 gap-4 mb-6">
      {metrics.map((metric) => (
        <div key={metric.label} className="bg-white rounded-lg p-4 shadow-sm flex flex-col items-center justify-center">
          <span className={cn("text-2xl font-bold mb-2", metric.color)}>
            {metric.value}
          </span>
          <div className="flex items-center gap-2 text-sm text-gray-600">
            <Circle className="w-4 h-4 text-gray-300" />
            {metric.label}
          </div>
        </div>
      ))}

      <div className="bg-white rounded-lg p-4 shadow-sm">
        {summaryMetrics.map((metric) => (
          <div key={metric.label} className="flex justify-between py-2">
            <span className="text-xs text-gray-600">{metric.label}</span>
            <span className="font-semibold">{metric.value}</span>
          </div>
        ))}
      </div>
    </div>
  );
};
