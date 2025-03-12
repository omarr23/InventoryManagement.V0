using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; } // ✅ Explicit precision

    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = "PENDING";

    public string? StripePaymentIntentId { get; set; }
    public string? StripeChargeId { get; set; }
}
