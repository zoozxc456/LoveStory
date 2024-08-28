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
    public DateTime CreateAt { get; set; }
    public UserManagementDto? Creator { get; set; }
}

public class CreateUserDto
{
    public required string Username { get; init; }
    public required string Role { get; init; }
    public required Guid CreatorId { get; init; }
}

public class ModifyUserBasicInfoDto
{
    public required Guid UserId { get; init; }
    public required string Username { get; init; }
    public required string Role { get; init; }
}