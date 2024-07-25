using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestManagementController(IServiceProvider provider) : Controller
{
    private readonly IGuestManagementService _guestManagementService =
        provider.GetRequiredService<IGuestManagementService>();

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_guestManagementService.GetAllGuestManagement());
    }
}