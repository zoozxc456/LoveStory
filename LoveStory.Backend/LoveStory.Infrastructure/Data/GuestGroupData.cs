using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("guest_groups")]
public class GuestGroupData
{
    [Key]
    [Column("guest_group_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GuestGroupId { get; set; }

    [Column("guest_group_name")]
    [StringLength(20)]
    public string GuestGroupName { get; set; } = string.Empty;

    [Column("remark")]
    [StringLength(int.MaxValue)]
    public string Remark { get; set; } = string.Empty;

    [Column("create_at")] public DateTime CreateAt { get; set; }

    [Column("creator")]
    [ForeignKey("Creator")]
    public Guid CreatorId { get; set; }

    [InverseProperty("CreatedGroups")] public UserData Creator { get; set; }

    public IEnumerable<GuestData> Guests { get; set; } = new List<GuestData>();
}