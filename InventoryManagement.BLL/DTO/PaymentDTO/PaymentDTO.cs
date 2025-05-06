using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.DTO.PaymentDTO
{
    public class PaymentDTO
    {

        public class CreatePaymentDto
        {
                [Required]
                public int UserId { get; set; }

                [Required]
                [Range(0.01, 999999999.99)]
                public decimal Amount { get; set; }

                [Required]
                public DateTime PaymentDate { get; set; }

                [Required]
                [StringLength(50)]
                public string PaymentMethod { get; set; } = string.Empty;

                [StringLength(20)]
                public string Status { get; set; } = "PENDING";

                public string? StripePaymentIntentId { get; set; }

                public string? StripeChargeId { get; set; }
        }
        public class UpdatePaymentDto
        {
            [Required]
            public int UserId { get; set; }

            [Required]
            [Range(0.01, 999999999.99)]
            public decimal Amount { get; set; }

            [Required]
            public DateTime PaymentDate { get; set; }

            [Required]
            [StringLength(50)]
            public string PaymentMethod { get; set; } = string.Empty;

            [Required]
            [StringLength(20)]
            public string Status { get; set; } = "PENDING";

            public string? StripePaymentIntentId { get; set; }

            public string? StripeChargeId { get; set; }
        }
        public class PaymentReadDto
        {
            public int PaymentId { get; set; }
            public int UserId { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
            public string PaymentMethod { get; set; } = string.Empty;
            public string Status { get; set; } = "PENDING";
            public string? StripePaymentIntentId { get; set; }
            public string? StripeChargeId { get; set; }
        }

    }
}
