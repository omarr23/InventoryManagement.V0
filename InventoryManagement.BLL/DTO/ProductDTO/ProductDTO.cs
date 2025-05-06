using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.DTO.ProductDTO
{
    public class ProductDTO
    {
        //create product
        public class ProductCreatDTO
        {
            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string Name { get; set; } = string.Empty;
            [Required]
            [StringLength(50)]
            public string Sku { get; set; } = string.Empty;

            [StringLength(500)]
            public string Description { get; set; } = string.Empty;
            [Required]
            [Range(1, 1000000.00)]
            public decimal Price { get; set; }




        }
        //read product
        public class ProductReadDTO
        {
            public int ProductId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Sku { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
        //update product 
        public class ProductUpdateDTO
        {
            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string Name { get; set; } = string.Empty;
            [Required]
            [StringLength(50)]
            public string Sku { get; set; } = string.Empty;
            [StringLength(500)]

            public string Description { get; set; } = string.Empty;
            [Required]
            [Range(1, 1000000.00)]
            public decimal Price { get; set; }

        }

    }
}
