using System.Text.Json.Serialization;

namespace LoveStory.Core.DTOs;

public class GuestDto
{
    public Guid GuestId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestRelationship { get; set; } = string.Empty;
    public GuestGroupDto? GuestGroup { get; set; }
    public BanquetTableDto? SeatLocation { get; set; }
    public bool IsAttended { get; set; }
    public string Remark { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public UserDto Creator { get; set; } = new();
    public IEnumerable<GuestSpecialNeedDto> SpecialNeeds { get; set; } = [];
}

public class GuestGroupDto
{
    public Guid GuestGroupId { get; set; }
    public string GuestGroupName { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public UserDto Creator { get; set; } = new();
}

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class GuestAttendanceDto
{
    public Guid AttendanceId { get; set; }
    public DateTime ArrivalAt { get; set; }
    public GuestDto? Guest { get; set; }
}

public class GuestSpecialNeedDto
{
    public Guid SpecialNeedId { get; set; }
    public string SpecialNeedContent { get; set; } = string.Empty;
    [JsonIgnore] public GuestDto Guest { get; set; } = new();
    public DateTime CreateAt { get; set; }
    public UserDto Creator { get; set; } = new();
}

public class BanquetTableDto
{
    public Guid BanquetTableId { get; set; }
    public string TableAlias { get; set; } = string.Empty;
    public int MaxSeatAmount { get; set; }
    public int MinSeatAmount { get; set; }
    public string Remark { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public UserDto Creator { get; set; } = new();
}