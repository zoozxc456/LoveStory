using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_creator_attribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guest_groups_users_UserDataUserId",
                table: "guest_groups");

            migrationBuilder.DropForeignKey(
                name: "FK_guest_special_needs_users_UserDataUserId",
                table: "guest_special_needs");

            migrationBuilder.DropIndex(
                name: "IX_guest_special_needs_UserDataUserId",
                table: "guest_special_needs");

            migrationBuilder.DropIndex(
                name: "IX_guest_groups_UserDataUserId",
                table: "guest_groups");

            migrationBuilder.DropColumn(
                name: "UserDataUserId",
                table: "guest_special_needs");

            migrationBuilder.DropColumn(
                name: "UserDataUserId",
                table: "guest_groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserDataUserId",
                table: "guest_special_needs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDataUserId",
                table: "guest_groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_guest_special_needs_UserDataUserId",
                table: "guest_special_needs",
                column: "UserDataUserId");

            migrationBuilder.CreateIndex(
                name: "IX_guest_groups_UserDataUserId",
                table: "guest_groups",
                column: "UserDataUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_guest_groups_users_UserDataUserId",
                table: "guest_groups",
                column: "UserDataUserId",
                principalTable: "users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_guest_special_needs_users_UserDataUserId",
                table: "guest_special_needs",
                column: "UserDataUserId",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
