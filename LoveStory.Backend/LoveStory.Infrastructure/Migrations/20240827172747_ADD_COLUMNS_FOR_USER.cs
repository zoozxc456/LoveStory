using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMNS_FOR_USER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "creator",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_creator",
                table: "users",
                column: "creator");

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_creator",
                table: "users",
                column: "creator",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_creator",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_creator",
                table: "users");

            migrationBuilder.DropColumn(
                name: "create_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "creator",
                table: "users");
        }
    }
}
