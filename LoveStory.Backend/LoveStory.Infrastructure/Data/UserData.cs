using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveStory.Infrastructure.Data;

[Table("users")]
public class UserData
{
    [Key]
    [Column("user_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; }

    [Column("username")]
    [StringLength(20)]
    public string Username { get; set; } = string.Empty;

    [Column("password")]
    [StringLength(64)]
    public string Password { get; set; } = string.Empty;

    [Column("role")] [StringLength(20)] public string Role { get; set; } = string.Empty;

    [Column("salted")] [StringLength(16)] public string Salted { get; set; } = string.Empty;
    [Column("is_needed_reset_password")][Required] public bool IsNeededResetPassword { get; set; }

    public ICollection<BanquetTableData> CreatedTables { get; set; } = new List<BanquetTableData>();
    public ICollection<GuestData> CreatedGuests { get; set; } = new List<GuestData>();
    public ICollection<GuestSpecialNeedData> CreatedSpecialNeeds { get; set; } = new List<GuestSpecialNeedData>();
    public ICollection<GuestGroupData> CreatedGroups { get; set; } = new List<GuestGroupData>();
}