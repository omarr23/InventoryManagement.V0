using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.InventoryProductDTO;


namespace InventoryManagement.BLL.DTO.InventoryDTO
{
    public class InventoryDTO
    {
        //to read
        public class InventoryReadDTO
        {
            public int InventoryId { get; set; }
            public string OwnerType { get; set; }
            public string OwnerId { get; set; }
            public string Name { get; set; }
            public bool IsPublic { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public List<InventoryProductReadDTO> InventoryProducts { get; set; } = new();

        }
        //CREATE
        public class CreateInventoryDTO
        {
            [Required(ErrorMessage = "{0} is required.")]
            [RegularExpression("^(USER|COMPANY)$", ErrorMessage = "{0} must be either 'USER' or 'COMPANY'.")]
            public string OwnerType { get; set; } = string.Empty;

            [Required(ErrorMessage = "{0} is required.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string Name { get; set; } = string.Empty;

            public bool IsPublic { get; set; } = false;

            public List<CreateInventoryProductDTO> InventoryProducts { get; set; } = new();
        }


        //UPDATE
        public class UpdateInventoryDTO
        {
            [Required(ErrorMessage = "{0} is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number.")]
            public int InventoryId { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [RegularExpression("^(USER|COMPANY)$", ErrorMessage = "{0} must be either 'USER' or 'COMPANY'.")]
            public string OwnerType { get; set; } = string.Empty;

            [Required(ErrorMessage = "{0} is required.")]
            public string OwnerId { get; set; } = string.Empty;

            [Required(ErrorMessage = "{0} is required.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string Name { get; set; } = string.Empty;

            public bool IsPublic { get; set; } = false;

            public List<UpdateInventoryProductDTO> InventoryProducts { get; set; } = new();

        }

    }
}