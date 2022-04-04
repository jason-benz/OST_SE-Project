using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaHub.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Biography = table.Column<string>(maxLength: 256, nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
