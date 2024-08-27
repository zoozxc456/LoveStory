using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.WebApi.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IServiceProvider serviceProvider) : BaseController(serviceProvider)
{
    private readonly IUserService _userService = serviceProvider.GetRequiredService<IUserService>();

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequestModel request)
    {
        await _userService.CreateUserAsync(new CreateUserDto { Username = request.Username, Role = request.Role });
        return Ok();
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> ModifyUserBasicInfo([FromBody] ModifyUserBasicInfoRequestModel request, Guid userId)
    {
        await _userService.ModifyUserBasicInfoAsync(new ModifyUserBasicInfoDto
        {
            UserId = userId,
            Username = request.Username,
            Role = request.Role
        });
        return Ok();
    }

    [HttpPatch("{userId:guid}/Password/Reset")]
    public async Task<IActionResult> ResetPassword(Guid userId)
    {
        await _userService.ResetPasswordAsync(userId);
        return Ok();
    }
}