using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;

namespace InventoryManagement.BLL.manager.SupplierService
{
    public interface ISupplierService
    {
        Task AddAsync(SupplierCreateDTO dto);
        Task<IEnumerable<SupplierReadDTO>> GetAllAsync();
        Task<SupplierReadDTO?> GetByIdAsync(int id);
        Task UpdateAsync(int id, SupplierUpdateDTO dto);
        Task DeleteAsync(int id);

        // Add here any Supplier-specific business logic if needed

    }

}
