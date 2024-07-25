namespace LoveStory.Core.DTOs;

public class GetGuestManagementDto
{
    public IEnumerable<GuestDto> SingleGuests { get; set; } = [];
    public IEnumerable<GuestDto> GroupGuests { get; set; } = [];
}