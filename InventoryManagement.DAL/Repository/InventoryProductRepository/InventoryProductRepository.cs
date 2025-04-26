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
    }
}
