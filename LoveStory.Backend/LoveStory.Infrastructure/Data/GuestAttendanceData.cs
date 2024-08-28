using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("guest_attendances")]
public class GuestAttendanceData
{
    [Key]
    [Column("attendance_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AttendanceId { get; set; }

    [Column("arrival_at")] public DateTime ArrivalAt { get; set; }

    [Column("guest_id")]
    [ForeignKey("Guest")]
    public Guid? GuestId { get; set; }

    public GuestData? Guest { get; set; }
}