using InventoryManagement.BLL.manager;
using InventoryManagement.DAL.Models;
using InventoryManagement.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]// 🔐 Require authentication for all actions in this controller
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
    public async Task<IActionResult> GetById(string id)  
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    
   [HttpPost]
public async Task<IActionResult> Create(CreateUserDto createUserDto)
{
    var createdUser = await _userService.CreateUserAsync(createUserDto);

    if (createdUser == null)
        return BadRequest("User creation failed.");

    return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, createdUser);
}


    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UserDto userDto)  // Changed to string
    {
        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)  // Changed to string
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
