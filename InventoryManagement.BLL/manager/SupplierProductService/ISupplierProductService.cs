using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.DAL.Interfaces;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierProductDTO.SupplierPrpductDTO;
using InventoryManagement.BLL.Helper;

namespace InventoryManagement.BLL.manager.SupplierProductService
{
    public interface ISupplierProductService
    {
        // Add a new SupplierProduct
        Task<ResultT<bool>> AddSupplierProductAsync(CreateSupplierProductDTO dto);

        // Get SupplierProduct by SupplierId and ProductId
        Task<ResultT<SupplierProductReadDTO>> GetSupplierProductByIdAsync(int supplierId, int productId);

        // Update an existing SupplierProduct
        Task<ResultT<bool>> UpdateSupplierProductAsync(UpdateSupplierProductDTO dto);

        // Delete SupplierProduct by SupplierId and ProductId
        Task<ResultT<bool>> DeleteSupplierProductAsync(int supplierId, int productId);
    }
}
