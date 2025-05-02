using InventoryManagement.BLL.DTO.authDTO;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.BLL.manager.auth
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto); // returns JWT token
    }
}
