using LoveStory.Core.Services;

namespace LoveStory.Core.Interfaces;

public interface IGuestSeatLocationManagementService
{
    public List<GetGuestSeatLocationDto> GetAllGuestSeatLocation();
    public bool UpdateGuestSeatLocation(Guid guestId, Guid seatLocationId);
}