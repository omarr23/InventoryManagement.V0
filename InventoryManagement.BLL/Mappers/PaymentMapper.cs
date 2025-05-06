using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.PaymentDTO;


namespace InventoryManagement.BLL.Mappers
{
   
        public static class PaymentMapper
        {
            public static PaymentDTO.PaymentReadDto MapToReadDto(Payment payment)
            {
                return new PaymentDTO.PaymentReadDto
                {
                    PaymentId = payment.PaymentId,
                    UserId = payment.UserId,
                    Amount = payment.Amount,
                    PaymentDate = payment.PaymentDate,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status,
                    StripePaymentIntentId = payment.StripePaymentIntentId,
                    StripeChargeId = payment.StripeChargeId
                };
            }

            public static Payment MapToEntity(PaymentDTO.CreatePaymentDto dto)
            {
                return new Payment
                {
                    UserId = dto.UserId,
                    Amount = dto.Amount,
                    PaymentDate = dto.PaymentDate,
                    PaymentMethod = dto.PaymentMethod,
                    Status = dto.Status,
                    StripePaymentIntentId = dto.StripePaymentIntentId,
                    StripeChargeId = dto.StripeChargeId
                };
            }

            public static void MapToExistingEntity(Payment payment, PaymentDTO.UpdatePaymentDto dto)
            {
                payment.UserId = dto.UserId;
                payment.Amount = dto.Amount;
                payment.PaymentDate = dto.PaymentDate;
                payment.PaymentMethod = dto.PaymentMethod;
                payment.Status = dto.Status;
                payment.StripePaymentIntentId = dto.StripePaymentIntentId;
                payment.StripeChargeId = dto.StripeChargeId;
            }
        }
    }


