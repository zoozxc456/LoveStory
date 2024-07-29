using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.WebApi.RequestModel;
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

    [HttpPost]
    public async Task<IActionResult> AddGuest([FromBody] CreateGuestRequestModel request)
    {
        var isSuccess = await _guestService.CreateNewGuest(new GuestDto
        {
            GuestName = request.GuestName,
            Remark = request.Remark,
            GuestRelationship = request.GuestRelationship,
            IsAttended = request.IsAttended,
            CreateAt = DateTime.Now,
            Creator = new UserDto
            {
                UserId = Guid.Parse("d31d634b-a157-4e60-8e0c-1058c50ac96d"),
                Username = "admin"
            }
        });
        return Ok(isSuccess);
    }
}