using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.BLL.Interfaces;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;
using InventoryManagement.BLL.DTO;

namespace InventoryManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Role = userDto.Role
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
            {
                user.Username = userDto.Username;
                user.Role = userDto.Role;
                user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

                _repository.Update(user);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
            {
                _repository.Delete(user);
                await _repository.SaveChangesAsync();
            }
        }

        // Manual mapping
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Role = user.Role,
                Password = string.Empty // Never expose the hashed password
            };
        }
    }
}
