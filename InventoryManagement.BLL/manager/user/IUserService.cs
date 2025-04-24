using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;

namespace InventoryManagement.BLL.manager.user
{
 public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string id);
    Task<UserDto?> CreateUserAsync(CreateUserDto createUserDto);

    Task UpdateUserAsync(string id, UserDto userDto);
    Task DeleteUserAsync(string id);
}

}
