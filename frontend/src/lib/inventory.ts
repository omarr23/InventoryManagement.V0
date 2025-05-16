import { API_URL } from './auth';

export interface InventoryItem {
  id: string;
  name: string;
  sku: string;
  quantity: number;
  price: number;
  description: string;
}

// Store inventory items in localStorage for demo purposes
const STORAGE_KEY = 'inventory';

export const inventoryService = {
  async createInventory(inventoryData: Omit<InventoryItem, 'id'>): Promise<InventoryItem> {
    try {
      const newInventory: InventoryItem = {
        id: Math.random().toString(36).substr(2, 9),
        ...inventoryData
      };
      
      // Get existing inventory items
      const existingInventory = this.getInventory();
      
      // Add new inventory item
      const updatedInventory = [...existingInventory, newInventory];
      
      // Save to localStorage
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updatedInventory));
      
      // Simulate API delay
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      return newInventory;
    } catch (error) {
      console.error('Create inventory error:', error);
      throw new Error('Failed to create inventory item. Please try again later.');
    }
  },

  getInventory(): InventoryItem[] {
    const inventory = localStorage.getItem(STORAGE_KEY);
    return inventory ? JSON.parse(inventory) : [];
  },

  async deleteInventory(id: string): Promise<void> {
    try {
      const inventory = this.getInventory();
      const updatedInventory = inventory.filter(item => item.id !== id);
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updatedInventory));
      
      // Simulate API delay
      await new Promise(resolve => setTimeout(resolve, 500));
    } catch (error) {
      console.error('Delete inventory error:', error);
      throw new Error('Failed to delete inventory item. Please try again later.');
    }
  },

  // You can add more methods here (getInventory, updateInventory, etc.)
}; 