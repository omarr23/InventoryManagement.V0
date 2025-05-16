using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.InventoryDTO;
namespace InventoryManagement.BLL.DTO.InventoryProductDTO
{
    public class InventoryProductReadDTO
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ProductName { get; set; } // Added for better readability
    }

    public class CreateInventoryProductDTO
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
        public int Quantity { get; set; }
    }

    public class UpdateInventoryProductDTO
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
        public int Quantity { get; set; }
    }
}

