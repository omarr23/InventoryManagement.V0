using InventoryManagement.BLL.DTO.InventoryProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Mappers
{
    
    public static class InventoryProductMapper
    {
        // Map CreateInventoryProductDTO to InventoryProduct entity
        public static InventoryProduct MapToInventoryProduct(CreateInventoryProductDTO createDto, int inventoryId)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            return new InventoryProduct
            {
                InventoryId = inventoryId,
                ProductId = createDto.ProductId,
                Quantity = createDto.Quantity,
                CreatedAt = DateTime.Now, // Set the creation date
                UpdatedAt = DateTime.Now // Set the updated date
            };
        }

        // Map UpdateInventoryProductDTO to existing InventoryProduct entity
        public static void MapToExistingInventoryProduct(UpdateInventoryProductDTO updateDto, InventoryProduct existingInventoryProduct)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));
            if (existingInventoryProduct == null) throw new ArgumentNullException(nameof(existingInventoryProduct));

            existingInventoryProduct.Quantity = updateDto.Quantity; // Update quantity
            existingInventoryProduct.UpdatedAt = DateTime.Now; // Update the updated date
        }

        // Map InventoryProduct entity to InventoryProductReadDTO
        public static InventoryProductReadDTO MapToInventoryProductReadDto(InventoryProduct inventoryProduct)
        {
            if (inventoryProduct == null) throw new ArgumentNullException(nameof(inventoryProduct));

            return new InventoryProductReadDTO
            {
                InventoryId = inventoryProduct.InventoryId,
                ProductId = inventoryProduct.ProductId,
                Quantity = inventoryProduct.Quantity,
                CreatedAt = inventoryProduct.CreatedAt,
                UpdatedAt = inventoryProduct.UpdatedAt,
                ProductName = inventoryProduct.Product != null ? inventoryProduct.Product.Name : "Unknown" // Handle null Product
            };
        }
    }
}

