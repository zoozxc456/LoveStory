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
}