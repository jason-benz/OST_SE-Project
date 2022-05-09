using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaHub.Data.Migrations
{
    public partial class FeedItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    ChangedTable = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItem_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedItem_UserId",
                table: "FeedItem",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedItem");
        }
    }
}
