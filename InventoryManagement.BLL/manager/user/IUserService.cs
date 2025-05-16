using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;
using InventoryManagement.BLL.Helper;

namespace InventoryManagement.BLL.manager.user
{
 public interface IUserService
{
    Task<ResultT<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<ResultT<UserDto?>> GetUserByIdAsync(string id);
    Task<ResultT<UserDto?>> CreateUserAsync(CreateUserDto createUserDto);

    Task<ResultT<bool>> UpdateUserAsync(string id, UserDto userDto);
    Task<ResultT<bool>> DeleteUserAsync(string id);
}

}
