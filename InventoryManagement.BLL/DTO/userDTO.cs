namespace InventoryManagement.BLL.DTO;

public class UserDto
{
    public string UserId { get; set; } = "";
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = "USER";
}

public class CreateUserDto
{
     public string UserId { get; set; } = "";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "USER";
}