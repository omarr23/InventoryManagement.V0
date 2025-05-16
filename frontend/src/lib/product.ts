import { API_URL } from './auth';

export interface Product {
  id: string;
  name: string;
  sku: string;
  price: number;
  description: string;
}

export const productService = {
  async createProduct(productData: Omit<Product, 'id'>): Promise<Product> {
    try {
      // For demo purposes, we'll simulate a successful product creation
      const newProduct: Product = {
        id: Math.random().toString(36).substr(2, 9),
        ...productData
      };
      
      // Simulate API delay
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      return newProduct;
    } catch (error) {
      console.error('Create product error:', error);
      throw new Error('Failed to create product. Please try again later.');
    }
  },
  // You can add more methods here (getProducts, updateProduct, etc.)
}; 