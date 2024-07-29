namespace LoveStory.WebApi.RequestModel;

public class CreateGuestRequestModel
{
    public required string GuestName { get; set; }
    public required string GuestRelationship { get; set; }
    public required bool IsAttended { get; set; }
    public required string Remark { get; set; }
}