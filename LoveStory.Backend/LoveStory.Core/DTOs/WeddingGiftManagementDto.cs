namespace LoveStory.Core.DTOs;

public class WeddingGiftManagementDto
{
    public Guid ManagementId { get; set; }
    public string ManagementName { get; set; }
    public string ManagementType { get; set; }
    public IEnumerable<WeddingGiftManagementAttendanceDto> Attendance { get; set; }
}

public class WeddingGiftManagementAttendanceDto
{
    public Guid GuestId { get; set; }
    public string GuestName { get; set; }
    public string GuestRelationship { get; set; }
    public WeddingGiftDto? WeddingGift { get; set; }
}

public class WeddingGiftDto
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime ReceivedAt { get; set; }
    public UserDto Recepient { get; set; }
    public string? Remark { get; set; }
}