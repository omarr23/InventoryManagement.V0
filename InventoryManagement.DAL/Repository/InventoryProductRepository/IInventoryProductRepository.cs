using InventoryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.InventoryProductRepository
{
    public interface IInventoryProductRepository : IGenericRepository<InventoryProduct>
    {
        // Custom operation specific to InventoryProduct
        Task<InventoryProduct?> GetInventoryProductByProductIdAsync(int productId);
    }
}
