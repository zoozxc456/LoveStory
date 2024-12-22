using LoveStory.Core.DTOs.Recipient;

namespace LoveStory.Core.Services;

public interface IRecipientService
{
    public bool RegisterGuestAttendance(RegisterGuestAttendanceDto dto);
}