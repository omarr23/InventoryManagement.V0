using System.ComponentModel.DataAnnotations.Schema;

public class InventoryProduct
{
    [ForeignKey("Inventory")]
    public int InventoryId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Inventory Inventory { get; set; }
    public Product Product { get; set; }
}

// you are using data anotation ,, we should use fluentapi for this

