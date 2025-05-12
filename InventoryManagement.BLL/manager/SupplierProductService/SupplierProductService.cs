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

        public async Task<ResultT<bool>> AddSupplierProductAsync(CreateSupplierProductDTO dto)
        {
            try
            {
                var existing = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
                if (existing != null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.Failure("SupplierProduct.Exists", "This SupplierProduct relation already exists.")
                    );

                var supplierProduct = SupplierProductMapper.MapToSupplierProductDto(dto);
                await _repository.AddSupplierProductAsync(supplierProduct);

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("SupplierProduct.Add", $"An error occurred while adding the SupplierProduct: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<SupplierProductReadDTO>> GetSupplierProductByIdAsync(int supplierId, int productId)
        {
            try
            {
                var supplierProduct = await _repository.GetSupplierProductAsync(supplierId, productId);
                if (supplierProduct == null)
                    return ResultT<SupplierProductReadDTO>.Failure(
                        ErrorMassege.NotFound("SupplierProduct.NotFound", "The SupplierProduct relation was not found.")
                    );

                return ResultT<SupplierProductReadDTO>.Success(
                    SupplierProductMapper.MapToSupplierProductReadDto(supplierProduct)
                );
            }
            catch (Exception ex)
            {
                return ResultT<SupplierProductReadDTO>.Failure(
                    ErrorMassege.Failure("SupplierProduct.Get", $"An error occurred while retrieving the SupplierProduct: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> UpdateSupplierProductAsync(UpdateSupplierProductDTO dto)
        {
            try
            {
                var existing = await _repository.GetSupplierProductAsync(dto.SupplierId, dto.ProductId);
                if (existing == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("SupplierProduct.NotFound", "The SupplierProduct to update does not exist.")
                    );

                SupplierProductMapper.MapToExistingSupplierProduct(dto, existing);
                await _repository.UpdateSupplierProductAsync(existing);

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("SupplierProduct.Update", $"An error occurred while updating the SupplierProduct: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> DeleteSupplierProductAsync(int supplierId, int productId)
        {
            try
            {
                var existing = await _repository.GetSupplierProductAsync(supplierId, productId);
                if (existing == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("SupplierProduct.NotFound", "The SupplierProduct to delete does not exist.")
                    );

                await _repository.DeleteSupplierProductAsync(supplierId, productId);
                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("SupplierProduct.Delete", $"An error occurred while deleting the SupplierProduct: {ex.Message}")
                );
            }
        }
    }
}