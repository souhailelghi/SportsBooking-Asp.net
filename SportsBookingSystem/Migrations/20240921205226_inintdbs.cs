using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class inintdbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdList",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdList",
                table: "Bookings");
        }
    }
}
