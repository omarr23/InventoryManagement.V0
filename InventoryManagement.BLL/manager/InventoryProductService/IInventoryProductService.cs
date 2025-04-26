using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.InventoryProductService
{
    public interface IInventoryProductService : IGenericService<InventoryProduct>
    {
        // Custom service operation to get InventoryProduct by ProductId
        Task<InventoryProduct?> GetInventoryProductByProductIdAsync(int productId);
    }
}
