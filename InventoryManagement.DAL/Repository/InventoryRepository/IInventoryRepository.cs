using InventoryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.InventoryRepository
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        // Here can add Special Inventory Services 
    }
}
