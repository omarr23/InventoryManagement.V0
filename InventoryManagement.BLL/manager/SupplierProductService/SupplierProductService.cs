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
using InventoryManagement.BLL.Helper;

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
        public async Task<Result<bool>> AddSupplierProductAsync(CreateSupplierProductDTO dto)
        {
            try
            {
                // Check if the SupplierProduct already exists to prevent duplicates
                var existing = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
                if (existing != null)
                    return Result<bool>.Failure("This SupplierProduct relation already exists.");

                // Map DTO to entity and add it
                var supplierProduct = SupplierProductMapper.MapToSupplierProductDto(dto);
                await _repository.AddSupplierProductAsync(supplierProduct);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while adding the SupplierProduct: {ex.Message}");
            }
        }

        // Get SupplierProduct by SupplierId and ProductId
        public async Task<Result<SupplierProductReadDTO>> GetSupplierProductByIdAsync(int supplierId, int productId)
        {
            try
            {
                var supplierProduct = await _repository.GetSupplierProductAsync(supplierId, productId);
                if (supplierProduct == null)
                    return Result<SupplierProductReadDTO>.Failure("The SupplierProduct relation was not found.");

                return Result<SupplierProductReadDTO>.Success(SupplierProductMapper.MapToSupplierProductReadDto(supplierProduct));
            }
            catch (Exception ex)
            {
                return Result<SupplierProductReadDTO>.Failure($"An error occurred while retrieving the SupplierProduct: {ex.Message}");
            }
        }

        // Update existing SupplierProduct
        public async Task<Result<bool>> UpdateSupplierProductAsync(UpdateSupplierProductDTO dto)
        {
            try
            {
                var existingSupplierProduct = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
                if (existingSupplierProduct == null)
                    return Result<bool>.Failure("The SupplierProduct to update does not exist.");

                // Map the DTO to update the existing entity
                SupplierProductMapper.MapToExistingSupplierProduct(dto, existingSupplierProduct);

                // Update the entity
                await _repository.UpdateSupplierProductAsync(existingSupplierProduct);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while updating the SupplierProduct: {ex.Message}");
            }
        }

        // Delete SupplierProduct by SupplierId and ProductId
        public async Task<Result<bool>> DeleteSupplierProductAsync(int supplierId, int productId)
        {
            try
            {
                var existingSupplierProduct = await _repository.GetSupplierProductAsync(supplierId, productId);
                if (existingSupplierProduct == null)
                    return Result<bool>.Failure("The SupplierProduct to delete does not exist.");

                // Delete the entity
                await _repository.DeleteSupplierProductAsync(supplierId, productId);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while deleting the SupplierProduct: {ex.Message}");
            }
        }
    }
}