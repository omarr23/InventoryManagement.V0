using InventoryManagement.DAL.Repositories;
using InventoryManagement.DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DAL.Repository.SupplierProductRepository
{
    public class SupplierProductRepository : GenericRepository<SupplierProduct>, ISupplierProductRepository
    {
        public SupplierProductRepository(InventoryDbContext context) : base(context)
        {
        }
        // Get SupplierProduct by SupplierId and ProductId with related Supplier and Product entities
        public async Task<SupplierProduct> GetSupplierProductAsync(int supplierId, int productId)
        {
            return await _context.SupplierProducts
                .Include(sp => sp.Supplier)  // Load related Supplier
                .Include(sp => sp.Product)   // Load related Product
                .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);
        }

        // Add a new SupplierProduct to the database
        public async Task AddSupplierProductAsync(SupplierProduct supplierProduct)
        {
            if (supplierProduct == null)
                throw new ArgumentNullException(nameof(supplierProduct));

            await _context.SupplierProducts.AddAsync(supplierProduct);
            await _context.SaveChangesAsync();
        }

        // Update existing SupplierProduct in the database
        public async Task UpdateSupplierProductAsync(SupplierProduct supplierProduct)
        {
            _context.SupplierProducts.Update(supplierProduct);
            await _context.SaveChangesAsync();
        }

        // Delete SupplierProduct by SupplierId and ProductId
        public async Task DeleteSupplierProductAsync(int supplierId, int productId)
        {
            var supplierProduct = await GetSupplierProductAsync(supplierId, productId);
            if (supplierProduct != null)
            {
                _context.SupplierProducts.Remove(supplierProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
