using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("wedding_gifts")]
public class WeddingGiftData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("wedding_gift_id")]
    public Guid WeddingGiftId { get; set; }

    [Column("gift_type")]
    [StringLength(20)]
    public string GiftType { get; set; } = string.Empty;

    [Column("amount", TypeName = "decimal(18,4)")]
    public decimal Amount { get; set; }

    [Column("remark")]
    [StringLength(int.MaxValue)]
    public string? Remark { get; set; }

    [Column("create_at")] public DateTime CreateAt { get; set; }

    [Column("creator")]
    [ForeignKey("Creator")]
    public Guid CreatorId { get; set; }

    [InverseProperty("CreatedWeddingGifts")]
    public UserData Creator { get; set; }
    
    [Column("guest")]
    [ForeignKey("Guest")]
    public Guid GuestId { get; set; }
    
    public GuestData Guest { get; set; }
}