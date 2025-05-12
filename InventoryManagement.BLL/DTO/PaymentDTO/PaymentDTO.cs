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
                [Required(ErrorMessage ="{0} is Required")]
                public int UserId { get; set; }

                [Required(ErrorMessage = "{0} is Required")]
                [Range(1, 1000000, ErrorMessage = "{0} must be between {1} and {2}.")]
                public decimal Amount { get; set; }

                [Required(ErrorMessage = "{0} is Required")]
                public DateTime PaymentDate { get; set; }

                [Required(ErrorMessage = "{0} is Required")]
                [StringLength(50,ErrorMessage = "{0} must be less than {1} character")]
                public string PaymentMethod { get; set; } = string.Empty;

                [StringLength(20, ErrorMessage = "{0} must be less than {1} character")]
                public string Status { get; set; } = "PENDING";

                public string? StripePaymentIntentId { get; set; }

                public string? StripeChargeId { get; set; }
        }
        public class UpdatePaymentDto
        {
            [Required(ErrorMessage = "{0} is Required")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "{0} is Required")]
            [Range(1, 1000000, ErrorMessage = "{0} must be between {1} and {2}.")]
            public decimal Amount { get; set; }

            [Required(ErrorMessage = "{0} is Required")]
            public DateTime PaymentDate { get; set; }

            [Required(ErrorMessage = "{0} is Required")]
            [StringLength(50, ErrorMessage = "{0} must be less than {1} character")]
            public string PaymentMethod { get; set; } = string.Empty;

            [Required(ErrorMessage = "{0} is Required")]
            [StringLength(20, ErrorMessage = "{0} must be less than {1} character")]
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
