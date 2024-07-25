using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class GuestService(IServiceProvider provider) : IGuestService, IGuestManagementService
{
    private readonly IRepository<GuestData> _guestRepository = provider.GetRequiredService<IRepository<GuestData>>();

    public IEnumerable<GuestDto> GetAllGuests() => _guestRepository.GetAll().Select(x => ConvertToDtoFromData(x));

    public IEnumerable<GuestDto> GetAllGroupGuests() => GetAllGuests().Where(x => x.GuestGroup != null);

    public IEnumerable<GuestDto> GetAllSingleGuests() => GetAllGuests().Where(x => x.GuestGroup == null);

    private static GuestDto ConvertToDtoFromData(GuestData data) => new()
    {
        GuestId = data.GuestId,
        GuestName = data.GuestName,
        GuestRelationship = data.GuestRelationship,
        IsAttended = data.IsAttended,
        Remark = data.Remark,
        CreateAt = data.CreateAt,
        GuestGroup = ConvertToDtoFromData(data.GuestGroup),
        SeatLocation = ConvertToDtoFromData(data.SeatLocation),
        Creator = ConvertToDtoFromData(data.Creator),
        SpecialNeeds = data.SpecialNeeds.Select(ConvertToDtoFromData)
    };

    private static GuestGroupDto? ConvertToDtoFromData(GuestGroupData? data) => data == null
        ? null
        : new()
        {
            GuestGroupId = data.GuestGroupId,
            GuestGroupName = data.GuestGroupName,
            Remark = data.Remark,
            CreateAt = data.CreateAt,
            Creator = ConvertToDtoFromData(data.Creator)
        };

    private static UserDto ConvertToDtoFromData(UserData data) => new()
    {
        UserId = data.UserId,
        Username = data.Username
    };

    private static BanquetTableDto? ConvertToDtoFromData(BanquetTableData? data) => data == null
        ? null
        : new()
        {
            BanquetTableId = data.BanquetTableId,
            MaxSeatAmount = data.MaxSeatAmount,
            MinSeatAmount = data.MinSeatAmount,
            TableAlias = data.TableAlias,
            Remark = data.Remark,
            CreateAt = data.CreateAt,
            Creator = ConvertToDtoFromData(data.Creator)
        };

    private static GuestSpecialNeedDto ConvertToDtoFromData(GuestSpecialNeedData data) => new()
    {
        SpecialNeedId = data.SpecialNeedId,
        SpecialNeedContent = data.SpecialNeedContent,
        Guest = ConvertToDtoFromData(data.Guest),
        CreateAt = data.CreateAt,
        Creator = ConvertToDtoFromData(data.Creator)
    };

    public GetGuestManagementDto GetAllGuestManagement() => new()
    {
        GroupGuests = GetAllGroupGuests(),
        SingleGuests = GetAllSingleGuests()
    };
}