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

        public async Task<ResultT<IEnumerable<InventoryProductReadDTO>>> GetAllAsync()
        {
            try
            {
                var inventoryProducts = await _repository.GetAllAsync();
                var result = inventoryProducts.Select(InventoryProductMapper.MapToInventoryProductReadDto);
                return ResultT<IEnumerable<InventoryProductReadDTO>>.Success(result);
            }
            catch (Exception ex)
            {
                return ResultT<IEnumerable<InventoryProductReadDTO>>.Failure(
                    ErrorMassege.Failure("InventoryProduct.GetAll", $"Error retrieving inventory products: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<InventoryProductReadDTO?>> GetByIdAsync(int inventoryId, int productId)
        {
            try
            {
                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                {
                    return ResultT<InventoryProductReadDTO?>.Failure(
                        ErrorMassege.NotFound("InventoryProduct.NotFound", $"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.")
                    );
                }

                return ResultT<InventoryProductReadDTO?>.Success(
                    InventoryProductMapper.MapToInventoryProductReadDto(inventoryProduct)
                );
            }
            catch (Exception ex)
            {
                return ResultT<InventoryProductReadDTO?>.Failure(
                    ErrorMassege.Failure("InventoryProduct.GetById", $"Error retrieving inventory product: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> AddAsync(CreateInventoryProductDTO dto, int inventoryId)
        {
            try
            {
                if (dto.Quantity <= 0)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.Validation("InventoryProduct.Quantity.Invalid", "Quantity must be a positive value.")
                    );
                }

                var inventoryProduct = InventoryProductMapper.MapToInventoryProduct(dto, inventoryId);
                await _repository.AddAsync(inventoryProduct);
                await _repository.SaveChangesAsync();

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("InventoryProduct.Create", $"Error adding inventory product: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> UpdateAsync(int inventoryId, int productId, UpdateInventoryProductDTO dto)
        {
            try
            {
                if (dto.Quantity <= 0)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.Validation("InventoryProduct.Quantity.Invalid", "Quantity must be a positive value.")
                    );
                }

                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("InventoryProduct.NotFound", $"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.")
                    );
                }

                InventoryProductMapper.MapToExistingInventoryProduct(dto, inventoryProduct);
                await _repository.SaveChangesAsync();

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("InventoryProduct.Update", $"Error updating inventory product: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> DeleteAsync(int inventoryId, int productId)
        {
            try
            {
                var inventoryProduct = await _repository.GetByIdAsync(inventoryId, productId);
                if (inventoryProduct == null)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("InventoryProduct.NotFound", $"InventoryProduct with InventoryId {inventoryId} and ProductId {productId} not found.")
                    );
                }

                _repository.Delete(inventoryProduct);
                await _repository.SaveChangesAsync();

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("InventoryProduct.Delete", $"Error deleting inventory product: {ex.Message}")
                );
            }
        }
    }
}