using InventoryManagement.BLL.Interfaces;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;

namespace InventoryManagement.BLL.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;

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
        _repository.AddAsync(user).Wait();
        _repository.SaveChangesAsync().Wait();
    }

    public void UpdateUser(User user)
    {
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
