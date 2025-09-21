using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sleepupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinutesOfSleepDisruption",
                table: "Sleeps",
                newName: "DisruptionCount");

            migrationBuilder.RenameColumn(
                name: "HoursOfSleep",
                table: "Sleeps",
                newName: "TotalHours");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Bedtime",
                table: "Sleeps",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<double>(
                name: "BedtimeHR",
                table: "Sleeps",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Waketime",
                table: "Sleeps",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bedtime", "BedtimeHR", "Waketime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), null, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Bedtime", "BedtimeHR", "Waketime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), null, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Bedtime", "BedtimeHR", "Waketime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), null, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Bedtime", "BedtimeHR", "Waketime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), null, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Bedtime", "BedtimeHR", "Waketime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), null, new TimeSpan(0, 0, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bedtime",
                table: "Sleeps");

            migrationBuilder.DropColumn(
                name: "BedtimeHR",
                table: "Sleeps");

            migrationBuilder.DropColumn(
                name: "Waketime",
                table: "Sleeps");

            migrationBuilder.RenameColumn(
                name: "TotalHours",
                table: "Sleeps",
                newName: "HoursOfSleep");

            migrationBuilder.RenameColumn(
                name: "DisruptionCount",
                table: "Sleeps",
                newName: "MinutesOfSleepDisruption");
        }
    }
}
