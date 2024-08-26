using LoveStory.Core.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace LoveStory.Core.Interfaces;

public interface IAccessTokenProvider
{
    public string GenerateAccessToken(AuthOriginLoginAccessTokenDto dto);
    public TokenValidationResult ValidateAccessToken(string accessToken);
}