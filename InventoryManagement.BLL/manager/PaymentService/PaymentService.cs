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

        public async Task<Result<IEnumerable<PaymentDTO.PaymentReadDto>>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllAsync();
            var result = payments.Select(PaymentMapper.MapToReadDto);
            return Result<IEnumerable<PaymentReadDto>>.Success(result);
        }

            public async Task<Result<PaymentDTO.PaymentReadDto?>> GetPaymentByIdAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                return Result<PaymentReadDto?>.Failure($"Payment with ID {id} not found.");

            return Result<PaymentReadDto?>.Success(PaymentMapper.MapToReadDto(payment));
        }

        public async Task<Result<PaymentDTO.PaymentReadDto>> CreatePaymentAsync(PaymentDTO.CreatePaymentDto dto)
        {
            var payment = PaymentMapper.MapToEntity(dto);
            await _repository.AddAsync(payment);
            await _repository.SaveChangesAsync();
            return Result<PaymentReadDto>.Success(PaymentMapper.MapToReadDto(payment));
        }

        public async Task<Result<bool>> UpdatePaymentAsync(int id, PaymentDTO.UpdatePaymentDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return Result<bool>.Failure("Payment not found.");

            PaymentMapper.MapToExistingEntity(existing, dto);
            _repository.Update(existing);
            await _repository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeletePaymentAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                return Result<bool>.Failure("Payment not found.");

            _repository.Delete(payment);
            await _repository.SaveChangesAsync();
            return Result<bool>.Success(true);

        }
    }
}
