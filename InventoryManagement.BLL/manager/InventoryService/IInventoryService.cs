using InventoryManagement.BLL.DTO.InventoryDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;

namespace InventoryManagement.BLL.manager.InventoryService
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO.InventoryReadDTO>> GetAllAsync();
        Task<InventoryDTO.InventoryReadDTO?> GetByIdAsync(int id);
        Task<InventoryDTO.InventoryReadDTO> AddAsync(InventoryDTO.CreateInventoryDTO dto, string userId);
        Task UpdateAsync(int id, InventoryDTO.UpdateInventoryDTO dto);
        Task DeleteAsync(int id);

        //to add own services
    }
}
