import axios from 'axios';
import { API_URL } from './auth';

export interface DashboardMetrics {
  toBePacked: number;
  toBeShipped: number;
  toBeDelivered: number;
  toBeInvoiced: number;
  quantityInHand: number;
  quantityToBeReceived: number;
}

export interface ProductDetails {
  lowStockItems: number;
  allItemGroups: number;
  allItems: number;
  unconfirmedItems: number;
  stockPercentage: number;
}

export interface TopSellingItem {
  name: string;
  quantity: number;
  unit: string;
  color: string;
}

export interface SalesOrder {
  channel: string;
  draft: number;
  confirmed: number;
  packed: number;
  shipped: number;
}

export interface PurchaseOrder {
  quantityOrdered: number;
  period: string;
}

export const dashboardService = {
  async getMetrics(): Promise<DashboardMetrics> {
    const response = await axios.get(`${API_URL}/dashboard/metrics`);
    return response.data;
  },

  async getProductDetails(): Promise<ProductDetails> {
    const response = await axios.get(`${API_URL}/dashboard/product-details`);
    return response.data;
  },

  async getTopSellingItems(): Promise<TopSellingItem[]> {
    const response = await axios.get(`${API_URL}/dashboard/top-selling-items`);
    return response.data;
  },

  async getSalesOrders(): Promise<SalesOrder[]> {
    const response = await axios.get(`${API_URL}/dashboard/sales-orders`);
    return response.data;
  },

  async getPurchaseOrders(): Promise<PurchaseOrder> {
    const response = await axios.get(`${API_URL}/dashboard/purchase-orders`);
    return response.data;
  }
}; 