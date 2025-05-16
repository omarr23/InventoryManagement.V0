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
        Task<ResultT<IEnumerable<InventoryProductReadDTO>>> GetAllAsync();

        Task<ResultT<InventoryProductReadDTO?>> GetByIdAsync(int inventoryId, int productId);

        Task<ResultT<bool>> AddAsync(CreateInventoryProductDTO dto, int inventoryId);

        Task<ResultT<bool>> UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto);

        Task<ResultT<bool>> DeleteAsync(int inventoryId, int productId);
    }
}
