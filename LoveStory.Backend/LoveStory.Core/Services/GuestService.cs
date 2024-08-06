using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class GuestService(IServiceProvider provider) : IGuestService, IGuestManagementService
{
    private readonly IRepository<GuestData> _guestRepository = provider.GetRequiredService<IRepository<GuestData>>();
    private readonly IGuestGroupRepository _guestGroupRepository = provider.GetRequiredService<IGuestGroupRepository>();

    public IEnumerable<GuestDto> GetAllGuests() => _guestRepository.GetAll().Select(x => ConvertToDtoFromData(x));

    public async Task<bool> CreateNewGuest(GuestDto guestDto)
    {
        return await _guestRepository.InsertAsync(new GuestData
        {
            GuestName = guestDto.GuestName,
            GuestRelationship = guestDto.GuestRelationship,
            IsAttended = guestDto.IsAttended,
            Remark = guestDto.Remark,
            CreatorId = guestDto.Creator.UserId,
            CreateAt = guestDto.CreateAt,
            SpecialNeeds = guestDto.SpecialNeeds.Select(ConvertToDataToDto).ToList()
        });
    }

    public async Task<bool> CreateFamilyGuest(string familyName, List<GuestDto> guestDtoList)
    {
        var (isSuccess, groupId) = await _guestGroupRepository.CreateWithIdAsync(new GuestGroupData
        {
            GuestGroupName = familyName,
            CreateAt = DateTime.Now,
            CreatorId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60"),
            Remark = ""
        });

        if (isSuccess)
        {
            var guestDatas = guestDtoList.Select(x => new GuestData
            {
                GuestName = x.GuestName,
                GuestRelationship = x.GuestRelationship,
                IsAttended = x.IsAttended,
                Remark = x.Remark,
                CreatorId = x.Creator.UserId,
                CreateAt = DateTime.Now,
                SpecialNeeds = x.SpecialNeeds.Select(ConvertToDataToDto).ToList(),
                GuestGroupId = groupId
            }).ToList();
            isSuccess = await _guestRepository.InsertMultipleAsync(guestDatas);

            return isSuccess;
        }

        return isSuccess;
    }

    public async Task<bool> DeleteGuestById(Guid guestId)
    {
        var guest = await _guestRepository.GetOneAsync(guest => guest.GuestId == guestId);
        if (guest == null) return false;

        return await _guestRepository.DeleteAsync(guest);
    }

    public async Task<bool> ModifySingleGuest(GuestDto guestDto)
    {
        var guest = _guestRepository.GetAll().FirstOrDefault(x => x.GuestId == guestDto.GuestId);
        if (guest == null) throw new Exception("No Data");

        guest.GuestRelationship = guestDto.GuestRelationship;
        guest.GuestName = guestDto.GuestName;
        guest.Remark = guestDto.Remark;

        guest.SpecialNeeds = guestDto.SpecialNeeds.Select(x =>
        {
            var guestSpecialNeedData = guest.SpecialNeeds.FirstOrDefault(g => g.SpecialNeedId == x.SpecialNeedId);
            if (guestSpecialNeedData == null)
            {
                return new GuestSpecialNeedData
                {
                    SpecialNeedContent = x.SpecialNeedContent,
                    GuestId = guest.GuestId,
                    CreateAt = DateTime.Now,
                    CreatorId = guest.CreatorId
                };
            }

            guestSpecialNeedData.SpecialNeedContent = x.SpecialNeedContent;
            return guestSpecialNeedData;
        }).ToList();

        var isSuccess = await _guestRepository.UpdateAsync(guest);
        return isSuccess;
    }

    public IEnumerable<GuestDto> GetAllGroupGuests() => GetAllGuests().Where(x => x.GuestGroup != null);

    public IEnumerable<GuestDto> GetAllSingleGuests() => GetAllGuests().Where(x => x.GuestGroup == null);

    public static GuestSpecialNeedData ConvertToDataToDto(GuestSpecialNeedDto dto) => new()
    {
        SpecialNeedContent = dto.SpecialNeedContent,
        GuestId = dto.Guest.GuestId,
        CreateAt = dto.CreateAt,
        CreatorId = dto.Creator.UserId
    };

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