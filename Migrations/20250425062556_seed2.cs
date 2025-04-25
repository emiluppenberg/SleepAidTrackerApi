using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoseUnit",
                table: "Supplements",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Sleeps",
                newName: "SleepDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoseDate",
                table: "Doses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DoseDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DoseDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DoseDate",
                value: new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DoseDate",
                value: new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DoseDate",
                value: new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DoseDate",
                value: new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DoseDate",
                value: new DateTime(2025, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DoseDate",
                value: new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseDate",
                table: "Doses");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Supplements",
                newName: "DoseUnit");

            migrationBuilder.RenameColumn(
                name: "SleepDate",
                table: "Sleeps",
                newName: "Date");
        }
    }
}
