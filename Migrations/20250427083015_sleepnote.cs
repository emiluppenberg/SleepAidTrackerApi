using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sleepnote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Sleeps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 5,
                column: "Note",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 6,
                column: "Note",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 7,
                column: "Note",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 8,
                column: "Note",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 9,
                column: "Note",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Sleeps");
        }
    }
}
