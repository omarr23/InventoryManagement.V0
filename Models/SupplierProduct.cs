using System.ComponentModel.DataAnnotations.Schema;

public class SupplierProduct
{
    public int SupplierId { get; set; }
    public int ProductId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DefaultCost { get; set; } // ✅ Explicit precision

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
