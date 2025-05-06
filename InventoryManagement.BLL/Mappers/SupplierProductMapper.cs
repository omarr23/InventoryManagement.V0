
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierProductDTO.SupplierPrpductDTO;

namespace InventoryManagement.BLL.Mappers
{
    public class SupplierProductMapper
    {
        // Convert Create DTO to SupplierProduct entity
        public static SupplierProduct MapToSupplierProductDto(CreateSupplierProductDTO dto)
        {
            if (dto == null) return null;

            return new SupplierProduct
            {
                SupplierId = dto.SupplierId,
                ProductId = dto.ProductId,
                DefaultCost = dto.DefaultCost,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        // Update existing SupplierProduct entity with Update DTO
        public static void MapToExistingSupplierProduct(UpdateSupplierProductDTO dto, SupplierProduct supplierProduct)
        {
            if (dto == null || supplierProduct == null) return;

            supplierProduct.DefaultCost = dto.DefaultCost;
            supplierProduct.UpdatedAt = DateTime.UtcNow;
        }
        // Convert SupplierProduct entity to SupplierProductRead DTO
        public static SupplierProductReadDTO MapToSupplierProductReadDto(SupplierProduct supplierProduct)
        {
            if (supplierProduct == null) return null;

            return new SupplierProductReadDTO
            {
                SupplierId = supplierProduct.SupplierId,
                ProductId = supplierProduct.ProductId,
                DefaultCost = supplierProduct.DefaultCost,
                CreatedAt = supplierProduct.CreatedAt,
                UpdatedAt = supplierProduct.UpdatedAt,
                SupplierName = supplierProduct.Supplier?.Name, // Assuming Supplier entity has Name property
                ProductName = supplierProduct.Product?.Name // Assuming Product entity has Name property
            };
        }
    }
}
