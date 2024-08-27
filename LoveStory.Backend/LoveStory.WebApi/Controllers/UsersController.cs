using LoveStory.Core.Interfaces;
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
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsersAsync().Result;
        return Ok(users);
    }
}