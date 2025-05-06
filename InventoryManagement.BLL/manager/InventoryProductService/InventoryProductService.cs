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
        public async Task<IEnumerable<InventoryProductReadDTO>> GetAllAsync()
        {
            var inventoryProducts = await _repository.GetAllAsync();
            return inventoryProducts.Select(InventoryProductMapper.MapToInventoryProductReadDto);
        }

        // Get InventoryProduct by ID
        public async Task<InventoryProductReadDTO?> GetByIdAsync(int inventoryId, int productId)
        {
            var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
            return inventoryProduct == null ? null : InventoryProductMapper.MapToInventoryProductReadDto(inventoryProduct);
        }

        // Add a new InventoryProduct
        public async Task AddAsync(CreateInventoryProductDTO dto)
        {
            // Validation
            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be a positive value.");

            // Map DTO to entity
            var inventoryProduct = InventoryProductMapper.MapToInventoryProduct(dto);
            await _repository.AddAsync(inventoryProduct);
            await _repository.SaveChangesAsync(); // Save changes to the database
        }

        // Update an existing InventoryProduct
        public async Task UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto)
        {
            // Validation
            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be a positive value.");

            var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
            if (inventoryProduct == null)
                throw new KeyNotFoundException($"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.");

            // Map DTO to existing entity
            InventoryProductMapper.MapToExistingInventoryProduct(dto, inventoryProduct);
            await _repository.SaveChangesAsync(); // Save changes to the database
        }

        // Delete an InventoryProduct
        public async Task DeleteAsync(int inventoryId, int productId)
        {
            var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
            if (inventoryProduct == null)
                throw new KeyNotFoundException($"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.");

            // Delete the entity
            _repository.Delete(inventoryProduct);
            await _repository.SaveChangesAsync(); // Save changes to the database
        }
    }
}