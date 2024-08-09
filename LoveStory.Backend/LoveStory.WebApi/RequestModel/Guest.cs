using LoveStory.Core.DTOs;

namespace LoveStory.WebApi.RequestModel;

public class CreateGuestRequestModel
{
    public required string GuestName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public required string Remark { get; init; }
    public required List<string> SpecialNeeds { get; init; }
    public required CreateGuestSeatLocationRequestModel? SeatLocation { get; init; }
}

public class CreateGuestSeatLocationRequestModel
{
    public required Guid BanquetTableId { get; init; }
    public required string Remark { get; init; }
}

public class CreateFamilyGuestRequestModel
{
    public required string FamilyName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public required CreateGuestSeatLocationRequestModel? SeatLocation { get; init; }
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
    public required ModifyGuestSeatLocationRequestModel? SeatLocation { get; init; }
}

public class ModifyFamilyGuestRequestModel
{
    public required Guid GuestGroupId { get; init; }
    public required string GuestGroupName { get; init; }
    public required string GuestRelationship { get; init; }
    public required bool IsAttended { get; init; }
    public List<ModifyFamilyGuestPersonRequestModel> Attendance { get; init; }
}

public class ModifyFamilyGuestPersonRequestModel
{
    public required Guid GuestId { get; init; }
    public required string GuestName { get; init; }
    public required string GuestRelationship { get; init; }
    public required string Remark { get; init; }
    public required bool IsAttended { get; init; }
    public required List<ModifySpecialNeedRequestModel> SpecialNeeds { get; init; }
}

public class ModifySpecialNeedRequestModel
{
    public Guid SpecialNeedId { get; set; }
    public string SpecialNeedContent { get; set; } = string.Empty;
}

public class ModifyGuestSeatLocationRequestModel
{
    public required Guid BanquetTableId { get; init; }
    public required string Remark { get; init; }
}