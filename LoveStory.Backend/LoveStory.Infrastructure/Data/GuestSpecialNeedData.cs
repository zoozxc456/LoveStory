using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("guest_special_needs")]
public class GuestSpecialNeedData
{
    [Key]
    [Column("special_need_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SpecialNeedId { get; set; }

    [Column("special_need_content")]
    [StringLength(int.MaxValue)]
    public string SpecialNeedContent { get; set; } = string.Empty;
    
    [Column("guest_id")]
    [ForeignKey("Guest")]
    public Guid GuestId { get; set; }

    public GuestData Guest { get; set; } = new();

    [Column("create_at")] public DateTime CreateAt { get; set; }

    [ForeignKey("Creator")]
    [Column("creator")]
    public Guid CreatorId { get; set; }

    public UserData Creator { get; set; } = new();
}