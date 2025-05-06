using InventoryManagement.BLL.DTO.PaymentDTO;
using InventoryManagement.BLL.manager.PaymentService;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.PaymentRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Manager.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllAsync();
            return payments.Select(PaymentMapper.MapToReadDto);
        }

        public async Task<PaymentDTO.PaymentReadDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            return payment == null ? null : PaymentMapper.MapToReadDto(payment);
        }

        public async Task<PaymentDTO.PaymentReadDto> CreatePaymentAsync(PaymentDTO.CreatePaymentDto dto)
        {
            var payment = PaymentMapper.MapToEntity(dto);
            await _repository.AddAsync(payment);
            await _repository.SaveChangesAsync();
            return PaymentMapper.MapToReadDto(payment);
        }

        public async Task<bool> UpdatePaymentAsync(int id, PaymentDTO.UpdatePaymentDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            PaymentMapper.MapToExistingEntity(existing, dto);
            _repository.Update(existing);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                return false;

            _repository.Delete(payment);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
