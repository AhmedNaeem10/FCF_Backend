using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCF.Data.Migrations
{
    /// <inheritdoc />
    public partial class tournamentColumnsRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Scheduled",
                table: "Tournaments",
                newName: "ScheduledAt");

            migrationBuilder.RenameColumn(
                name: "DateTimeCreation",
                table: "Tournaments",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduledAt",
                table: "Tournaments",
                newName: "Scheduled");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tournaments",
                newName: "DateTimeCreation");
        }
    }
}
