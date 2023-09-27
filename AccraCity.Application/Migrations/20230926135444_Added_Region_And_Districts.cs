using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccraCity.Application.Migrations
{
    /// <inheritdoc />
    public partial class Added_Region_And_Districts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Town");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Town");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "Town",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Town",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictName = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Town_DistrictId",
                table: "Town",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_RegionId",
                table: "Town",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Town_Districts_DistrictId",
                table: "Town",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Town_Regions_RegionId",
                table: "Town",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Town_Districts_DistrictId",
                table: "Town");

            migrationBuilder.DropForeignKey(
                name: "FK_Town_Regions_RegionId",
                table: "Town");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Town_DistrictId",
                table: "Town");

            migrationBuilder.DropIndex(
                name: "IX_Town_RegionId",
                table: "Town");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Town");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Town");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Town",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Town",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
