using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeRadar.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CafeId = table.Column<int>(type: "int", nullable: false),
                    NoiseValue = table.Column<int>(type: "int", nullable: false),
                    NoiseLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LightValue = table.Column<int>(type: "int", nullable: false),
                    LightLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasuredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Address", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Lacina 5, Mostar 88000 Bosnia and Herzegovina", null, null, "Mood Bar - Night & Lounge Bar" },
                    { 2, "Zalik 12, Mostar 88000 Bosnia and Herzegovina", null, null, "Caffe Pizzeria Urban Forest" },
                    { 3, "Fejiceva Bb, Mostar 88000 Bosnia and Herzegovina", null, null, "Fabrika Coffee Mostar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_CafeId",
                table: "Measurements",
                column: "CafeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Cafes");
        }
    }
}
