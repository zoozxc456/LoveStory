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
        var toBeCreatedGuestDto = new GuestDto
        {
            GuestName = request.GuestName,
            Remark = request.Remark,
            GuestRelationship = request.GuestRelationship,
            IsAttended = request.IsAttended,
            CreateAt = DateTime.Now,
            Creator = new UserDto
            {
                UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                Username = "admin"
            },
            SeatLocation = request.SeatLocation == null
                ? null
                : new BanquetTableDto
                {
                    BanquetTableId = request.SeatLocation.BanquetTableId,
                    Creator = new UserDto
                    {
                        UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                        Username = "admin"
                    }
                }
        };

        toBeCreatedGuestDto.SpecialNeeds = request.SpecialNeeds.Select(x => new GuestSpecialNeedDto
        {
            SpecialNeedContent = x,
            CreateAt = DateTime.Now,
            Guest = toBeCreatedGuestDto,
            Creator = new UserDto
            {
                UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                Username = "admin"
            }
        });

        var isSuccess = await _guestService.CreateNewGuest(toBeCreatedGuestDto);
        return Ok(isSuccess);
    }

    [HttpPost("Family")]
    public async Task<IActionResult> AddFamilyGuest([FromBody] CreateFamilyGuestRequestModel request)
    {
        var toBeCreatedGuestDtoList = request.Attendance.Select(x =>
        {
            var toBeCreatedGuestDto = new GuestDto
            {
                GuestName = x.GuestName,
                Remark = x.Remark,
                GuestRelationship = request.GuestRelationship,
                IsAttended = request.IsAttended,
                SeatLocation = request.SeatLocation == null
                    ? null
                    : new BanquetTableDto
                    {
                        BanquetTableId = request.SeatLocation.BanquetTableId,
                        Creator = new UserDto
                        {
                            UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                            Username = "admin"
                        }
                    },
                CreateAt = DateTime.Now,
                Creator = new UserDto
                {
                    UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                    Username = "admin"
                }
            };

            toBeCreatedGuestDto.SpecialNeeds = x.SpecialNeeds.Select(need => new GuestSpecialNeedDto
            {
                SpecialNeedContent = need,
                CreateAt = DateTime.Now,
                Guest = toBeCreatedGuestDto,
                Creator = new UserDto
                {
                    UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                    Username = "admin"
                }
            });

            return toBeCreatedGuestDto;
        });

        var isSuccess = await _guestService.CreateFamilyGuest(request.FamilyName, toBeCreatedGuestDtoList.ToList());
        return Ok(isSuccess);
    }

    [HttpDelete("{guestId:guid}")]
    public async Task<IActionResult> DeleteGuestById(Guid guestId)
    {
        return Ok(await _guestService.DeleteGuestById(guestId));
    }

    [HttpPut]
    public async Task<IActionResult> ModifySingleGuest([FromBody] ModifySingleGuestRequestModel request)
    {
        var toBeModifiedGuestDto = new GuestDto
        {
            GuestId = request.GuestId,
            GuestName = request.GuestName,
            Remark = request.Remark,
            GuestRelationship = request.GuestRelationship,
            SeatLocation = request.SeatLocation == null
                ? null
                : new BanquetTableDto
                {
                    BanquetTableId = request.SeatLocation.BanquetTableId,
                    Creator = new UserDto
                    {
                        UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                        Username = "admin"
                    }
                },
            IsAttended = request.IsAttended,
            CreateAt = DateTime.Now,
            Creator = new UserDto
            {
                UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                Username = "admin"
            },
            SpecialNeeds = request.SpecialNeeds.Select(x => new GuestSpecialNeedDto
            {
                SpecialNeedId = x.SpecialNeedId,
                SpecialNeedContent = x.SpecialNeedContent,
            })
        };

        var isSuccess = await _guestService.ModifySingleGuest(toBeModifiedGuestDto);
        return Ok(isSuccess);
    }

    [HttpPut("Family")]
    public async Task<IActionResult> ModifyFamilyGuests([FromBody] ModifyFamilyGuestRequestModel request)
    {
        var toBeModifiedGuestDtoList = request.Attendance.Select(attendance => new GuestDto
        {
            GuestId = attendance.GuestId,
            GuestName = attendance.GuestName,
            GuestRelationship = request.GuestRelationship,
            Remark = attendance.Remark,
            IsAttended = request.IsAttended,
            SpecialNeeds = attendance.SpecialNeeds.Select(x => new GuestSpecialNeedDto
            {
                SpecialNeedId = x.SpecialNeedId,
                SpecialNeedContent = x.SpecialNeedContent,
            }),
            GuestGroup = new GuestGroupDto
            {
                GuestGroupId = request.GuestGroupId,
                GuestGroupName = request.GuestGroupName
            },
            SeatLocation = request.SeatLocation == null
                ? null
                : new BanquetTableDto
                {
                    BanquetTableId = request.SeatLocation.BanquetTableId,
                    Creator = new UserDto
                    {
                        UserId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
                        Username = "admin"
                    }
                }
        }).ToList();


        var isSuccess = await _guestService.ModifyFamilyGuest(request.GuestGroupId,request.GuestGroupName,toBeModifiedGuestDtoList);

        return Ok(isSuccess);
    }
}