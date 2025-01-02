using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Creator_For_Guest_Attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guest_attendances_guests_guest_id",
                table: "guest_attendances");

            migrationBuilder.DropIndex(
                name: "IX_guest_attendances_guest_id",
                table: "guest_attendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "guest_id",
                table: "guest_attendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "creator",
                table: "guest_attendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_guest_attendances_creator",
                table: "guest_attendances",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_guest_attendances_guest_id",
                table: "guest_attendances",
                column: "guest_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_guest_attendances_guests_guest_id",
                table: "guest_attendances",
                column: "guest_id",
                principalTable: "guests",
                principalColumn: "guest_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_guest_attendances_users_creator",
                table: "guest_attendances",
                column: "creator",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guest_attendances_guests_guest_id",
                table: "guest_attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_guest_attendances_users_creator",
                table: "guest_attendances");

            migrationBuilder.DropIndex(
                name: "IX_guest_attendances_creator",
                table: "guest_attendances");

            migrationBuilder.DropIndex(
                name: "IX_guest_attendances_guest_id",
                table: "guest_attendances");

            migrationBuilder.DropColumn(
                name: "creator",
                table: "guest_attendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "guest_id",
                table: "guest_attendances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_guest_attendances_guest_id",
                table: "guest_attendances",
                column: "guest_id",
                unique: true,
                filter: "[guest_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_guest_attendances_guests_guest_id",
                table: "guest_attendances",
                column: "guest_id",
                principalTable: "guests",
                principalColumn: "guest_id");
        }
    }
}
