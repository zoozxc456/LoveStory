using LoveStory.Core.Interfaces;
using LoveStory.WebApi.RequestModel.GuestManagement;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestManagementController(IServiceProvider provider) : Controller
{
    private readonly IGuestManagementService _guestManagementService =
        provider.GetRequiredService<IGuestManagementService>();

    private readonly IBanquetTableService _banquetTableService = provider.GetRequiredService<IBanquetTableService>();

    private readonly IGuestSeatLocationManagementService _seatLocationManagementService =
        provider.GetRequiredService<IGuestSeatLocationManagementService>();

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

    [HttpGet("SeatLocation-Guests")]
    public IActionResult GetGuestSeatLocationManagement()
    {
        return Ok(_seatLocationManagementService.GetAllGuestSeatLocation());
    }

    [HttpPatch("SeatLocation-Guests/{guestId:guid}")]
    public IActionResult UpdateGuestSeatLocation(Guid guestId, [FromBody] UpdateGuestSeatLocation requestModel)
    {
        return Ok(_seatLocationManagementService.UpdateGuestSeatLocation(guestId, requestModel.SeatLocationId));
    }
}