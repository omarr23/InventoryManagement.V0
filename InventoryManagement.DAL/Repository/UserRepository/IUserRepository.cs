using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.DAL.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> FindByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
    }
}
