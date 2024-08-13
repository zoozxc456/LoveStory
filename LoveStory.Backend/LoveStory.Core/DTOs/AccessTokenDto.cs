namespace LoveStory.Core.DTOs;

public class AuthOriginLoginAccessTokenDto
{
    public string Username { get; set; } = string.Empty;
    public DateTime IssueAt { get; init; }
    public DateTime Expired { get; init; }
}