using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.SupplierRepository;
using InventoryManagement.BLL.DTO.SupplierDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;

namespace InventoryManagement.BLL.manager.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        // Get all suppliers
        public async Task<IEnumerable<SupplierReadDTO>> GetAllAsync()
        {
            var suppliers = await _repository.GetAllAsync();
            return suppliers.Select(SupplierMapper.MapToSupplierReadDto);
        }

        // Get supplier by ID
        public async Task<SupplierReadDTO?> GetByIdAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            return supplier == null ? null : SupplierMapper.MapToSupplierReadDto(supplier);
        }

        // Add a new supplier
        public async Task AddAsync(SupplierCreateDTO dto)
        {
            var supplier = SupplierMapper.MapToSupplier(dto); // Map DTO to entity
            await _repository.AddAsync(supplier);            // Add to database
            await _repository.SaveChangesAsync();           // Commit changes to DB
        }

        // Update an existing supplier
        public async Task UpdateAsync(int id, SupplierUpdateDTO dto)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");

            // Map the updated values to the existing Supplier object
            SupplierMapper.MapToExistingSupplier(dto, supplier);
            await _repository.SaveChangesAsync();
        }

        // Delete supplier by ID
        public async Task DeleteAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");

            _repository.Delete(supplier);
            await _repository.SaveChangesAsync();
        }
    }
}
