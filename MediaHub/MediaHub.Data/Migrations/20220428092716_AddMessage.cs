using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaHub.Data.Migrations
{
    public partial class AddMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderUserId = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "NVARCHAR(450)", nullable: true),
                    TimeReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.TimeSent);
                    table.ForeignKey(
                        name: "FK_Message_UserProfile_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_UserProfile_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverUserId",
                table: "Message",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderUserId",
                table: "Message",
                column: "SenderUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MediaRating",
                newName: "MovieIdentifier");
        }
    }
}
