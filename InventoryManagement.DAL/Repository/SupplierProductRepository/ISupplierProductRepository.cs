using InventoryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.SupplierProductRepository
{
    public interface ISupplierProductRepository : IGenericRepository<SupplierProduct>
    {
        // Method to add a new SupplierProduct
        Task AddSupplierProductAsync(SupplierProduct supplierProduct);

        // Method to get a SupplierProduct by SupplierId and ProductId
        Task<SupplierProduct> GetSupplierProductAsync(int supplierId, int productId);

        // Method to update an existing SupplierProduct
        Task UpdateSupplierProductAsync(SupplierProduct supplierProduct);

        // Method to delete a SupplierProduct by SupplierId and ProductId
        Task DeleteSupplierProductAsync(int supplierId, int productId);
    }
    // Add any custom methods later if needed
}

