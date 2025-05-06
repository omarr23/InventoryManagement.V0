using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.DAL.Interfaces;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierProductDTO.SupplierPrpductDTO;

namespace InventoryManagement.BLL.manager.SupplierProductService
{
    public interface ISupplierProductService
    {
        // Add a new SupplierProduct
        Task AddSupplierProductAsync(CreateSupplierProductDTO dto);

        // Get SupplierProduct by SupplierId and ProductId
        Task<SupplierProductReadDTO> GetSupplierProductByIdAsync(int supplierId, int productId);

        // Update an existing SupplierProduct
        Task UpdateSupplierProductAsync(UpdateSupplierProductDTO dto);

        // Delete SupplierProduct by SupplierId and ProductId
        Task DeleteSupplierProductAsync(int supplierId, int productId);
    }
}
