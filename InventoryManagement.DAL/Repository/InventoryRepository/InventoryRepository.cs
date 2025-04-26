using InventoryManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Interfaces;


namespace InventoryManagement.DAL.Repository.InventoryRepository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(InventoryDbContext context) : base(context)
        {
        }

        // Here can add Special Inventory Services  
    }
}
