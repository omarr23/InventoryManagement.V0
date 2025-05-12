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

        public async Task<Result<IEnumerable<SupplierReadDTO>>> GetAllAsync()
        {
            try
            {
                var suppliers = await _repository.GetAllAsync();
                var mapped = suppliers.Select(SupplierMapper.MapToSupplierReadDto);
                return Result<IEnumerable<SupplierReadDTO>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SupplierReadDTO>>.Failure($"Error retrieving suppliers: {ex.Message}");
            }
        }

        public async Task<Result<SupplierReadDTO?>> GetByIdAsync(int id)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return Result<SupplierReadDTO?>.Failure($"Supplier with ID {id} not found.");

                var mapped = SupplierMapper.MapToSupplierReadDto(supplier);
                return Result<SupplierReadDTO?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<SupplierReadDTO?>.Failure($"Error retrieving supplier: {ex.Message}");
            }
        }

        public async Task<Result<SupplierReadDTO>> AddAsync(SupplierCreateDTO dto)
        {
            try
            {
                var supplier = SupplierMapper.MapToSupplier(dto);
                await _repository.AddAsync(supplier);
                await _repository.SaveChangesAsync();

                var mapped = SupplierMapper.MapToSupplierReadDto(supplier);
                return Result<SupplierReadDTO>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<SupplierReadDTO>.Failure($"Error adding supplier: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UpdateAsync(int id, SupplierUpdateDTO dto)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return Result<bool>.Failure($"Supplier with ID {id} not found.");

                SupplierMapper.MapToExistingSupplier(dto, supplier);
                await _repository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating supplier: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var supplier = await _repository.GetByIdAsync(id);
                if (supplier == null)
                    return Result<bool>.Failure($"Supplier with ID {id} not found.");

                _repository.Delete(supplier);
                await _repository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting supplier: {ex.Message}");
            }
        }
    }
}
