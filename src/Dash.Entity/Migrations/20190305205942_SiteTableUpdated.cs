using Microsoft.EntityFrameworkCore.Migrations;

namespace Dash.Entity.Migrations
{
    public partial class SiteTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Site",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Site");
        }
    }
}
