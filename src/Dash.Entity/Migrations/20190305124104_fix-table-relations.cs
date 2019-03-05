using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dash.Entity.Migrations
{
    public partial class fixtablerelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleSite_Site_AccessibleSiteIdId",
                table: "AccessibleSite");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleSite_Site_SiteIdId",
                table: "AccessibleSite");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShares_Site_OwnerSiteIdId",
                table: "DataShares");

            migrationBuilder.RenameColumn(
                name: "OwnerSiteIdId",
                table: "DataShares",
                newName: "OwnerSiteId");

            migrationBuilder.RenameIndex(
                name: "IX_DataShares_OwnerSiteIdId",
                table: "DataShares",
                newName: "IX_DataShares_OwnerSiteId");

            migrationBuilder.RenameColumn(
                name: "SiteIdId",
                table: "AccessibleSite",
                newName: "SiteId");

            migrationBuilder.RenameColumn(
                name: "AccessibleSiteIdId",
                table: "AccessibleSite",
                newName: "AccessibleSiteId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibleSite_SiteIdId",
                table: "AccessibleSite",
                newName: "IX_AccessibleSite_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibleSite_AccessibleSiteIdId",
                table: "AccessibleSite",
                newName: "IX_AccessibleSite_AccessibleSiteId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Site",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Site",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DataShares",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "DataShares",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AccessibleSite",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AccessibleSite",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleSite_Site_AccessibleSiteId",
                table: "AccessibleSite",
                column: "AccessibleSiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleSite_Site_SiteId",
                table: "AccessibleSite",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataShares_Site_OwnerSiteId",
                table: "DataShares",
                column: "OwnerSiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleSite_Site_AccessibleSiteId",
                table: "AccessibleSite");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleSite_Site_SiteId",
                table: "AccessibleSite");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShares_Site_OwnerSiteId",
                table: "DataShares");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DataShares");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "DataShares");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AccessibleSite");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AccessibleSite");

            migrationBuilder.RenameColumn(
                name: "OwnerSiteId",
                table: "DataShares",
                newName: "OwnerSiteIdId");

            migrationBuilder.RenameIndex(
                name: "IX_DataShares_OwnerSiteId",
                table: "DataShares",
                newName: "IX_DataShares_OwnerSiteIdId");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "AccessibleSite",
                newName: "SiteIdId");

            migrationBuilder.RenameColumn(
                name: "AccessibleSiteId",
                table: "AccessibleSite",
                newName: "AccessibleSiteIdId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibleSite_SiteId",
                table: "AccessibleSite",
                newName: "IX_AccessibleSite_SiteIdId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibleSite_AccessibleSiteId",
                table: "AccessibleSite",
                newName: "IX_AccessibleSite_AccessibleSiteIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleSite_Site_AccessibleSiteIdId",
                table: "AccessibleSite",
                column: "AccessibleSiteIdId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleSite_Site_SiteIdId",
                table: "AccessibleSite",
                column: "SiteIdId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataShares_Site_OwnerSiteIdId",
                table: "DataShares",
                column: "OwnerSiteIdId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
