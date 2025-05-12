using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;
using InventoryManagement.BLL.manager;
using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;
using InventoryManagement.DAL.Repository.UserRepository;
using InventoryManagement.BLL.Helper;

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

        public async Task<ResultT<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var userDtos = new List<UserDto>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userDtos.Add(new UserDto
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        Role = roles.FirstOrDefault() ?? "USER"
                    });
                }

                return ResultT<IEnumerable<UserDto>>.Success(userDtos);
            }
            catch (Exception ex)
            {
                return ResultT<IEnumerable<UserDto>>.Failure(
                    ErrorMassege.Failure("User.GetAll", $"Error retrieving users: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<UserDto>> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userRepository.FindByIdAsync(id);
                if (user == null)
                    return ResultT<UserDto>.Failure(
                        ErrorMassege.NotFound("User.NotFound", $"User with ID {id} not found.")
                    );

                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Role = roles.FirstOrDefault() ?? "USER"
                };

                return ResultT<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                return ResultT<UserDto>.Failure(
                    ErrorMassege.Failure("User.Get", $"Error retrieving user: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<UserDto>> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Username,
                    Email = dto.Username
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return ResultT<UserDto>.Failure(
                        ErrorMassege.Failure("User.Create", $"Failed to create user: {errors}")
                    );
                }

                if (!string.IsNullOrWhiteSpace(dto.Role))
                    await _userManager.AddToRoleAsync(user, dto.Role);

                return ResultT<UserDto>.Success(new UserDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Role = dto.Role
                });
            }
            catch (Exception ex)
            {
                return ResultT<UserDto>.Failure(
                    ErrorMassege.Failure("User.Create", $"Error creating user: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> UpdateUserAsync(string id, UserDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("User.NotFound", $"User with ID {id} not found.")
                    );

                user.UserName = dto.Username;
                await _userManager.UpdateAsync(user);

                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!string.IsNullOrEmpty(dto.Role))
                    await _userManager.AddToRoleAsync(user, dto.Role);

                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("User.Update", $"Error updating user: {ex.Message}")
                );
            }
        }

        public async Task<ResultT<bool>> DeleteUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return ResultT<bool>.Failure(
                        ErrorMassege.NotFound("User.NotFound", $"User with ID {id} not found.")
                    );

                await _userManager.DeleteAsync(user);
                return ResultT<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultT<bool>.Failure(
                    ErrorMassege.Failure("User.Delete", $"Error deleting user: {ex.Message}")
                );
            }
        }
    }
}