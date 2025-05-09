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
        public async Task<IEnumerable<InventoryDTO.InventoryReadDTO>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();
            return inventories.Select(inventory =>
                InventoryMapper.MapToInventoryReadDto(inventory)); // Map entities to DTOs with InventoryProducts
        }

        // Get Inventory by ID with Inventory Products
        public async Task<InventoryDTO.InventoryReadDTO?> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                return null;

            return InventoryMapper.MapToInventoryReadDto(inventory); // Map entity to DTO with InventoryProducts
        }

        // Add a new Inventory with Inventory Products
        public async Task<InventoryDTO.InventoryReadDTO> AddAsync(InventoryDTO.CreateInventoryDTO dto, string userId)
        {
            var inventory = InventoryMapper.MapToInventory(dto, userId); // Map DTO to Inventory entity with userId
            
            // First save the inventory to get its ID
            await _repository.AddAsync(inventory);
            await _repository.SaveChangesAsync();

            // Now create the inventory products with the correct inventory ID
            var inventoryProducts = dto.InventoryProducts.Select(ip => 
                InventoryProductMapper.MapToInventoryProduct(ip, inventory.InventoryId)
            ).ToList();

            inventory.InventoryProducts = inventoryProducts; // Link Inventory with InventoryProducts
            await _repository.SaveChangesAsync(); // Save the inventory products

            // Reload the inventory with included Product data
            var reloadedInventory = await _repository.GetByIdAsync(inventory.InventoryId);
            if (reloadedInventory == null)
                throw new Exception("Failed to reload created inventory");

            return InventoryMapper.MapToInventoryReadDto(reloadedInventory); // Return the created inventory
        }

        // Update an existing Inventory with Inventory Products
        public async Task UpdateAsync(int id, InventoryDTO.UpdateInventoryDTO dto)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                throw new KeyNotFoundException($"Inventory with ID {id} not found.");

            InventoryMapper.MapToExistingInventory(dto, inventory); // Map DTO to existing Inventory entity

            // Clear existing InventoryProducts and link the new ones
            inventory.InventoryProducts.Clear();
            var inventoryProducts = dto.InventoryProducts.Select(ip => new InventoryProduct
            {
                ProductId = ip.ProductId,
                
                Quantity = ip.Quantity
            }).ToList();
            // Add new InventoryProducts
            foreach (var ip in inventoryProducts)
            {
                inventory.InventoryProducts.Add(ip);
            }
            

            await _repository.SaveChangesAsync(); // Save the changes to DB
        }

        // Delete an Inventory with its Inventory Products
        public async Task DeleteAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);
            if (inventory == null)
                throw new KeyNotFoundException($"Inventory with ID {id} not found.");

            _repository.Delete(inventory); // Delete Inventory and its InventoryProducts
            await _repository.SaveChangesAsync(); // Save changes to DB
        }
    }
}