namespace LoveStory.WebApi.RequestModel;

public class CreateUserRequestModel
{
    public required string Username { get; set; }
    public required string Role { get; set; }
}