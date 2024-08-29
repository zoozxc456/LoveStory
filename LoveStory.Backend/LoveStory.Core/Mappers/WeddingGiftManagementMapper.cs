using AutoMapper;
using LoveStory.Core.DTOs;
using LoveStory.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Mappers;

public class SingleGuestDataToWeddingGiftManagementConverter(IServiceProvider provider)
    : ITypeConverter<GuestData, WeddingGiftManagementDto>
{
    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();

    public WeddingGiftManagementDto Convert(GuestData source, WeddingGiftManagementDto destination,
        ResolutionContext context)
    {
        destination = new WeddingGiftManagementDto
        {
            ManagementName = source.GuestName,
            ManagementId = source.GuestId,
            ManagementType = "single",
            Attendance = new[] { MapSingleGuestToWeddingGiftManagementAttendanceDto(source) }
        };

        return destination;
    }

    private WeddingGiftManagementAttendanceDto MapSingleGuestToWeddingGiftManagementAttendanceDto(
        GuestData source)
    {
        return new WeddingGiftManagementAttendanceDto
        {
            GuestId = source.GuestId,
            GuestName = source.GuestName,
            GuestRelationship = source.GuestRelationship,
            WeddingGift = MapSingleGuestToWeddingGiftDto(source)
        };
    }

    private WeddingGiftDto? MapSingleGuestToWeddingGiftDto(GuestData source)
    {
        if (source.WeddingGift == null) return null;

        return new WeddingGiftDto
        {
            Id = source.WeddingGift.WeddingGiftId,
            Type = source.WeddingGift.GiftType,
            Amount = source.WeddingGift.Amount,
            ReceivedAt = source.WeddingGift.CreateAt,
            Recepient = _mapper.Map<UserDto>(source.Creator),
            Remark = source.WeddingGift.Remark
        };
    }
}

public class GroupGuestDataToWeddingGiftManagementConverter(IServiceProvider provider) :
    ITypeConverter<List<GuestData>, WeddingGiftManagementDto>
{
    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();

    public WeddingGiftManagementDto Convert(
        List<GuestData> source,
        WeddingGiftManagementDto destination,
        ResolutionContext context)
    {
        var guestGroupData = source.First().GuestGroup;
        if (guestGroupData == null) return destination;

        destination = new WeddingGiftManagementDto
        {
            ManagementId = guestGroupData.GuestGroupId,
            ManagementName = guestGroupData.GuestGroupName,
            ManagementType = "group",
            Attendance = MapGroupGuestToWeddingGiftManagementAttendanceDto(source)
        };

        return destination;
    }

    private IEnumerable<WeddingGiftManagementAttendanceDto> MapGroupGuestToWeddingGiftManagementAttendanceDto(
        List<GuestData> source)
    {
        return source.Select(guest => new WeddingGiftManagementAttendanceDto
        {
            GuestId = guest.GuestId,
            GuestName = guest.GuestName,
            GuestRelationship = guest.GuestRelationship,
            WeddingGift = MapToWeddingGiftDto(guest)
        });
    }

    private WeddingGiftDto? MapToWeddingGiftDto(GuestData guest)
    {
        if (guest.WeddingGift == null) return null;

        return new WeddingGiftDto
        {
            Id = guest.WeddingGift.WeddingGiftId,
            Type = guest.WeddingGift.GiftType,
            Amount = guest.WeddingGift.Amount,
            ReceivedAt = guest.WeddingGift.CreateAt,
            Recepient = _mapper.Map<UserDto>(guest.Creator),
            Remark = guest.WeddingGift.Remark
        };
    }
}