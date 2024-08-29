namespace LoveStory.WebApi.RequestModel;

public class CreateWeddingGiftManagementRequestModel
{
    public required Guid ManagementId { get; init; }
    public required string ManagementType { get; init; }
    public required string GiftType { get; init; }
    public required decimal Amount { get; init; }
    public string? Remark { get; init; }
}