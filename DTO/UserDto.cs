public class UserDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // ✅ Correct field name (Not "PasswordHash")
    public string Role { get; set; } = "USER";
}
