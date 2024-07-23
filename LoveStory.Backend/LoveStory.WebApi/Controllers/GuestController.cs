using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestController(IServiceProvider provider) : Controller
{
    private readonly IGuestService _guestService = provider.GetRequiredService<IGuestService>();

    [HttpGet]
    public IActionResult GetAllGuests()
    {
        var allGuests = _guestService.GetAllGuests().ToList();
        return Ok(allGuests);
    }
}