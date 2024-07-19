using LoveStory.Infrastructure.Configurations;
using LoveStory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LoveStory.Infrastructure.Contexts;

public class LoveStoryContext(InfrastructureConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.ConnectionString);
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
    }

    public DbSet<BanquetTableData> BanquetTables { get; set; }
    public DbSet<GuestAttendanceData> GuestAttendances { get; set; }
    public DbSet<GuestData> Guests { get; set; }
    public DbSet<GuestGroupData> GuestGroups { get; set; }
    public DbSet<GuestSpecialNeedData> GuestSpecialNeeds { get; set; }
    public DbSet<UserData> Users { get; set; }
}