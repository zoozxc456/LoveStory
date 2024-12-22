using LoveStory.Core.DTOs.Recipient;
using LoveStory.Core.Services;
using LoveStory.WebApi.RequestModel.Recipient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RecipientController(IServiceProvider provider) : BaseController(provider)
{
    private readonly IRecipientService _recipientService = provider.GetRequiredService<IRecipientService>();


    [HttpPatch("guest-arrive")]
    public IActionResult GuestArrive([FromBody] GuestArriveRequestModel requestModel)
    {
        var isSuccess = _recipientService.RegisterGuestAttendance(new RegisterGuestAttendanceDto
        {
            CreatorId = UserId,
            GuestId = requestModel.GuestId
        });
        return Ok(isSuccess);
    }
}