using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateHours_Facilities_FacilityId",
                table: "DateHours");

            migrationBuilder.DropIndex(
                name: "IX_DateHours_FacilityId",
                table: "DateHours");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "DateHours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "DateHours");

            migrationBuilder.CreateIndex(
                name: "IX_DateHours_FacilityId",
                table: "DateHours",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DateHours_Facilities_FacilityId",
                table: "DateHours",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
