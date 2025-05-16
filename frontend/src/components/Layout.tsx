
import { Sidebar } from "./Sidebar";
import { Header } from "./Header";
import { cn } from "@/lib/utils";

export const Layout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="flex h-screen bg-[#f5f7fa] overflow-hidden">
      <Sidebar />
      <main className="flex-1 overflow-y-auto">
        <div className="container">
          <Header />
          {children}
        </div>
      </main>
    </div>
  );
};
