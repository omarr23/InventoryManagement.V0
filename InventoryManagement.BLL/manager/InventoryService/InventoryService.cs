using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.InventoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.InventoryDTO;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.BLL.Helper;
using InventoryManagement.BLL.DTO.InventoryProductDTO;
using InventoryManagement.BLL.Helper;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;

namespace InventoryManagement.BLL.manager.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        // Get all Inventories with Inventory Products
        public async Task<Result<IEnumerable<InventoryDTO.InventoryReadDTO>>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();
            var result = inventories.Select(inventory => InventoryMapper.MapToInventoryReadDto(inventory));
            return Result<IEnumerable<InventoryReadDTO>>.Success(result);
        }

        // Get Inventory by ID with Inventory Products
        public async Task<Result<InventoryDTO.InventoryReadDTO?>> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                return Result<InventoryReadDTO?>.Failure("Inventory not found.");

            var dto = InventoryMapper.MapToInventoryReadDto(inventory);
            return Result<InventoryReadDTO?>.Success(dto); // Map entity to DTO with InventoryProducts
        }

        // Add a new Inventory with Inventory Products
        public async Task<Result<InventoryDTO.InventoryReadDTO>> AddAsync(InventoryDTO.CreateInventoryDTO dto, string userId)
        {
            try
            {
                var inventory = InventoryMapper.MapToInventory(dto, userId);

                await _repository.AddAsync(inventory);
                await _repository.SaveChangesAsync();

                var inventoryProducts = dto.InventoryProducts.Select(ip =>
                    InventoryProductMapper.MapToInventoryProduct(ip, inventory.InventoryId)).ToList();

                inventory.InventoryProducts = inventoryProducts;
                await _repository.SaveChangesAsync();

                var reloaded = await _repository.GetByIdAsync(inventory.InventoryId);
                if (reloaded == null)
                    return Result<InventoryReadDTO>.Failure("Failed to reload inventory after creation.");

                var resultDto = InventoryMapper.MapToInventoryReadDto(reloaded);
                return Result<InventoryReadDTO>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<InventoryReadDTO>.Failure("Error creating inventory: " + ex.Message);
            }
        }

            // Update an existing Inventory with Inventory Products
            public async Task<Result<bool>> UpdateAsync(int id, InventoryDTO.UpdateInventoryDTO dto)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                return Result<bool>.Failure($"Inventory with ID {id} not found.");

            InventoryMapper.MapToExistingInventory(dto, inventory);

            inventory.InventoryProducts.Clear();

            var inventoryProducts = dto.InventoryProducts.Select(ip => new InventoryProduct
            {
                ProductId = ip.ProductId,
                Quantity = ip.Quantity
            }).ToList();

            foreach (var ip in inventoryProducts)
            {
                inventory.InventoryProducts.Add(ip);
            }

            await _repository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        // Delete an Inventory with its Inventory Products
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                return Result<bool>.Failure($"Inventory with ID {id} not found.");

            _repository.Delete(inventory);
            await _repository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}