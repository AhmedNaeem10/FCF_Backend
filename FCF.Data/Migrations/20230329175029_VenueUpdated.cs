using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCF.Data.Migrations
{
    /// <inheritdoc />
    public partial class VenueUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Venues");
        }
    }
}
