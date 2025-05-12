using InventoryManagement.BLL.DTO.InventoryProductDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.Helper;

namespace InventoryManagement.BLL.manager.InventoryProductService
{
    public interface IInventoryProductService
    {
        // Get all InventoryProducts
        Task<Result<IEnumerable<InventoryProductReadDTO>>> GetAllAsync();

        Task<Result<InventoryProductReadDTO?>> GetByIdAsync(int inventoryId, int productId);

        Task<Result<bool>> AddAsync(CreateInventoryProductDTO dto, int inventoryId);

        Task<Result<bool>> UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto);

        Task<Result<bool>> DeleteAsync(int inventoryId, int productId);
    }
}
