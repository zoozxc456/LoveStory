using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "banquet_tables",
                columns: table => new
                {
                    banquet_table_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    table_alias = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    max_seat_amount = table.Column<int>(type: "int", nullable: false),
                    min_seat_amount = table.Column<int>(type: "int", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banquet_tables", x => x.banquet_table_id);
                    table.ForeignKey(
                        name: "FK_banquet_tables_users_creator",
                        column: x => x.creator,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guest_groups",
                columns: table => new
                {
                    guest_group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    guest_group_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDataUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest_groups", x => x.guest_group_id);
                    table.ForeignKey(
                        name: "FK_guest_groups_users_UserDataUserId",
                        column: x => x.UserDataUserId,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_guest_groups_users_creator",
                        column: x => x.creator,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    guest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    guest_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    guest_relationship = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    guest_group = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    seat_location = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_attended = table.Column<bool>(type: "bit", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDataUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.guest_id);
                    table.ForeignKey(
                        name: "FK_guests_banquet_tables_seat_location",
                        column: x => x.seat_location,
                        principalTable: "banquet_tables",
                        principalColumn: "banquet_table_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guests_guest_groups_guest_group",
                        column: x => x.guest_group,
                        principalTable: "guest_groups",
                        principalColumn: "guest_group_id");
                    table.ForeignKey(
                        name: "FK_guests_users_UserDataUserId",
                        column: x => x.UserDataUserId,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_guests_users_creator",
                        column: x => x.creator,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guest_attendances",
                columns: table => new
                {
                    attendance_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    arrival_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    guest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest_attendances", x => x.attendance_id);
                    table.ForeignKey(
                        name: "FK_guest_attendances_guests_guest_id",
                        column: x => x.guest_id,
                        principalTable: "guests",
                        principalColumn: "guest_id");
                });

            migrationBuilder.CreateTable(
                name: "guest_special_needs",
                columns: table => new
                {
                    special_need_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    special_need_content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    guest_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDataUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest_special_needs", x => x.special_need_id);
                    table.ForeignKey(
                        name: "FK_guest_special_needs_guests_guest_id",
                        column: x => x.guest_id,
                        principalTable: "guests",
                        principalColumn: "guest_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guest_special_needs_users_UserDataUserId",
                        column: x => x.UserDataUserId,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_guest_special_needs_users_creator",
                        column: x => x.creator,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_banquet_tables_creator",
                table: "banquet_tables",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_guest_attendances_guest_id",
                table: "guest_attendances",
                column: "guest_id",
                unique: true,
                filter: "[guest_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_guest_groups_creator",
                table: "guest_groups",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_guest_groups_UserDataUserId",
                table: "guest_groups",
                column: "UserDataUserId");

            migrationBuilder.CreateIndex(
                name: "IX_guest_special_needs_creator",
                table: "guest_special_needs",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_guest_special_needs_guest_id",
                table: "guest_special_needs",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_guest_special_needs_UserDataUserId",
                table: "guest_special_needs",
                column: "UserDataUserId");

            migrationBuilder.CreateIndex(
                name: "IX_guests_creator",
                table: "guests",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_guests_guest_group",
                table: "guests",
                column: "guest_group");

            migrationBuilder.CreateIndex(
                name: "IX_guests_seat_location",
                table: "guests",
                column: "seat_location");

            migrationBuilder.CreateIndex(
                name: "IX_guests_UserDataUserId",
                table: "guests",
                column: "UserDataUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guest_attendances");

            migrationBuilder.DropTable(
                name: "guest_special_needs");

            migrationBuilder.DropTable(
                name: "guests");

            migrationBuilder.DropTable(
                name: "banquet_tables");

            migrationBuilder.DropTable(
                name: "guest_groups");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
