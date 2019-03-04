using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dash.Entity.Migrations
{
    public partial class InitializeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteAddress = table.Column<string>(nullable: true),
                    AccessCode = table.Column<string>(nullable: true),
                    AccessCodeEndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessibleSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteIdId = table.Column<Guid>(nullable: true),
                    AccessibleSiteIdId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibleSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibleSite_Site_AccessibleSiteIdId",
                        column: x => x.AccessibleSiteIdId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessibleSite_Site_SiteIdId",
                        column: x => x.SiteIdId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataShares",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerSiteIdId = table.Column<Guid>(nullable: true),
                    DataShareCode = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataShares_Site_OwnerSiteIdId",
                        column: x => x.OwnerSiteIdId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleSite_AccessibleSiteIdId",
                table: "AccessibleSite",
                column: "AccessibleSiteIdId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleSite_SiteIdId",
                table: "AccessibleSite",
                column: "SiteIdId");

            migrationBuilder.CreateIndex(
                name: "IX_DataShares_OwnerSiteIdId",
                table: "DataShares",
                column: "OwnerSiteIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessibleSite");

            migrationBuilder.DropTable(
                name: "DataShares");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
