using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BanquetTableController(IServiceProvider provider) : BaseController(provider)
{
    private readonly IBanquetTableService _banquetTableService = provider.GetRequiredService<IBanquetTableService>();

    [HttpGet]
    public IActionResult GetAllBanquetTables()
    {
        return Ok(_banquetTableService.GetAllBanquetTables());
    }
}