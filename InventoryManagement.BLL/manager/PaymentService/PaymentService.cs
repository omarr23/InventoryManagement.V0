using InventoryManagement.BLL.DTO.PaymentDTO;
using InventoryManagement.BLL.Helper;
using InventoryManagement.BLL.manager.PaymentService;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.PaymentRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.BLL.Helper;
using static InventoryManagement.BLL.DTO.PaymentDTO.PaymentDTO;

namespace InventoryManagement.BLL.manager.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultT<IEnumerable<PaymentReadDto>>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _repository.GetAllAsync();
                var result = payments.Select(PaymentMapper.MapToReadDto);
                return ResultT<IEnumerable<PaymentReadDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return ResultT<IEnumerable<PaymentReadDto>>.Failure(
                    ErrorMassege.Failure("Payment.GetAll", $"Error retrieving payments: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<PaymentReadDto?>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _repository.GetByIdAsync(id);
                if (payment == null)
                {
                    return ResultT<PaymentReadDto?>.Failure(
                        ErrorMassege.NotFound("Payment.NotFound", $"Payment with ID {id} not found.")
                    );
                }

                return ResultT<PaymentReadDto?>.Success(PaymentMapper.MapToReadDto(payment));
            }
            catch (Exception ex)
            {
                return ResultT<PaymentReadDto?>.Failure(
                    ErrorMassege.Failure("Payment.GetById", $"Error retrieving payment: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<PaymentReadDto>> CreatePaymentAsync(CreatePaymentDto dto)
        {
            try
            {
                var payment = PaymentMapper.MapToEntity(dto);
                await _repository.AddAsync(payment);
                await _repository.SaveChangesAsync();
                return ResultT<PaymentReadDto>.Success(PaymentMapper.MapToReadDto(payment));
            }
            catch (Exception ex)
            {
                return ResultT<PaymentReadDto>.Failure(
                    ErrorMassege.Failure("Payment.Create", $"Error creating payment: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> UpdatePaymentAsync(int id, UpdatePaymentDto dto)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("Payment.NotFound", $"Payment with ID {id} not found.")
                    );
                }

                PaymentMapper.MapToExistingEntity(existing, dto);
                _repository.Update(existing);
                await _repository.SaveChangesAsync();

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("Payment.Update", $"Error updating payment: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await _repository.GetByIdAsync(id);
                if (payment == null)
                {
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("Payment.NotFound", $"Payment with ID {id} not found.")
                    );
                }

                _repository.Delete(payment);
                await _repository.SaveChangesAsync();
                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("Payment.Delete", $"Error deleting payment: {ex.Message}")
                );
            }
        }
    }
}