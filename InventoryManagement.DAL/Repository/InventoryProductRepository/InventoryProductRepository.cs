using InventoryManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.InventoryProductRepository
{
    public class InventoryProductRepository : GenericRepository<InventoryProduct>, IInventoryProductRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        // Get InventoryProduct by ProductId with related Inventory and Product details
        public async Task<InventoryProduct?> GetInventoryProductByProductIdAsync(int productId)
        {
            return await _context.InventoryProducts
                                 .Include(ip => ip.Inventory)
                                 .Include(ip => ip.Product)
                                 .FirstOrDefaultAsync(ip => ip.ProductId == productId);
        }
        // Method to get InventoryProduct by inventoryId and productId
        public async Task<InventoryProduct?> GetByIdAsync(int inventoryId, int productId)
        {
            // Fetching the InventoryProduct from the database using both inventoryId and productId
            return await _context.InventoryProducts
                         .Include(ip => ip.Product)
                         .FirstOrDefaultAsync(ip => ip.InventoryId == inventoryId && ip.ProductId == productId);
        }
        public async Task<IEnumerable<InventoryProduct>> GetAllAsync()
        {
            return await _context.InventoryProducts
                                 .Include(ip => ip.Product) 
                                 .ToListAsync();
        }
    }
}
