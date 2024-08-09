using AutoMapper;
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
    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();
    public IEnumerable<GuestDto> GetAllGuests() => _guestRepository.GetAll().Select(x => _mapper.Map<GuestDto>(x));

    public async Task<bool> CreateNewGuest(GuestDto guestDto)
    {
        var guestData = _mapper.Map<GuestData>(guestDto);
        return await _guestRepository.InsertAsync(guestData);
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
            var toBeCreatedGuests = guestDtoList.Select(x => _mapper.Map<GuestData>(x)).ToList();
            toBeCreatedGuests.ForEach(guest => { guest.GuestGroupId = groupId; });
            isSuccess = await _guestRepository.InsertMultipleAsync(toBeCreatedGuests);

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
        guest.IsAttended = guestDto.IsAttended;
        guest.SeatLocationId = guestDto.SeatLocation?.BanquetTableId;
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

    public async Task<bool> ModifyFamilyGuest(Guid groupId, string groupName, List<GuestDto> guestDtoList)
    {
        var groupGuests = _guestRepository.GetAll().ToList()
            .Where(guest => guest.GuestGroupId.Equals(guestDtoList.First().GuestGroup?.GuestGroupId)).ToList();
        var toBeModifiedGuests = groupGuests
            .Where(guest => guestDtoList.Any(dto => dto.GuestId == guest.GuestId))
            .ToList();

        var toBeCreatedGuests = guestDtoList
            .ExceptBy(toBeModifiedGuests.Select(x => x.GuestId), dto => dto.GuestId)
            .Select(dto => _mapper.Map<GuestData>(dto))
            .ToList();

        toBeCreatedGuests.ForEach(guest =>
        {
            guest.GuestGroupId = guest.GuestGroup?.GuestGroupId;
            guest.GuestGroup = null;
            guest.CreatorId = Guid.Parse("3d9d1f27-34e5-4310-bb88-9399cb5dad60");
            guest.CreateAt = DateTime.Now;
        });

        toBeModifiedGuests.ForEach(guest =>
        {
            var targetDto = guestDtoList.FirstOrDefault(x => x.GuestId == guest.GuestId);
            if (targetDto == null) return;

            guest.GuestName = targetDto.GuestName;
            guest.GuestRelationship = targetDto.GuestRelationship;
            guest.SeatLocationId = targetDto.SeatLocation?.BanquetTableId;
            guest.Remark = guest.Remark;
            guest.IsAttended = guest.IsAttended;
            guest.GuestGroupId = guest.GuestGroupId;
            guest.SpecialNeeds = targetDto.SpecialNeeds.Select(x =>
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
        });

        var toBeDeletedGuests = _guestRepository.GetAll().ToList()
            .Where(x => x.GuestGroupId == guestDtoList[0].GuestGroup?.GuestGroupId)
            .ExceptBy(guestDtoList.Select(x => x.GuestId), x => x.GuestId).ToList();

        var isSuccess = true;

        var group = await _guestGroupRepository.GetOneAsync(group => group.GuestGroupId == groupId);
        if (group == null) throw new Exception("No Group");
        group.GuestGroupName = groupName;
        isSuccess &= await _guestGroupRepository.UpdateAsync(group);

        if (!toBeCreatedGuests.Count.Equals(0))
        {
            isSuccess &= await _guestRepository.InsertMultipleAsync(toBeCreatedGuests);
        }

        if (!toBeModifiedGuests.Count.Equals(0))
        {
            isSuccess &= await _guestRepository.UpdateMultipleAsync(toBeModifiedGuests);
        }

        if (!toBeDeletedGuests.Count.Equals(0))
        {
            isSuccess &= await _guestRepository.DeleteMultipleAsync(toBeDeletedGuests);
        }

        return isSuccess;
    }

    public IEnumerable<GuestDto> GetAllGroupGuests() => GetAllGuests().Where(x => x.GuestGroup != null);

    public IEnumerable<GuestDto> GetAllSingleGuests() => GetAllGuests().Where(x => x.GuestGroup == null);

    public GetGuestManagementDto GetAllGuestManagement() => new()
    {
        GroupGuests = GetAllGroupGuests(),
        SingleGuests = GetAllSingleGuests()
    };
}