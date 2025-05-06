using InventoryManagement.BLL.manager.services;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Repository.SupplierProductRepository;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.BLL.DTO.SupplierProductDTO.SupplierPrpductDTO;

namespace InventoryManagement.BLL.manager.SupplierProductService
{
    public class SupplierProductService : ISupplierProductService
    {
        private readonly ISupplierProductRepository _repository;

        public SupplierProductService(ISupplierProductRepository repository)
        {
            _repository = repository;
        }

        // Add new SupplierProduct
        public async Task AddSupplierProductAsync(CreateSupplierProductDTO dto)
        {
            // Check if the SupplierProduct already exists to prevent duplicates
            var existing = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
            if (existing != null)
                throw new InvalidOperationException("This SupplierProduct relation already exists.");

            // Map DTO to entity and add it
            var supplierProduct = SupplierProductMapper.MapToSupplierProductDto(dto);
            await _repository.AddSupplierProductAsync(supplierProduct);
        }

        // Get SupplierProduct by SupplierId and ProductId
        public async Task<SupplierProductReadDTO> GetSupplierProductByIdAsync(int supplierId, int productId)
        {
            var supplierProduct = await _repository.GetSupplierProductAsync(supplierId, productId);
            if (supplierProduct == null)
                return null;

            // Map the entity to DTO
            return SupplierProductMapper.MapToSupplierProductReadDto(supplierProduct);
        }

        // Update existing SupplierProduct
        public async Task UpdateSupplierProductAsync(UpdateSupplierProductDTO dto)
        {
            var existingSupplierProduct = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
            if (existingSupplierProduct == null)
                throw new InvalidOperationException("The SupplierProduct to update does not exist.");

            // Map the DTO to update the existing entity
            SupplierProductMapper.MapToExistingSupplierProduct(dto, existingSupplierProduct);

            // Update the entity
            await _repository.UpdateSupplierProductAsync(existingSupplierProduct);
        }

        // Delete SupplierProduct by SupplierId and ProductId
        public async Task DeleteSupplierProductAsync(int supplierId, int productId)
        {
            var existingSupplierProduct = await _repository.GetSupplierProductAsync(supplierId, productId);
            if (existingSupplierProduct == null)
                throw new InvalidOperationException("The SupplierProduct to delete does not exist.");

            // Delete the entity
            await _repository.DeleteSupplierProductAsync(supplierId, productId);
        }
    }
}
