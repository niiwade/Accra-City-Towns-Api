using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccraCity.Application.Migrations
{
    /// <inheritdoc />
    public partial class intial_commit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TownName = table.Column<string>(type: "text", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Population = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NearbyTowns = table.Column<List<string>>(type: "text[]", nullable: false),
                    NotableLandMarks = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Town");
        }
    }
}
