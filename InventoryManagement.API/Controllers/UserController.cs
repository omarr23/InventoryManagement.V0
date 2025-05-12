using InventoryManagement.BLL.manager.user;
using InventoryManagement.DAL.Models;
using InventoryManagement.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = "Admin,Manager")]
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
        var result = await _userService.GetAllUsersAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateUserAsync(createUserDto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.UserId }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UserDto userDto)
    {
        var result = await _userService.UpdateUserAsync(id, userDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }
}