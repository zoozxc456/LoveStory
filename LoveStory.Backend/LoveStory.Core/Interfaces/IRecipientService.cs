using LoveStory.Core.DTOs.Recipient;

namespace LoveStory.Core.Interfaces;

public interface IRecipientService
{
    public List<GetRecipientGuestDto> GetRecipientGuests();
    public bool RegisterGuestAttendance(RegisterGuestAttendanceDto dto);
}