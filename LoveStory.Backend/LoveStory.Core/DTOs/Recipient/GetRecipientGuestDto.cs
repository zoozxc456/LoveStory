namespace LoveStory.Core.DTOs.Recipient;

public class GetRecipientGuestDto
{
    public required Guid TargetId { get; set; }
    public required string GuestName { get; set; }
    public required int AttendanceAmount { get; set; }
    public required string Relationship { get; set; }
}