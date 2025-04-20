using InventoryManagement.BLL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDto userDto)
    {
        await _userService.CreateUserAsync(userDto);
        // Assuming GetByIdAsync returns UserDto:
        var createdUser = await _userService.GetAllUsersAsync();
        var latestUser = createdUser.LastOrDefault(); // crude but fine for now
        return CreatedAtAction(nameof(GetById), new { id = latestUser?.Username }, latestUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserDto userDto)
    {
        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
