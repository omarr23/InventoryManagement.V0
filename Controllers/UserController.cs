using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ 1. GET ALL USERS
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users); // Returns HTTP 200 with list of users
    }

    // ✅ 2. GET A SINGLE USER BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found." }); // Returns HTTP 404
        }

        return Ok(user); // Returns HTTP 200 with user details
    }

    // ✅ 3. CREATE A NEW USER
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
        {
            return BadRequest(new { message = "Username and Password are required." });
        }

        // Check if username already exists
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
        if (existingUser != null)
        {
            return BadRequest(new { message = "Username already exists." });
        }

        // Hash the password before saving (security best practice)
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        // Create User Entity
        var newUser = new User
        {
            Username = userDto.Username,
            PasswordHash = hashedPassword, // Storing hashed password
            Role = userDto.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
    }

    // ✅ 4. UPDATE USER BY ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        user.Username = userDto.Username ?? user.Username;
        user.Role = userDto.Role ?? user.Role;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    // ✅ 5. DELETE USER BY ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
}
