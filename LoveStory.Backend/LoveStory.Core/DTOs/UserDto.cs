namespace LoveStory.Core.DTOs;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class UserManagementDto : UserDto
{
    public string Role { get; set; } = string.Empty;
    public bool IsNeededResetPassword { get; set; }
}

public class CreateUserDto
{
    public required string Username { get; set; }
    public required string Role { get; set; }
}