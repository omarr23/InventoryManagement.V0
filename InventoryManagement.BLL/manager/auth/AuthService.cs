using InventoryManagement.BLL.DTO.authDTO;
using InventoryManagement.BLL.manager;
using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement.BLL.manager.auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
      var user = new ApplicationUser
    {
    UserName = dto.Username,
    Email = dto.Email  
    };

        var result = await _userManager.CreateAsync(user, dto.Password);

        // Optional: assign default role if you have RoleManager configured
        if (result.Succeeded && !string.IsNullOrEmpty(dto.Role))
        {
            await _userManager.AddToRoleAsync(user, dto.Role);
        }

        return result;
    }

   public async Task<string?> LoginAsync(LoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email); // updated!
    if (user == null)
    {
        Console.WriteLine("User not found");
        return null;
    }

    var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
    if (!result.Succeeded)
    {
        Console.WriteLine("Password incorrect");
        return null;
    }

    return GenerateJwtToken(user);
}

    private string GenerateJwtToken(ApplicationUser user)
    {
       var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id), // 🔑 This is what .User.FindFirst(...) uses
    new Claim(ClaimTypes.Name, user.UserName ?? ""),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
