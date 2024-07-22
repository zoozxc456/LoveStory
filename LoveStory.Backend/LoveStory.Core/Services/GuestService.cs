using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class GuestService(IServiceProvider provider) : IGuestService
{
    private readonly IRepository<GuestData> _guestRepository = provider.GetRequiredService<IRepository<GuestData>>();

    public IEnumerable<GuestDto> GetAllGuests()
    {
        return _guestRepository.GetAll().Select(x => ConvertToDtoFromData(x));
    }

    public async Task<GuestDto> GetGuestById(Guid guestId)
    {
        var guest = await _guestRepository.GetOneAsync(x => Equals(x.GuestId, guestId));
        if (Equals(guest, null)) throw new Exception("");

        return ConvertToDtoFromData(guest);
    }

    private static GuestDto ConvertToDtoFromData(GuestData data) => new()
    {
        GuestId = data.GuestId,
        GuestName = data.GuestName,
        GuestRelationship = data.GuestRelationship,
        IsAttended = data.IsAttended,
        Remark = data.Remark,
        CreateAt = data.CreateAt
    };
}