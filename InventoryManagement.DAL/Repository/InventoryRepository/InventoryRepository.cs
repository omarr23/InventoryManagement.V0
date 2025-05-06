using InventoryManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagement.DAL.Repository.InventoryRepository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(InventoryDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(i => i.InventoryProducts)
                .ThenInclude(ip => ip.Product)
                .ToListAsync();
        }
        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(i => i.InventoryProducts)
                .ThenInclude(ip => ip.Product)
                .FirstOrDefaultAsync(i => i.InventoryId == id);
        }



        // Here can add Special Inventory Services  
    }
}
