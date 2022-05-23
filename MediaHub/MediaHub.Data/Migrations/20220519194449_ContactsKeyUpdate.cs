using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace MediaHub.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ContactsKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactId",
                table: "Contact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserProfile_ContactId",
                table: "Contact",
                column: "ContactId",
                principalTable: "UserProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserProfile_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserProfile_ContactId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserProfile_UserId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_UserId",
                table: "Contact");
        }
    }
}
