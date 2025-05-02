using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;
using InventoryManagement.BLL.manager;
using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;
using InventoryManagement.DAL.Repository.UserRepository;

namespace InventoryManagement.BLL.manager.user
{
    public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    // your methods...


        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
{
    var users = _userManager.Users.ToList();

    var userDtos = new List<UserDto>();
    foreach (var user in users)
    {
        var roles = await _userManager.GetRolesAsync(user);
        userDtos.Add(new UserDto
        {
            UserId = user.Id,                          // ✅ Add this
            Username = user.UserName,
            Role = roles.FirstOrDefault() ?? "USER",
        });
    }

    return userDtos;
}


     public async Task<UserDto?> GetUserByIdAsync(string id)
{
    var user = await _userRepository.FindByIdAsync(id); // ✅ Using repository here
    if (user == null) return null;

    var roles = await _userManager.GetRolesAsync(user); // still using UserManager here

    return new UserDto
    {
        UserId = user.Id,
        Username = user.UserName,
        Role = roles.FirstOrDefault() ?? "USER"
    };
}


public async Task<UserDto?> CreateUserAsync(CreateUserDto createUserDto)
{
    var user = new ApplicationUser
    {
        UserName = createUserDto.Username,
        Email = createUserDto.Username // assuming Username is email
    };

    var result = await _userManager.CreateAsync(user, createUserDto.Password);
    if (!result.Succeeded) return null;

    if (!string.IsNullOrWhiteSpace(createUserDto.Role))
        await _userManager.AddToRoleAsync(user, createUserDto.Role);

    return new UserDto
    {
        UserId = user.Id,
        Username = user.UserName,
        Role = createUserDto.Role
    };
}


        public async Task UpdateUserAsync(string id, UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return;

            user.UserName = userDto.Username;
            await _userManager.UpdateAsync(user);

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!string.IsNullOrEmpty(userDto.Role))
                await _userManager.AddToRoleAsync(user, userDto.Role);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.DeleteAsync(user);
        }
    }
}
