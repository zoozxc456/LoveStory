using LoveStory.WebApi.RequestModel.Recipient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RecipientController(IServiceProvider provider) : BaseController(provider)
{

    [HttpPatch("guest-arrive")]
    public IActionResult GuestArrive([FromBody]GuestArriveRequestModel requestModel)
    {
        return Ok();
    }
    
}