using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dash.Entity.Migrations
{
    public partial class DataShareTableAddExpireColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "DataShares",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "DataShares",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "DataShares");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "DataShares",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
