namespace LoveStory.Core.DTOs;

public class AuthOriginLoginAccessTokenDto
{
    public Guid UserId { get; init; }
    public DateTime IssueAt { get; init; }
    public DateTime Expired { get; init; }
    public DateTime NotBefore { get; init; }
}