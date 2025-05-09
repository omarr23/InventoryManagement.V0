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
            [Required(ErrorMessage = "OwnerType is required.")]
            [RegularExpression("^(USER|COMPANY)$", ErrorMessage = "OwnerType must be either 'USER' or 'COMPANY'.")]
            public string OwnerType { get; set; } = string.Empty;

            

            [Required(ErrorMessage = "Name is required.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
            public string Name { get; set; } = string.Empty;

            public bool IsPublic { get; set; } = false;

            public List<CreateInventoryProductDTO> InventoryProducts { get; set; } = new();

        }

        //UPDATE
        public class UpdateInventoryDTO
        {
            [Required(ErrorMessage = "InventoryId is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "InventoryId must be a positive number.")]
            public int InventoryId { get; set; }

            [Required(ErrorMessage = "OwnerType is required.")]
            [RegularExpression("^(USER|COMPANY)$", ErrorMessage = "OwnerType must be either 'USER' or 'COMPANY'.")]
            public string OwnerType { get; set; } = string.Empty;

            [Required(ErrorMessage = "OwnerId is required.")]
            public string OwnerId { get; set; } = string.Empty;

            [Required(ErrorMessage = "Name is required.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
            public string Name { get; set; } = string.Empty;

            public bool IsPublic { get; set; } = false;

            public List<UpdateInventoryProductDTO> InventoryProducts { get; set; } = new();

        }

    }
}