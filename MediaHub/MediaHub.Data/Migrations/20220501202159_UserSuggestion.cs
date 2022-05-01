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
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IgnoreSuggestion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSuggestion", x => new { x.UserId1, x.UserId2 });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSuggestion");
        }
    }
}
