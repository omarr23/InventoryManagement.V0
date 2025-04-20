using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;

namespace InventoryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(int id, UserDto userDto);
        Task DeleteUserAsync(int id);
    }
}
