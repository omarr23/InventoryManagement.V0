using InventoryManagement.BLL.DTO.SupplierDTO;
using InventoryManagement.BLL.Helper;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.SupplierRepository;

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

        public async Task<ResultT<IEnumerable<SupplierReadDTO>>> GetAllAsync()
        {
            try
            {
                var suppliers = await _repository.GetAllAsync();
                var mapped = suppliers.Select(SupplierMapper.MapToSupplierReadDto);
                return ResultT<IEnumerable<SupplierReadDTO>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return ResultT<IEnumerable<SupplierReadDTO>>.Failure(
                    ErrorMassege.Failure("Supplier.GetAll", $"Error retrieving suppliers: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<SupplierReadDTO?>> GetByIdAsync(int id)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return ResultT<SupplierReadDTO?>.Failure(
                        ErrorMassege.NotFound("Supplier.NotFound", $"Supplier with ID {id} not found.")
                    );

                var mapped = SupplierMapper.MapToSupplierReadDto(supplier);
                return ResultT<SupplierReadDTO?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return ResultT<SupplierReadDTO?>.Failure(
                    ErrorMassege.Failure("Supplier.GetById", $"Error retrieving supplier: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<SupplierReadDTO>> AddAsync(SupplierCreateDTO dto)
        {
            try
            {
                var supplier = SupplierMapper.MapToSupplier(dto);
                await _repository.AddAsync(supplier);
                await _repository.SaveChangesAsync();

                var mapped = SupplierMapper.MapToSupplierReadDto(supplier);
                return ResultT<SupplierReadDTO>.Success(mapped);
            }
            catch (Exception ex)
            {
                return ResultT<SupplierReadDTO>.Failure(
                    ErrorMassege.Failure("Supplier.Add", $"Error adding supplier: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> UpdateAsync(int id, SupplierUpdateDTO dto)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("Supplier.NotFound", $"Supplier with ID {id} not found.")
                    );

                SupplierMapper.MapToExistingSupplier(dto, supplier);
                await _repository.SaveChangesAsync();
                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("Supplier.Update", $"Error updating supplier: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> DeleteAsync(int id)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("Supplier.NotFound", $"Supplier with ID {id} not found.")
                    );

                _repository.Delete(supplier);
                await _repository.SaveChangesAsync();
                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("Supplier.Delete", $"Error deleting supplier: {ex.Message}")
                );
            }
        }
    }
}