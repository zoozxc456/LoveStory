namespace LoveStory.WebApi.RequestModel;

public class CreateGuestRequestModel
{
    public required string GuestName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public required string Remark { get; init; }
    public required List<string> SpecialNeeds { get; init; }
}

public class CreateFamilyGuestRequestModel
{
    public required string FamilyName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public List<CreateFamilyGuestPersonRequestModel> Attendance { get; init; }
}

public class CreateFamilyGuestPersonRequestModel
{
    public required string GuestName { get; init; }
    public required string Remark { get; init; }
    public required List<string> SpecialNeeds { get; init; }
}