using AutoMapper;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class WeddingGiftManagementService(IServiceProvider provider) : IWeddingGiftManagementService
{
    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();
    private readonly IRepository<GuestData> _guestRepository = provider.GetRequiredService<IRepository<GuestData>>();

    private readonly IRepository<WeddingGiftData> _weddingGiftRepository =
        provider.GetRequiredService<IRepository<WeddingGiftData>>();

    public IEnumerable<WeddingGiftManagementDto> Get()
    {
        var guestDataList = _guestRepository.GetAll()
            .Include(x => x.GuestGroup)
            .Include(x => x.Creator)
            .Include(x => x.GuestAttendance)
            .Include(x => x.WeddingGift).ToList();

        var singleGuests = guestDataList.Where(x => x.GuestGroup == null)
            .Select(x => _mapper.Map<WeddingGiftManagementDto>(x));

        var groupGuests = guestDataList.Where(x => x.GuestGroup != null)
            .GroupBy(x => x.GuestGroupId, (_, value) => value.ToList())
            .Select(x => _mapper.Map<WeddingGiftManagementDto>(x));

        return singleGuests.Concat(groupGuests);
    }

    public async Task<bool> CreateWeddingGift(CreateWeddingGiftDto dto)
    {
        if (dto.ManagementType == "single")
        {
            return await CreateSingleGuestWeddingGift(dto);
        }

        return await CreateGroupGuestWeddingGift(dto);
    }

    public async Task<bool> DeleteWeddingGiftById(Guid id)
    {
        var weddingGift = await _weddingGiftRepository.GetOneAsync(gift => gift.WeddingGiftId == id);

        if (weddingGift == null) throw new Exception($"Wedding Gift Id:{id} is not exist");

        return await _weddingGiftRepository.DeleteAsync(weddingGift);
    }

    private async Task<bool> CreateGroupGuestWeddingGift(CreateWeddingGiftDto dto)
    {
        var groupGuests = _guestRepository.GetAll().ToList()
            .Where(x => x.GuestGroupId == dto.ManagementId)
            .ToList();

        if (groupGuests.Any(x => x.GuestId == dto.ManagementId))
            throw new Exception($"GroupId :{dto.ManagementId}, has gift record already.");

        return await _weddingGiftRepository.InsertAsync(new WeddingGiftData
        {
            Amount = dto.Amount,
            CreateAt = DateTime.Now,
            CreatorId = dto.CreatorId,
            Remark = dto.Remark,
            GiftType = dto.GiftType,
            GuestId = groupGuests.First().GuestId
        });
    }

    private async Task<bool> CreateSingleGuestWeddingGift(CreateWeddingGiftDto dto)
    {
        if (await _weddingGiftRepository.GetOneAsync(gift => gift.GuestId == dto.ManagementId) != null)
            throw new Exception($"GuestId :{dto.ManagementId}, has gift record already.");

        return await _weddingGiftRepository.InsertAsync(new WeddingGiftData
        {
            Amount = dto.Amount,
            CreateAt = DateTime.Now,
            CreatorId = dto.CreatorId,
            Remark = dto.Remark,
            GiftType = dto.GiftType,
            GuestId = dto.ManagementId
        });
    }
}