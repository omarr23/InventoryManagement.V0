using InventoryManagement.BLL.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Mappers
{ 
    public static class ProductMapper
    {
        //mapp product to productcreatedto
        public static Product MapToProduct(ProductDTO.ProductCreatDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Sku = dto.Sku,
                Description = dto.Description,
                Price = dto.Price
            };
        }
        //mapp to update
        public static void MapToExistingProduct(ProductDTO.ProductUpdateDTO dto, Product product)
        {
            product.Name = dto.Name;
            product.Sku = dto.Sku;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.UpdatedAt = DateTime.UtcNow;
        }
        // mapp to read
        public static ProductDTO.ProductReadDTO MapToProductReadDto(Product product)
        {
            return new ProductDTO.ProductReadDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt


            };
        }
    }
}