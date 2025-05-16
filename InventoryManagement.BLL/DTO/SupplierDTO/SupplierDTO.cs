using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.DTO.SupplierDTO
{
    public class SupplierDTO
    {
        public class SupplierReadDTO
        {
            public int SupplierId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
        public class SupplierCreateDTO
        {
            [Required(ErrorMessage = "{0} Is Required")]
            [StringLength(100, ErrorMessage = "{0} must be less than {1} character")]
            public string Name { get; set; } = string.Empty;

            [StringLength(200, ErrorMessage = "{0} must be less than {1} character")]
            public string Address { get; set; } = string.Empty;

            [Phone]
            public string Phone { get; set; } = string.Empty;

            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        }

        public class SupplierUpdateDTO
        {
            [Required(ErrorMessage = "{0} Is Required")]
            [StringLength(100, ErrorMessage = "{0} must be less than {1} character")]
            public string Name { get; set; } = string.Empty;

            [StringLength(200, ErrorMessage = "{0} must be less than {1} character")]
            public string Address { get; set; } = string.Empty;

            [Phone]
            public string Phone { get; set; } = string.Empty;

            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required(ErrorMessage = "{0} Is Required")]
            public int SupplierId { get; set; }  // Add SupplierId for updates
        }
    }
}
