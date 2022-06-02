using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KenKata.WebApp.Migrations
{
    public partial class TeamMemberFirstAndLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TeamMemberProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TeamMemberProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TeamMemberProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TeamMemberProfiles");
        }
    }
}
