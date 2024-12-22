namespace LoveStory.Core.DTOs.Recipient;

public class RegisterGuestAttendanceDto
{
    public required Guid GuestId { get; set; }
    public required Guid CreatorId { get; set; }
}