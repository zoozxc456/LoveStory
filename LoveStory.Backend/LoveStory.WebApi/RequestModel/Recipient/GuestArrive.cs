namespace LoveStory.WebApi.RequestModel.Recipient;

public class GuestArriveRequestModel
{
    public required Guid GuestId { get; set; }
    public required string GuestType { get; set; }
}