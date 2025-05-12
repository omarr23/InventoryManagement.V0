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
        Task<ResultT<SupplierReadDTO>> AddAsync(SupplierCreateDTO dto);
        Task<ResultT<IEnumerable<SupplierReadDTO>>> GetAllAsync();
        Task<ResultT<SupplierReadDTO?>> GetByIdAsync(int id);
        Task<ResultT<bool>> UpdateAsync(int id, SupplierUpdateDTO dto);
        Task<ResultT<bool>> DeleteAsync(int id);

        // Add here any Supplier-specific business logic if needed

    }

}
