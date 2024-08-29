using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.WebApi.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeddingGiftManagementController(IServiceProvider provider) : BaseController(provider)
{
    private readonly IWeddingGiftManagementService _weddingGiftService =
        provider.GetRequiredService<IWeddingGiftManagementService>();

    [HttpGet]
    public IActionResult Get()
    {
        var list = _weddingGiftService.Get().ToList();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWeddingGiftManagementRequestModel request)
    {
        await _weddingGiftService.CreateWeddingGift(new CreateWeddingGiftDto
        {
            Amount = request.Amount,
            CreatorId = UserId,
            GiftType = request.GiftType,
            ManagementId = request.ManagementId,
            ManagementType = request.ManagementType,
            Remark = request.Remark
        });
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByManagementId(Guid id)
    {
        await _weddingGiftService.DeleteWeddingGiftById(id);
        return Ok();
    }
}