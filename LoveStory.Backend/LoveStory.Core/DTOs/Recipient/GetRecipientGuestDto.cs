namespace LoveStory.Core.DTOs.Recipient;

public class GetRecipientGuestDto
{
    public required Guid TargetId { get; set; }
    public required string GuestName { get; set; }
    public required int AttendanceAmount { get; set; }
    public required string Relationship { get; set; }
    public required DateTime? ArrivedAt { get; set; }
}

public class GetRecipientGuestOverviewDto : GetRecipientGuestDto
{
    public required List<string> SpecialNeeds { get; set; }
    public required string? Remark { get; set; }
    public required string? SeatLocation { get; set; }
}