using LoveStory.Core.DTOs.Recipient;

namespace LoveStory.Core.Interfaces;

public interface IRecipientService
{
    public List<GetRecipientGuestDto> GetRecipientGuests();
    public GetRecipientGuestOverviewDto GetRecipientGuestOverview(Guid targetId, string guestType);
    public bool RegisterGuestAttendance(RegisterGuestAttendanceDto dto);
    public bool RegisterGuestAttendance(RegisterGuestGroupAttendanceDto dto);
}