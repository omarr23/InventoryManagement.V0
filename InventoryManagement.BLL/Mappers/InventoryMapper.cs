using InventoryManagement.BLL.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;
using InventoryManagement.BLL.DTO.InventoryDTO;
using InventoryManagement.BLL.DTO.InventoryProductDTO;


namespace InventoryManagement.BLL.Mappers
{
    public static class InventoryMapper
    {
        // Map from Inventory to InventoryReadDTO
        public static InventoryDTO.InventoryReadDTO MapToInventoryReadDto(Inventory inventory)
        {
            return new InventoryDTO.InventoryReadDTO
            {
                InventoryId = inventory.InventoryId,
                OwnerType = inventory.OwnerType,
                OwnerId = inventory.OwnerId,
                Name = inventory.Name,
                IsPublic = inventory.IsPublic,
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt,
                InventoryProducts = inventory.InventoryProducts.Select(ip => new InventoryProductReadDTO
                {
                    ProductId = ip.ProductId,
                    ProductName = ip.Product.Name, // You can map other fields from the Product as needed
                    Quantity = ip.Quantity
                }).ToList()
            };
        }

        // Map from CreateInventoryDTO to Inventory
        public static Inventory MapToInventory(CreateInventoryDTO createInventoryDto)
        {
            return new Inventory
            {
                OwnerType = createInventoryDto.OwnerType,
                OwnerId = createInventoryDto.OwnerId,
                Name = createInventoryDto.Name,
                IsPublic = createInventoryDto.IsPublic,
                InventoryProducts = createInventoryDto.InventoryProducts.Select(ip => new InventoryProduct
                {
                    ProductId = ip.ProductId,
                    Quantity = ip.Quantity
                }).ToList()
            };
        }

        // Map from UpdateInventoryDTO to Inventory (existing)
        public static void MapToExistingInventory(UpdateInventoryDTO updateInventoryDto, Inventory inventory)
        {
            inventory.OwnerType = updateInventoryDto.OwnerType;
            inventory.OwnerId = updateInventoryDto.OwnerId;
            inventory.Name = updateInventoryDto.Name;
            inventory.IsPublic = updateInventoryDto.IsPublic;
        }
    }

}
