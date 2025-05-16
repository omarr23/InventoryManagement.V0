using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.DTO.SupplierProductDTO
{
    public class SupplierPrpductDTO
    {
        public class SupplierProductReadDTO
        {
            public int SupplierId { get; set; }
            public int ProductId { get; set; }
            public decimal? DefaultCost { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string SupplierName { get; set; } // Added for better readability
            public string ProductName { get; set; } // Added for better readability
        }
        public class CreateSupplierProductDTO
        {
            [Required(ErrorMessage = "{0} is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public int SupplierId { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public int ProductId { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [Range(0, double.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public decimal? DefaultCost { get; set; }
        }
        public class UpdateSupplierProductDTO
        {
            [Required(ErrorMessage = "{0} is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public int SupplierId { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public int ProductId { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [Range(0, double.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public decimal? DefaultCost { get; set; }
        }




    }
}
