using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_guest_and_banquentTable_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_banquet_tables_seat_location",
                table: "guests");

            migrationBuilder.DropForeignKey(
                name: "FK_guests_users_UserDataUserId",
                table: "guests");

            migrationBuilder.DropIndex(
                name: "IX_guests_UserDataUserId",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "UserDataUserId",
                table: "guests");

            migrationBuilder.AlterColumn<Guid>(
                name: "seat_location",
                table: "guests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_banquet_tables_seat_location",
                table: "guests",
                column: "seat_location",
                principalTable: "banquet_tables",
                principalColumn: "banquet_table_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_banquet_tables_seat_location",
                table: "guests");

            migrationBuilder.AlterColumn<Guid>(
                name: "seat_location",
                table: "guests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDataUserId",
                table: "guests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_guests_UserDataUserId",
                table: "guests",
                column: "UserDataUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_banquet_tables_seat_location",
                table: "guests",
                column: "seat_location",
                principalTable: "banquet_tables",
                principalColumn: "banquet_table_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_guests_users_UserDataUserId",
                table: "guests",
                column: "UserDataUserId",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
