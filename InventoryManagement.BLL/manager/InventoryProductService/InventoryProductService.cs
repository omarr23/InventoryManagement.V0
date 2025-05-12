using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.InventoryProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.DTO.InventoryProductDTO;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.BLL.Helper;


namespace InventoryManagement.BLL.manager.InventoryProductService
{
    public class InventoryProductService : IInventoryProductService
    {
        private readonly IInventoryProductRepository _repository;

        public InventoryProductService(IInventoryProductRepository repository)
        {
            _repository = repository;
        }

        // Get all InventoryProducts
        public async Task<Result<IEnumerable<InventoryProductReadDTO>>> GetAllAsync()
        {
            try
            {
                var inventoryProducts = await _repository.GetAllAsync();
                var result = inventoryProducts.Select(InventoryProductMapper.MapToInventoryProductReadDto);
                return Result<IEnumerable<InventoryProductReadDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<InventoryProductReadDTO>>.Failure($"Error retrieving InventoryProducts: {ex.Message}");
            }
        }

        // Get InventoryProduct by ID
        public async Task<Result<InventoryProductReadDTO?>> GetByIdAsync(int inventoryId, int productId)
        {
            try
            {
                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                {
                    return Result<InventoryProductReadDTO?>.Failure($"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.");
                }
                return Result<InventoryProductReadDTO?>.Success(InventoryProductMapper.MapToInventoryProductReadDto(inventoryProduct));
            }
            catch (Exception ex)
            {
                return Result<InventoryProductReadDTO?>.Failure($"Error retrieving InventoryProduct: {ex.Message}");
            }
        }

        // Add a new InventoryProduct
        public async Task<Result<bool>> AddAsync(CreateInventoryProductDTO dto, int inventoryId)
        {
            try
            {
                // Validation
                if (dto.Quantity <= 0)
                    return Result<bool>.Failure("Quantity must be a positive value.");

                // Map DTO to entity
                var inventoryProduct = InventoryProductMapper.MapToInventoryProduct(dto, inventoryId);
                await _repository.AddAsync(inventoryProduct);
                await _repository.SaveChangesAsync(); // Save changes to the database
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error adding InventoryProduct: {ex.Message}");
            }
        }

        // Update an existing InventoryProduct
        public async Task<Result<bool>> UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto)
        {
            try
            {
                // Validation
                if (dto.Quantity <= 0)
                    return Result<bool>.Failure("Quantity must be a positive value.");

                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                    return Result<bool>.Failure($"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.");

                // Map DTO to existing entity
                InventoryProductMapper.MapToExistingInventoryProduct(dto, inventoryProduct);
                await _repository.SaveChangesAsync(); // Save changes to the database
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating InventoryProduct: {ex.Message}");
            }
        }

        // Delete an InventoryProduct
        public async Task<Result<bool>> DeleteAsync(int inventoryId, int productId)
        {
            try
            {
                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                    return Result<bool>.Failure($"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.");

                // Delete the entity
                _repository.Delete(inventoryProduct);
                await _repository.SaveChangesAsync(); // Save changes to the database
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting InventoryProduct: {ex.Message}");
            }
        }
    }
}