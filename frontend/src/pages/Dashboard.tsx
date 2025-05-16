import React, { useState, useEffect, useMemo } from 'react';
import { useAuth } from '../lib/AuthContext';
import { useNavigate } from 'react-router-dom';
import { Eye, EyeOff, Search, Bell, Settings, HelpCircle, ChevronDown, Package, ShoppingCart, ShoppingBag, Link, Network, BarChart2, FileText, RefreshCw, ArrowUp, ArrowDown, Building2, Trash2, Sun, Moon } from 'lucide-react';
import { dashboardService, DashboardMetrics, ProductDetails, TopSellingItem, SalesOrder, PurchaseOrder } from '../lib/dashboard';
import { companyService, Company } from '../lib/company';
import { inventoryService, InventoryItem } from '../lib/inventory';
import { ErrorAlert } from '../components/ErrorAlert';
import { MetricSkeleton, ProductDetailsSkeleton, TopSellingItemsSkeleton, OrderPanelSkeleton } from '../components/Skeleton';
import { Tooltip } from '../components/Tooltip';
import { Sidebar } from '../components/Sidebar';
import '../styles/dashboard.css';

export const Dashboard: React.FC = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState('');
  const [loading, setLoading] = useState(true);
  const [metrics, setMetrics] = useState<DashboardMetrics | null>(null);
  const [productDetails, setProductDetails] = useState<ProductDetails | null>(null);
  const [topSellingItems, setTopSellingItems] = useState<TopSellingItem[]>([]);
  const [salesOrders, setSalesOrders] = useState<SalesOrder[]>([]);
  const [purchaseOrders, setPurchaseOrders] = useState<PurchaseOrder | null>(null);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [inventoryItems, setInventoryItems] = useState<InventoryItem[]>([]);
  const [isRefreshing, setIsRefreshing] = useState(false);
  const [isDarkMode, setIsDarkMode] = useState(() => {
    const savedMode = localStorage.getItem('darkMode');
    return savedMode ? JSON.parse(savedMode) : false;
  });
  const [deleteConfirm, setDeleteConfirm] = useState<{
    type: 'company' | 'inventory' | null;
    id: string | null;
    name: string;
  }>({ type: null, id: null, name: '' });
  const [sortConfig, setSortConfig] = useState<{
    key: keyof SalesOrder;
    direction: 'asc' | 'desc';
  } | null>(null);

  useEffect(() => {
    localStorage.setItem('darkMode', JSON.stringify(isDarkMode));
    if (isDarkMode) {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
  }, [isDarkMode]);

  const toggleDarkMode = () => {
    setIsDarkMode(!isDarkMode);
  };

  const fetchDashboardData = async () => {
    try {
      setLoading(true);
      
      // Fetch companies and inventory items
      const companiesData = companyService.getCompanies();
      const inventoryData = inventoryService.getInventory();
      
      setCompanies(companiesData);
      setInventoryItems(inventoryData);

      // Fetch other dashboard data
      const [metricsData, productDetailsData, topSellingItemsData, salesOrdersData, purchaseOrdersData] = await Promise.all([
        dashboardService.getMetrics(),
        dashboardService.getProductDetails(),
        dashboardService.getTopSellingItems(),
        dashboardService.getSalesOrders(),
        dashboardService.getPurchaseOrders()
      ]);

      setMetrics(metricsData);
      setProductDetails(productDetailsData);
      setTopSellingItems(topSellingItemsData);
      setSalesOrders(salesOrdersData);
      setPurchaseOrders(purchaseOrdersData);
    } finally {
      setLoading(false);
      setIsRefreshing(false);
    }
  };

  useEffect(() => {
    fetchDashboardData();
  }, []);

  const handleRefresh = () => {
    setIsRefreshing(true);
    fetchDashboardData();
  };

  const handleSort = (key: keyof SalesOrder) => {
    let direction: 'asc' | 'desc' = 'asc';
    if (sortConfig && sortConfig.key === key && sortConfig.direction === 'asc') {
      direction = 'desc';
    }
    setSortConfig({ key, direction });
  };

  const sortedSalesOrders = useMemo(() => {
    if (!sortConfig) return salesOrders;

    return [...salesOrders].sort((a, b) => {
      if (a[sortConfig.key] < b[sortConfig.key]) {
        return sortConfig.direction === 'asc' ? -1 : 1;
      }
      if (a[sortConfig.key] > b[sortConfig.key]) {
        return sortConfig.direction === 'asc' ? 1 : -1;
      }
      return 0;
    });
  }, [salesOrders, sortConfig]);

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    // Filter data based on search query
    const query = searchQuery.toLowerCase();
    
    // Filter top selling items
    const filteredTopSellingItems = topSellingItems.filter(item => 
      item.name.toLowerCase().includes(query)
    );
    setTopSellingItems(filteredTopSellingItems);

    // Filter sales orders
    const filteredSalesOrders = salesOrders.filter(order =>
      order.channel.toLowerCase().includes(query)
    );
    setSalesOrders(filteredSalesOrders);
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
    if (!e.target.value) {
      // Reset to original data when search is cleared
      fetchDashboardData();
    }
  };

  const handleDeleteCompany = async (id: string, name: string) => {
    setDeleteConfirm({ type: 'company', id, name });
  };

  const handleDeleteInventory = async (id: string, name: string) => {
    setDeleteConfirm({ type: 'inventory', id, name });
  };

  const confirmDelete = async () => {
    if (!deleteConfirm.id || !deleteConfirm.type) return;

    try {
      if (deleteConfirm.type === 'company') {
        await companyService.deleteCompany(deleteConfirm.id);
        setCompanies(companies.filter(c => c.id !== deleteConfirm.id));
      } else {
        await inventoryService.deleteInventory(deleteConfirm.id);
        setInventoryItems(inventoryItems.filter(i => i.id !== deleteConfirm.id));
      }
      setDeleteConfirm({ type: null, id: null, name: '' });
    } catch (error) {
      console.error('Delete error:', error);
      setError('Failed to delete item. Please try again later.');
    }
  };

  const cancelDelete = () => {
    setDeleteConfirm({ type: null, id: null, name: '' });
  };

  const renderMetrics = () => {
    if (loading) {
      return Array(5).fill(null).map((_, index) => (
        <MetricSkeleton key={index} />
      ));
    }

    return (
      <>
        <div className="bg-gradient-to-br from-blue-500 to-blue-600 dark:from-blue-600 dark:to-blue-700 p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-200">
          <div className="text-3xl font-bold text-white mb-2">{metrics?.toBePacked || 0}</div>
          <div className="text-sm text-blue-100 flex items-center gap-2">
            <Package className="h-5 w-5" />
            TO BE PACKED
          </div>
        </div>
        <div className="bg-gradient-to-br from-red-500 to-red-600 dark:from-red-600 dark:to-red-700 p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-200">
          <div className="text-3xl font-bold text-white mb-2">{metrics?.toBeShipped || 0}</div>
          <div className="text-sm text-red-100 flex items-center gap-2">
            <ShoppingBag className="h-5 w-5" />
            TO BE SHIPPED
          </div>
        </div>
        <div className="bg-gradient-to-br from-green-500 to-green-600 dark:from-green-600 dark:to-green-700 p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-200">
          <div className="text-3xl font-bold text-white mb-2">{metrics?.toBeDelivered || 0}</div>
          <div className="text-sm text-green-100 flex items-center gap-2">
            <ShoppingCart className="h-5 w-5" />
            TO BE DELIVERED
          </div>
        </div>
        <div className="bg-gradient-to-br from-purple-500 to-purple-600 dark:from-purple-600 dark:to-purple-700 p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-200">
          <div className="text-3xl font-bold text-white mb-2">{metrics?.toBeInvoiced || 0}</div>
          <div className="text-sm text-purple-100 flex items-center gap-2">
            <FileText className="h-5 w-5" />
            TO BE INVOICED
          </div>
        </div>
        <div className="bg-gradient-to-br from-indigo-500 to-indigo-600 dark:from-indigo-600 dark:to-indigo-700 p-6 rounded-xl shadow-lg transform hover:scale-105 transition-transform duration-200">
          <div className="flex flex-col space-y-2">
            <div className="flex justify-between items-center">
              <div className="text-sm text-indigo-100">QUANTITY IN HAND</div>
              <div className="text-xl font-bold text-white">{metrics?.quantityInHand || 0}</div>
            </div>
            <div className="flex justify-between items-center">
              <div className="text-sm text-indigo-100">QUANTITY TO BE RECEIVED</div>
              <div className="text-xl font-bold text-white">{metrics?.quantityToBeReceived || 0}</div>
            </div>
          </div>
        </div>
      </>
    );
  };

  return (
    <div className="flex min-h-screen bg-gray-50 dark:bg-gray-900 transition-colors duration-200">
      <Sidebar />
      <div className="flex-1 ml-64 p-8">
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-white">Dashboard</h1>
          <div className="flex items-center space-x-4">
            <form onSubmit={handleSearch} className="relative">
              <input
                type="text"
                placeholder="Search..."
                value={searchQuery}
                onChange={handleSearchChange}
                className="pl-10 pr-4 py-2 border border-gray-200 dark:border-gray-700 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent shadow-sm bg-white dark:bg-gray-800 text-gray-900 dark:text-white placeholder-gray-500 dark:placeholder-gray-400"
              />
              <Search className="absolute left-3 top-2.5 h-5 w-5 text-gray-400 dark:text-gray-500" />
            </form>
            <button
              onClick={toggleDarkMode}
              className="p-2 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-xl transition-colors"
              title={isDarkMode ? "Switch to light mode" : "Switch to dark mode"}
            >
              {isDarkMode ? (
                <Sun className="h-5 w-5 text-yellow-500" />
              ) : (
                <Moon className="h-5 w-5 text-gray-600" />
              )}
            </button>
            <button
              onClick={handleRefresh}
              className="p-2 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-xl transition-colors"
              disabled={isRefreshing}
            >
              <RefreshCw className={`h-5 w-5 text-indigo-600 dark:text-indigo-400 ${isRefreshing ? 'animate-spin' : ''}`} />
            </button>
            <button className="p-2 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-xl transition-colors">
              <Bell className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
            </button>
            <button className="p-2 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-xl transition-colors">
              <Settings className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
            </button>
            <button className="p-2 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-xl transition-colors">
              <HelpCircle className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
            </button>
          </div>
        </div>

        {/* Delete Confirmation Modal */}
        {deleteConfirm.type && deleteConfirm.id && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 backdrop-blur-sm">
            <div className="bg-white dark:bg-gray-800 rounded-xl p-6 max-w-md w-full mx-4 shadow-2xl transform transition-all">
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-4">Confirm Delete</h3>
              <p className="text-gray-600 dark:text-gray-300 mb-6">
                Are you sure you want to delete {deleteConfirm.name}? This action cannot be undone.
              </p>
              <div className="flex justify-end space-x-4">
                <button
                  onClick={cancelDelete}
                  className="px-4 py-2 text-gray-600 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 rounded-lg transition-colors"
                >
                  Cancel
                </button>
                <button
                  onClick={confirmDelete}
                  className="px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition-colors shadow-md hover:shadow-lg"
                >
                  Delete
                </button>
              </div>
            </div>
          </div>
        )}

        <div className="grid grid-cols-5 gap-6 mb-8">
          {renderMetrics()}
        </div>

        <div className="grid grid-cols-2 gap-8 mb-8">
          <div className="bg-white dark:bg-gray-800 rounded-xl shadow-lg p-6 border border-gray-100 dark:border-gray-700">
            <h2 className="text-xl font-semibold text-gray-900 dark:text-white mb-4 flex items-center gap-2">
              <Building2 className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
              Companies
            </h2>
            {loading ? (
              <div className="animate-pulse space-y-4">
                {[1, 2, 3].map((i) => (
                  <div key={i} className="h-16 bg-gray-200 dark:bg-gray-700 rounded-lg"></div>
                ))}
              </div>
            ) : companies.length === 0 ? (
              <p className="text-gray-500 dark:text-gray-400 text-center py-4">No companies added yet</p>
            ) : (
              <div className="space-y-4">
                {companies.map((company) => (
                  <div key={company.id} className="flex items-center justify-between p-4 border border-gray-100 dark:border-gray-700 rounded-xl hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
                    <div className="flex items-center space-x-3">
                      <div className="w-12 h-12 bg-indigo-100 dark:bg-indigo-900 rounded-xl flex items-center justify-center">
                        <Building2 className="h-6 w-6 text-indigo-600 dark:text-indigo-400" />
                      </div>
                      <div>
                        <div className="font-medium text-gray-900 dark:text-white">{company.name}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">{company.email}</div>
                      </div>
                    </div>
                    <div className="flex items-center space-x-4">
                      <div className="text-right">
                        <div className="text-sm text-gray-500 dark:text-gray-400">{company.phone}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">{company.address}</div>
                      </div>
                      <button
                        onClick={() => handleDeleteCompany(company.id, company.name)}
                        className="p-2 text-gray-400 hover:text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                        title="Delete company"
                      >
                        <Trash2 className="h-5 w-5" />
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>

          <div className="bg-white dark:bg-gray-800 rounded-xl shadow-lg p-6 border border-gray-100 dark:border-gray-700">
            <h2 className="text-xl font-semibold text-gray-900 dark:text-white mb-4 flex items-center gap-2">
              <Package className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
              Inventory Items
            </h2>
            {loading ? (
              <div className="animate-pulse space-y-4">
                {[1, 2, 3].map((i) => (
                  <div key={i} className="h-16 bg-gray-200 dark:bg-gray-700 rounded-lg"></div>
                ))}
              </div>
            ) : inventoryItems.length === 0 ? (
              <p className="text-gray-500 dark:text-gray-400 text-center py-4">No inventory items added yet</p>
            ) : (
              <div className="space-y-4">
                {inventoryItems.map((item) => (
                  <div key={item.id} className="flex items-center justify-between p-4 border border-gray-100 dark:border-gray-700 rounded-xl hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
                    <div className="flex items-center space-x-3">
                      <div className="w-12 h-12 bg-indigo-100 dark:bg-indigo-900 rounded-xl flex items-center justify-center">
                        <Package className="h-6 w-6 text-indigo-600 dark:text-indigo-400" />
                      </div>
                      <div>
                        <div className="font-medium text-gray-900 dark:text-white">{item.name}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">SKU: {item.sku}</div>
                      </div>
                    </div>
                    <div className="flex items-center space-x-4">
                      <div className="text-right">
                        <div className="font-medium text-indigo-600 dark:text-indigo-400">${item.price.toFixed(2)}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">Qty: {item.quantity}</div>
                      </div>
                      <button
                        onClick={() => handleDeleteInventory(item.id, item.name)}
                        className="p-2 text-gray-400 hover:text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20 rounded-lg transition-colors"
                        title="Delete inventory item"
                      >
                        <Trash2 className="h-5 w-5" />
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>

        <div className="grid grid-cols-2 gap-8">
          <div className="bg-white dark:bg-gray-800 rounded-xl shadow-lg p-6 border border-gray-100 dark:border-gray-700">
            <h2 className="text-xl font-semibold text-gray-900 dark:text-white mb-4 flex items-center gap-2">
              <BarChart2 className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
              Top Selling Items
            </h2>
            {loading ? (
              <TopSellingItemsSkeleton />
            ) : (
              <div className="space-y-4">
                {topSellingItems.map((item) => (
                  <div key={item.id} className="flex items-center justify-between p-4 border border-gray-100 dark:border-gray-700 rounded-xl hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
                    <div className="flex items-center space-x-3">
                      <div className="w-12 h-12 bg-indigo-100 dark:bg-indigo-900 rounded-xl flex items-center justify-center">
                        <Package className="h-6 w-6 text-indigo-600 dark:text-indigo-400" />
                      </div>
                      <div>
                        <div className="font-medium text-gray-900 dark:text-white">{item.name}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">SKU: {item.sku}</div>
                      </div>
                    </div>
                    <div className="text-right">
                      <div className="font-medium text-indigo-600 dark:text-indigo-400">{item.quantity} units</div>
                      <div className="text-sm text-gray-500 dark:text-gray-400">${item.revenue.toFixed(2)}</div>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>

          <div className="bg-white dark:bg-gray-800 rounded-xl shadow-lg p-6 border border-gray-100 dark:border-gray-700">
            <h2 className="text-xl font-semibold text-gray-900 dark:text-white mb-4 flex items-center gap-2">
              <ShoppingCart className="h-5 w-5 text-indigo-600 dark:text-indigo-400" />
              Recent Sales Orders
            </h2>
            {loading ? (
              <OrderPanelSkeleton />
            ) : (
              <div className="overflow-x-auto">
                <table className="min-w-full">
                  <thead>
                    <tr className="border-b border-gray-100 dark:border-gray-700">
                      <th className="text-left py-3 px-4 text-sm font-medium text-gray-500 dark:text-gray-400">Channel</th>
                      <th className="text-left py-3 px-4 text-sm font-medium text-gray-500 dark:text-gray-400">Order ID</th>
                      <th className="text-left py-3 px-4 text-sm font-medium text-gray-500 dark:text-gray-400">Status</th>
                      <th className="text-left py-3 px-4 text-sm font-medium text-gray-500 dark:text-gray-400">Amount</th>
                    </tr>
                  </thead>
                  <tbody>
                    {sortedSalesOrders.map((order) => (
                      <tr key={order.id} className="border-b border-gray-100 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
                        <td className="py-3 px-4 text-gray-900 dark:text-white">{order.channel}</td>
                        <td className="py-3 px-4 text-gray-900 dark:text-white">{order.orderId}</td>
                        <td className="py-3 px-4">
                          <span className={`px-3 py-1 rounded-full text-xs font-medium ${
                            order.status === 'Completed' ? 'bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200' :
                            order.status === 'Processing' ? 'bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-200' :
                            'bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-200'
                          }`}>
                            {order.status}
                          </span>
                        </td>
                        <td className="py-3 px-4 font-medium text-indigo-600 dark:text-indigo-400">${order.amount.toFixed(2)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}; 