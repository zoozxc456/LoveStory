using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IAccessTokenProvider
{
    public string GenerateAccessToken(AuthOriginLoginAccessTokenDto dto);
    public bool ValidateAccessToken(string accessToken);
}