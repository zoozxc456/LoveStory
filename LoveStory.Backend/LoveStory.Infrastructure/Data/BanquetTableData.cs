using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("banquet_tables")]
public class BanquetTableData
{
    [Key]
    [Column("banquet_table_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid BanquetTableId { get; set; }

    [Column("table_alias")]
    [StringLength(20)]
    public string TableAlias { get; set; } = string.Empty;

    [Column("max_seat_amount")] public int MaxSeatAmount { get; set; }
    [Column("min_seat_amount")] public int MinSeatAmount { get; set; }

    [Column("remark")]
    [StringLength(int.MaxValue)]
    public string Remark { get; set; } = string.Empty;

    [Column("create_at")] public DateTime CreateAt { get; set; }

    [Column("creator")]
    [ForeignKey("Creator")]
    public Guid CreatorId { get; set; }

    public UserData Creator { get; set; } = new();

    public ICollection<GuestData> Guests { get; set; } = new List<GuestData>();
}