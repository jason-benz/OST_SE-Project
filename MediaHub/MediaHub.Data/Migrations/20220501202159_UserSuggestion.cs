using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaHub.Data.Migrations
{
    public partial class UserSuggestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSuggestion",
                columns: table => new
                {
                    UserId1 = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    UserId2 = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    IgnoreSuggestion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSuggestion", x => new { x.UserId1, x.UserId2 });
                    table.ForeignKey(
                        name: "FK_UserSuggestion_UserProfile_UserId1",
                        column: x => x.UserId1,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserSuggestion_UserProfile_UserId2",
                        column: x => x.UserId2,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSuggestion_UserId2",
                table: "UserSuggestion",
                column: "UserId2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSuggestion");
        }
    }
}
