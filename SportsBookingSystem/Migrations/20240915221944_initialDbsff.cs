using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialDbsff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursEnd",
                table: "DateHours");

            migrationBuilder.DropColumn(
                name: "HoursStart",
                table: "DateHours");

            migrationBuilder.CreateTable(
                name: "TimeRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoursStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoursEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    DateHoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRanges_DateHours_DateHoursId",
                        column: x => x.DateHoursId,
                        principalTable: "DateHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeRanges_DateHoursId",
                table: "TimeRanges",
                column: "DateHoursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeRanges");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoursEnd",
                table: "DateHours",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoursStart",
                table: "DateHours",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
