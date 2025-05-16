import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Home, Package, Building2, ShoppingCart, Settings, LogOut } from 'lucide-react';
import { useAuth } from '../lib/AuthContext';

export const Sidebar: React.FC = () => {
  const location = useLocation();
  const { logout } = useAuth();

  const isActive = (path: string) => {
    return location.pathname === path;
  };

  const navItems = [
    { path: '/dashboard', icon: Home, label: 'Dashboard' },
    { path: '/inventory/add', icon: Package, label: 'Add Inventory' },
    { path: '/company/add', icon: Building2, label: 'Add Company' },
    { path: '/product/add', icon: ShoppingCart, label: 'Add Product' },
    { path: '/settings', icon: Settings, label: 'Settings' },
  ];

  return (
    <div className="fixed left-0 top-0 h-full w-64 bg-white dark:bg-gray-800 border-r border-gray-100 dark:border-gray-700 transition-colors duration-200">
      <div className="p-6">
        <h1 className="text-2xl font-bold text-gray-900 dark:text-white">Stock Pulse</h1>
      </div>
      <nav className="mt-6">
        {navItems.map((item) => (
          <Link
            key={item.path}
            to={item.path}
            className={`flex items-center px-6 py-3 text-gray-600 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors ${
              isActive(item.path) ? 'bg-indigo-50 dark:bg-indigo-900/20 text-indigo-600 dark:text-indigo-400' : ''
            }`}
          >
            <item.icon className="h-5 w-5 mr-3" />
            {item.label}
          </Link>
        ))}
        <button
          onClick={logout}
          className="w-full flex items-center px-6 py-3 text-gray-600 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors"
        >
          <LogOut className="h-5 w-5 mr-3" />
          Logout
        </button>
      </nav>
    </div>
  );
};
