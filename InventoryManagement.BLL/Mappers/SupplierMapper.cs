using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;

namespace InventoryManagement.BLL.Mappers
{
    public static class SupplierMapper
    {
        // Map SupplierCreateDto to Supplier entity
        public static Supplier MapToSupplier(SupplierCreateDTO dto)
        {
            return new Supplier
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // Map SupplierUpdateDTO to existing Supplier (for updates)
        public static void MapToExistingSupplier(SupplierUpdateDTO dto, Supplier supplier)
        {
            supplier.Name = dto.Name;
            supplier.Address = dto.Address;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;
            supplier.UpdatedAt = DateTime.UtcNow;
        }

        // Map Supplier entity to SupplierReadDto
        public static SupplierReadDTO MapToSupplierReadDto(Supplier supplier)
        {
            return new SupplierReadDTO
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                CreatedAt = supplier.CreatedAt,
                UpdatedAt = supplier.UpdatedAt
            };
        }

    }
}
