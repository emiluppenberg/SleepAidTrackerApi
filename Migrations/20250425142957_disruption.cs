using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class disruption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MinutesOfSleepDisruption",
                table: "Sleeps",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 5,
                column: "MinutesOfSleepDisruption",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 6,
                column: "MinutesOfSleepDisruption",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 7,
                column: "MinutesOfSleepDisruption",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 8,
                column: "MinutesOfSleepDisruption",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 9,
                column: "MinutesOfSleepDisruption",
                value: 5.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesOfSleepDisruption",
                table: "Sleeps");
        }
    }
}
