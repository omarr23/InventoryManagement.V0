using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace InventoryManagement.DAL.Models;


public class User :IdentityUser<int>
{
    //[Key]
   // public int UserId { get; set; } //to identity

    //[Required]
   // public string Username { get; set; } = string.Empty;

    //[Required]
   // public string PasswordHash { get; set; } = string.Empty;
   //the id & password are existed in identity user

    [Required]
    public string Role { get; set; } = "USER";

    [ForeignKey("Company")]
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }  // Added virtual for EF navigation

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}