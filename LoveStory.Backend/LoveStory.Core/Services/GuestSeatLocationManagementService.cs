using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class GuestSeatLocationManagementService(IServiceProvider provider) : IGuestSeatLocationManagementService
{
    private readonly IRepository<GuestData> _repository = provider.GetRequiredService<IRepository<GuestData>>();

    public List<GetGuestSeatLocationDto> GetAllGuestSeatLocation()
    {
        return _repository.GetAll()
            .Select(x => new GetGuestSeatLocationDto
            {
                GuestId = x.GuestId, GuestName = x.GuestName, GuestRelationship = x.GuestRelationship,
                TableAlias = x.SeatLocation != null ? x.SeatLocation.TableAlias : null,
                SpecialNeeds = x.SpecialNeeds.Select(y => y.SpecialNeedContent).ToList()
            })
            .ToList();
    }

    public bool UpdateGuestSeatLocation(Guid guestId, Guid seatLocationId)
    {
        var guest = _repository.GetOneAsync(guest=>guest.GuestId == guestId).Result;

        if (guest is null) throw new Exception("");

        guest.SeatLocationId = seatLocationId;

        return _repository.UpdateAsync(guest).Result;
    }
}

public class GetGuestSeatLocationDto
{
    public required Guid GuestId { get; set; }
    public required string GuestName { get; set; }
    public required string GuestRelationship { get; set; }
    public required string? TableAlias { get; set; }
    public required List<string> SpecialNeeds { get; set; }
}