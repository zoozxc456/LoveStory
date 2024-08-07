using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IGuestService
{
    IEnumerable<GuestDto> GetAllGuests();
    Task<bool> CreateNewGuest(GuestDto guestDto);
    Task<bool> CreateFamilyGuest(string familyName, List<GuestDto> guestDtoList);
    Task<bool> DeleteGuestById(Guid guestId);

    Task<bool> ModifySingleGuest(GuestDto guestDto);
    Task<bool> ModifyFamilyGuest(List<GuestDto> guestDtoList);
}