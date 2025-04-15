using System.Threading.Tasks;
using InventoryManagement.BLL.Interfaces;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagement.BLL.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();


    public UserService(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.GetAllAsync().Result;
    }

    public User? GetUserById(int id)
    {
        return _repository.GetByIdAsync(id).Result;
    }

    public void CreateUser(User user)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        _repository.AddAsync(user).Wait();
        _repository.SaveChangesAsync().Wait();
    }

    public void UpdateUser(User user)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        _repository.Update(user);
        _repository.SaveChangesAsync().Wait();
    }

    public void DeleteUser(int id)
    {
        var user = _repository.GetByIdAsync(id).Result;
        if (user != null)
        {
            _repository.Delete(user);
            _repository.SaveChangesAsync().Wait();
        }
    }
}
