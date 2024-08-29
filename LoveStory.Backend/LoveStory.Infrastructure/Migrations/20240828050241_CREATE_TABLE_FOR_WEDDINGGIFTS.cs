using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveStory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_TABLE_FOR_WEDDINGGIFTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wedding_gifts",
                columns: table => new
                {
                    wedding_gift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    gift_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    guest = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wedding_gifts", x => x.wedding_gift_id);
                    table.ForeignKey(
                        name: "FK_wedding_gifts_guests_guest",
                        column: x => x.guest,
                        principalTable: "guests",
                        principalColumn: "guest_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_wedding_gifts_users_creator",
                        column: x => x.creator,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wedding_gifts_creator",
                table: "wedding_gifts",
                column: "creator");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_gifts_guest",
                table: "wedding_gifts",
                column: "guest",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wedding_gifts");
        }
    }
}
