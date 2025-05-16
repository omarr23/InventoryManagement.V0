using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.DTO.ProductDTO
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

    public class ProductDTO
    {
        //create product
        public class ProductCreatDTO
        {
            [Required (ErrorMessage = "{0} Is Required")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string Name { get; set; } = string.Empty;
            [Required (ErrorMessage = "{0} Is Required")]
            [StringLength(50, ErrorMessage = "{0} must be less than {1} character")]
            public string Sku { get; set; } = string.Empty;

            [StringLength(500, ErrorMessage = "{0} must be less than {1} character")]
            public string Description { get; set; } = string.Empty;
            [Required(ErrorMessage = "{0} Is Required")]
            [Range(1, 1000000.00, ErrorMessage = "{0} must be between {1} and {2}.")]
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
            public bool IsDeleted { get; set; }
            public DateTime? DeletedAt { get; set; }
        }

        //update product 
        public class ProductUpdateDTO
        {
            [Required(ErrorMessage = "{0} Is Required")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string Name { get; set; } = string.Empty;
            [Required(ErrorMessage = "{0} Is Required")]
            [StringLength(50, ErrorMessage = "{0} must be less than {1} character")]
            public string Sku { get; set; } = string.Empty;
            [StringLength(500, ErrorMessage = "{0} must be less than {1} character")]
            public string Description { get; set; } = string.Empty;
            [Required(ErrorMessage = "{0} Is Required")]
            [Range(1, 1000000.00, ErrorMessage = "{0} must be between {1} and {2}.")]
            public decimal Price { get; set; }
        }
    }
}
