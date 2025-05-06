using InventoryManagement.BLL.DTO.PaymentDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.PaymentService
{
    public interface IPaymentService 
    {
       
            Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPaymentsAsync();
            Task<PaymentDTO.PaymentReadDto?> GetPaymentByIdAsync(int id);
            Task<PaymentDTO.PaymentReadDto> CreatePaymentAsync(PaymentDTO.CreatePaymentDto dto);
            Task<bool> UpdatePaymentAsync(int id, PaymentDTO.UpdatePaymentDto dto);
            Task<bool> DeletePaymentAsync(int id);
        }
        // Add custom service methods if needed
 }

