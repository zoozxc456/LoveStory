using LoveStory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoveStory.Infrastructure.Contexts;

public class LoveStoryContext : DbContext
{
    public LoveStoryContext()
    {
    }

    public LoveStoryContext(DbContextOptions<LoveStoryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<GuestData>(e =>
        {
            e.HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GuestSpecialNeedData>(e =>
        {
            e.HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GuestGroupData>(e =>
        {
            e.HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SetGuidDefaultValue(modelBuilder.Entity<BanquetTableData>().Property(e => e.BanquetTableId));
        SetGuidDefaultValue(modelBuilder.Entity<GuestAttendanceData>().Property(e => e.AttendanceId));
        SetGuidDefaultValue(modelBuilder.Entity<GuestData>().Property(e => e.GuestId));
        SetGuidDefaultValue(modelBuilder.Entity<GuestGroupData>().Property(e => e.GuestGroupId));
        SetGuidDefaultValue(modelBuilder.Entity<GuestSpecialNeedData>().Property(e => e.SpecialNeedId));
        SetGuidDefaultValue(modelBuilder.Entity<UserData>().Property(e => e.UserId));
    }

    private static void SetGuidDefaultValue(PropertyBuilder<Guid> builder) => builder.HasDefaultValueSql("NEWID()");

    public DbSet<BanquetTableData> BanquetTables { get; set; }
    public DbSet<GuestAttendanceData> GuestAttendances { get; set; }
    public DbSet<GuestData> Guests { get; set; }
    public DbSet<GuestGroupData> GuestGroups { get; set; }
    public DbSet<GuestSpecialNeedData> GuestSpecialNeeds { get; set; }
    public DbSet<UserData> Users { get; set; }
}