using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestManagementController(IServiceProvider provider) : Controller
{
    private readonly IGuestManagementService _guestManagementService =
        provider.GetRequiredService<IGuestManagementService>();

    private readonly IBanquetTableService _banquetTableService = provider.GetRequiredService<IBanquetTableService>();

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_guestManagementService.GetAllGuestManagement());
    }

    [HttpGet("SeatLocation")]
    public IActionResult GetAllSeatLocations()
    {
        return Ok(_banquetTableService.GetAllBanquetTables());
    }
}