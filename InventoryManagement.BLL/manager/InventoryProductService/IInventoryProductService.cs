using InventoryManagement.BLL.DTO.InventoryProductDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.InventoryProductService
{
    public interface IInventoryProductService
    {
        // Get all InventoryProducts
        Task<IEnumerable<InventoryProductReadDTO>> GetAllAsync();

        Task<InventoryProductReadDTO?> GetByIdAsync(int inventoryId, int productId);

        Task AddAsync(CreateInventoryProductDTO dto);

        Task UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto);

        Task DeleteAsync(int inventoryId, int productId);
    }
}
