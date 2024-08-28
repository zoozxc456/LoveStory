using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Core.Securities;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;

namespace LoveStory.Core.Services;

public class LoginService(
    IRepository<UserData> userRepository,
    IHashProvider hashProvider,
    IAccessTokenProvider accessTokenProvider) : ILoginService
{
    public async Task<(bool, string?)> Login(LoginRequestDto requestDto)
    {
        var user = await userRepository.GetOneAsync(user => user.Username == requestDto.Username);
        if (user == null) throw new Exception("No User");

        var isValidPassword = hashProvider.VerifyPassword(requestDto.Password, user.Password);
        if (!isValidPassword) return (false, null);

        var accessToken = accessTokenProvider.GenerateAccessToken(new AuthOriginLoginAccessTokenDto
        {
            UserId = user.UserId,
            IssueAt = DateTime.Now,
            Expired = DateTime.Now.AddMinutes(30),
            NotBefore = DateTime.Now
        });

        return (true, accessToken);
    }
}