using InventoryManagement.DAL.Models;
using System.Collections.Generic;

namespace InventoryManagement.BLL.Interfaces;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
}
