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
        Task<ResultT<IEnumerable<InventoryReadDTO>>> GetAllAsync();
        Task<ResultT<InventoryReadDTO?>> GetByIdAsync(int id);
        Task<ResultT<InventoryReadDTO>> AddAsync(CreateInventoryDTO dto, string userId);
        Task<ResultT<bool>> UpdateAsync(int id, UpdateInventoryDTO dto);
        Task<ResultT<bool>> DeleteAsync(int id);

        //to add own services
    }
}
