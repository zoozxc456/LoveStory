using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeddingGiftManagementController(IServiceProvider provider) : ControllerBase
{
    private readonly IWeddingGiftManagementService _weddingGiftService = provider.GetRequiredService<IWeddingGiftManagementService>();
    
    [HttpGet]
    public IActionResult Get()
    {
        var list = _weddingGiftService.Get().ToList();
        return Ok(list);
    }
}