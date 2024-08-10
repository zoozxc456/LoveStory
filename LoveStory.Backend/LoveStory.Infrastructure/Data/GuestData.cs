using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("guests")]
public class GuestData
{
    [Key]
    [Column("guest_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GuestId { get; set; }

    [Column("guest_name")]
    [StringLength(20)]
    public string GuestName { get; set; } = string.Empty;

    [Column("guest_relationship")]
    [StringLength(20)]
    public string GuestRelationship { get; set; } = string.Empty;

    [Column("guest_group")]
    [ForeignKey("GuestGroup")]
    public Guid? GuestGroupId { get; set; }

    public GuestGroupData? GuestGroup { get; set; }

    [Column("seat_location")]
    [ForeignKey("SeatLocation")]
    public Guid? SeatLocationId { get; set; }

    public BanquetTableData? SeatLocation { get; set; }

    [Column("is_attended")] public bool IsAttended { get; set; }

    [Column("remark")]
    [StringLength(int.MaxValue)]
    public string Remark { get; set; } = string.Empty;
    [Column("create_at")] public DateTime CreateAt { get; set; }

    [Column("creator")]
    [ForeignKey("Creator")]
    public Guid CreatorId { get; set; }

    [InverseProperty("CreatedGuests")] public UserData Creator { get; set; }

    public GuestAttendanceData? GuestAttendance { get; set; }
    public ICollection<GuestSpecialNeedData> SpecialNeeds { get; set; } = new List<GuestSpecialNeedData>();
}