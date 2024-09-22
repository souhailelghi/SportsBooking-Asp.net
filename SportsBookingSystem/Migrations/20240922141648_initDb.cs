using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sports");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Sports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Sports");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Sports",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
