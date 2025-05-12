using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;
using InventoryManagement.BLL.Helper;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.SupplierRepository;


namespace InventoryManagement.BLL.manager.SupplierService
{
    public interface ISupplierService
    {
        Task<Result<SupplierReadDTO>> AddAsync(SupplierCreateDTO dto);
        Task<Result<IEnumerable<SupplierReadDTO>>> GetAllAsync();
        Task<Result<SupplierReadDTO?>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(int id, SupplierUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int id);

        // Add here any Supplier-specific business logic if needed

    }

}
