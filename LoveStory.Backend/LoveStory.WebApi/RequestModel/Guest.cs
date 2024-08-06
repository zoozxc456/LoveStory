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

public class ModifySingleGuestRequestModel
{
    public required Guid GuestId { get; init; }
    public required string GuestName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public required string Remark { get; init; }
    public required List<ModifySpecialNeedRequestModel> SpecialNeeds { get; init; }
}

public class ModifyFamilyGuestRequestModel
{
    public required string FamilyName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public List<ModifyFamilyGuestPersonRequestModel> Attendance { get; init; }
}

public class ModifyFamilyGuestPersonRequestModel
{
    public required string GuestName { get; init; }
    public required string Remark { get; init; }
    public required List<string> SpecialNeeds { get; init; }
}

public class ModifySpecialNeedRequestModel
{
    public Guid SpecialNeedId { get; set; }
    public string SpecialNeedContent { get; set; } = string.Empty;
}