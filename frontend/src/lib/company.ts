import { API_URL } from './auth';

export interface Company {
  id: string;
  name: string;
  address: string;
  email: string;
  phone: string;
  description: string;
}

// Store companies in localStorage for demo purposes
const STORAGE_KEY = 'companies';

export const companyService = {
  async createCompany(companyData: Omit<Company, 'id'>): Promise<Company> {
    try {
      const newCompany: Company = {
        id: Math.random().toString(36).substr(2, 9),
        ...companyData
      };
      
      // Get existing companies
      const existingCompanies = this.getCompanies();
      
      // Add new company
      const updatedCompanies = [...existingCompanies, newCompany];
      
      // Save to localStorage
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updatedCompanies));
      
      // Simulate API delay
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      return newCompany;
    } catch (error) {
      console.error('Create company error:', error);
      throw new Error('Failed to create company. Please try again later.');
    }
  },

  getCompanies(): Company[] {
    const companies = localStorage.getItem(STORAGE_KEY);
    return companies ? JSON.parse(companies) : [];
  },

  async deleteCompany(id: string): Promise<void> {
    try {
      const companies = this.getCompanies();
      const updatedCompanies = companies.filter(company => company.id !== id);
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updatedCompanies));
      
      // Simulate API delay
      await new Promise(resolve => setTimeout(resolve, 500));
    } catch (error) {
      console.error('Delete company error:', error);
      throw new Error('Failed to delete company. Please try again later.');
    }
  },

  // You can add more methods here (getCompanies, updateCompany, etc.)
}; 