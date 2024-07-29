using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IGuestService
{
    IEnumerable<GuestDto> GetAllGuests();
    Task<bool> CreateNewGuest(GuestDto guestDto);
}