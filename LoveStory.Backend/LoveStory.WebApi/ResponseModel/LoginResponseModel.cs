namespace LoveStory.WebApi.ResponseModel;

public class LoginResponseModel
{
    public bool IsSuccess { get; set; }
    public string? AccessToken { get; set; }
}