
import { Bell, ChevronDown, Cog, History, HelpCircle, Search } from "lucide-react";
import { Input } from "./ui/input";

export const Header = () => {
  return (
    <div className="flex justify-between items-center py-4">
      <div className="flex items-center gap-4">
        <Bell className="w-5 h-5 text-gray-600" />
        <History className="w-5 h-5 text-gray-600" />
      </div>

      <div className="relative w-[300px]">
        <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-4 h-4" />
        <Input
          className="pl-10 pr-4 h-10 rounded-full bg-white"
          placeholder="Search"
          type="search"
        />
      </div>

      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          Zylker
          <ChevronDown className="w-4 h-4" />
        </div>
        <Bell className="w-5 h-5 text-gray-600" />
        <Cog className="w-5 h-5 text-gray-600" />
        <HelpCircle className="w-5 h-5 text-gray-600" />
        <div className="w-8 h-8 rounded-full bg-[#ff8c00] text-white flex items-center justify-center font-bold">
          Z
        </div>
      </div>
    </div>
  );
};
