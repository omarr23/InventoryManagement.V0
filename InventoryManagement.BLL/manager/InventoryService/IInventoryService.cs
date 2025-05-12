using InventoryManagement.BLL.DTO.InventoryDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;
using InventoryManagement.BLL.Helper;

namespace InventoryManagement.BLL.manager.InventoryService
{
    public interface IInventoryService
    {
        Task<Result<IEnumerable<InventoryReadDTO>>> GetAllAsync();
        Task<Result<InventoryReadDTO?>> GetByIdAsync(int id);
        Task<Result<InventoryReadDTO>> AddAsync(CreateInventoryDTO dto, string userId);
        Task<Result<bool>> UpdateAsync(int id, UpdateInventoryDTO dto);
        Task<Result<bool>> DeleteAsync(int id);

        //to add own services
    }
}
