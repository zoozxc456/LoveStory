﻿// <auto-generated />
using System;
using LoveStory.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    [DbContext(typeof(LoveStoryContext))]
    [Migration("20240828050241_CREATE_TABLE_FOR_WEDDINGGIFTS")]
    partial class CREATE_TABLE_FOR_WEDDINGGIFTS
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoveStory.Infrastructure.Data.BanquetTableData", b =>
                {
                    b.Property<Guid>("BanquetTableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("banquet_table_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<int>("MaxSeatAmount")
                        .HasColumnType("int")
                        .HasColumnName("max_seat_amount");

                    b.Property<int>("MinSeatAmount")
                        .HasColumnType("int")
                        .HasColumnName("min_seat_amount");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.Property<string>("TableAlias")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("table_alias");

                    b.HasKey("BanquetTableId");

                    b.HasIndex("CreatorId");

                    b.ToTable("banquet_tables");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestAttendanceData", b =>
                {
                    b.Property<Guid>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("attendance_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("ArrivalAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("arrival_at");

                    b.Property<Guid?>("GuestId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest_id");

                    b.HasKey("AttendanceId");

                    b.HasIndex("GuestId")
                        .IsUnique()
                        .HasFilter("[guest_id] IS NOT NULL");

                    b.ToTable("guest_attendances");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestData", b =>
                {
                    b.Property<Guid>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<Guid?>("GuestGroupId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest_group");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("guest_name");

                    b.Property<string>("GuestRelationship")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("guest_relationship");

                    b.Property<bool>("IsAttended")
                        .HasColumnType("bit")
                        .HasColumnName("is_attended");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.Property<Guid?>("SeatLocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("seat_location");

                    b.HasKey("GuestId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GuestGroupId");

                    b.HasIndex("SeatLocationId");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestGroupData", b =>
                {
                    b.Property<Guid>("GuestGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest_group_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<string>("GuestGroupName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("guest_group_name");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.HasKey("GuestGroupId");

                    b.HasIndex("CreatorId");

                    b.ToTable("guest_groups");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestSpecialNeedData", b =>
                {
                    b.Property<Guid>("SpecialNeedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("special_need_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest_id");

                    b.Property<string>("SpecialNeedContent")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("special_need_content");

                    b.HasKey("SpecialNeedId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GuestId");

                    b.ToTable("guest_special_needs");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.UserData", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<bool>("IsNeededResetPassword")
                        .HasColumnType("bit")
                        .HasColumnName("is_needed_reset_password");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("role");

                    b.Property<string>("Salted")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("salted");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.HasIndex("CreatorId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.WeddingGiftData", b =>
                {
                    b.Property<Guid>("WeddingGiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("wedding_gift_id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<string>("GiftType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("gift_type");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guest");

                    b.Property<string>("Remark")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.HasKey("WeddingGiftId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GuestId")
                        .IsUnique();

                    b.ToTable("wedding_gifts");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.BanquetTableData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedTables")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestAttendanceData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.GuestData", "Guest")
                        .WithOne("GuestAttendance")
                        .HasForeignKey("LoveStory.Infrastructure.Data.GuestAttendanceData", "GuestId");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedGuests")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LoveStory.Infrastructure.Data.GuestGroupData", "GuestGroup")
                        .WithMany("Guests")
                        .HasForeignKey("GuestGroupId");

                    b.HasOne("LoveStory.Infrastructure.Data.BanquetTableData", "SeatLocation")
                        .WithMany("Guests")
                        .HasForeignKey("SeatLocationId");

                    b.Navigation("Creator");

                    b.Navigation("GuestGroup");

                    b.Navigation("SeatLocation");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestGroupData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedGroups")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestSpecialNeedData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedSpecialNeeds")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LoveStory.Infrastructure.Data.GuestData", "Guest")
                        .WithMany("SpecialNeeds")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.UserData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.WeddingGiftData", b =>
                {
                    b.HasOne("LoveStory.Infrastructure.Data.UserData", "Creator")
                        .WithMany("CreatedWeddingGifts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LoveStory.Infrastructure.Data.GuestData", "Guest")
                        .WithOne("WeddingGift")
                        .HasForeignKey("LoveStory.Infrastructure.Data.WeddingGiftData", "GuestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.BanquetTableData", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestData", b =>
                {
                    b.Navigation("GuestAttendance");

                    b.Navigation("SpecialNeeds");

                    b.Navigation("WeddingGift");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.GuestGroupData", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("LoveStory.Infrastructure.Data.UserData", b =>
                {
                    b.Navigation("CreatedGroups");

                    b.Navigation("CreatedGuests");

                    b.Navigation("CreatedSpecialNeeds");

                    b.Navigation("CreatedTables");

                    b.Navigation("CreatedUsers");

                    b.Navigation("CreatedWeddingGifts");
                });
#pragma warning restore 612, 618
        }
    }
}
