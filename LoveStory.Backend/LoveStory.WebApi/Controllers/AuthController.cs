using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Core.Securities;
using LoveStory.WebApi.RequestModel;
using LoveStory.WebApi.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IServiceProvider provider) : ControllerBase
{
    private readonly ILoginService _loginService = provider.GetRequiredService<ILoginService>();
    private readonly IHostEnvironment _environment = provider.GetRequiredService<IHostEnvironment>();

    [HttpPost("Origin")]
    public async Task<IActionResult> AuthOriginLogin([FromBody] LoginRequestModel request)
    {
        var (isSuccess, token) = await (_environment.IsProduction()
            ? _loginService.Login(new LoginRequestDto
            {
                Username = request.Username,
                Password = request.Password
            })
            : _loginService.Login(new DevelopLoginRequestDto
            {
                Username = request.Username
            }));

        return Ok(new LoginResponseModel
        {
            IsSuccess = isSuccess, AccessToken = token
        });
    }
}