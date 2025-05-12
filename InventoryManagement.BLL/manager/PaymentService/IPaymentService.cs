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
       
            Task<ResultT<IEnumerable<PaymentDTO.PaymentReadDto>>> GetAllPaymentsAsync();
            Task<ResultT<PaymentDTO.PaymentReadDto?>> GetPaymentByIdAsync(int id);
            Task<ResultT<PaymentDTO.PaymentReadDto>> CreatePaymentAsync(PaymentDTO.CreatePaymentDto dto);
            Task<ResultT<bool>> UpdatePaymentAsync(int id, PaymentDTO.UpdatePaymentDto dto);
            Task<ResultT<bool>> DeletePaymentAsync(int id);
        }
        // Add custom service methods if needed
 }

