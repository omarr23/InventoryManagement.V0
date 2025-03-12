using System.ComponentModel.DataAnnotations;

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

    public ICollection<User> Users { get; set; } = new List<User>();
}
