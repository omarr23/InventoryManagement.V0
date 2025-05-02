using System.ComponentModel.DataAnnotations;
using InventoryManagement.DAL.Models;

public class Company
{
    [Key]
    public int CompanyId { get; set; }

    [Required]
    public string CompanyName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
