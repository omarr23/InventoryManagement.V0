using System.ComponentModel.DataAnnotations;

public class Inventory
{
    [Key]
    public int InventoryId { get; set; }

    [Required]
    public string OwnerType { get; set; } = string.Empty; // 'USER' or 'COMPANY'

    public int OwnerId { get; set; } // Foreign key (UserId or CompanyId)

    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsPublic { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}
