using InventoryManagement.BLL.DTO.PaymentDTO;
using InventoryManagement.BLL.manager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.Helper;
namespace InventoryManagement.BLL.manager.PaymentService
{
    public interface IPaymentService 
    {
       
            Task<Result<IEnumerable<PaymentDTO.PaymentReadDto>>> GetAllPaymentsAsync();
            Task<Result<PaymentDTO.PaymentReadDto?>> GetPaymentByIdAsync(int id);
            Task<Result<PaymentDTO.PaymentReadDto>> CreatePaymentAsync(PaymentDTO.CreatePaymentDto dto);
            Task<Result<bool>> UpdatePaymentAsync(int id, PaymentDTO.UpdatePaymentDto dto);
            Task<Result<bool>> DeletePaymentAsync(int id);
        }
        // Add custom service methods if needed
 }

