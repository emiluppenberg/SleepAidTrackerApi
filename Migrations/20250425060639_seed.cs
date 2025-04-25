using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sleeps",
                columns: new[] { "Id", "Date", "HoursOfSleep", "UserId" },
                values: new object[,]
                {
                    { 5, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.5, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 6, new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6.0, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 7, new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.5, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 8, new DateTime(2025, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.5, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 9, new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, "31e2241d-da6a-4825-883e-6b6bd0e37db0" }
                });

            migrationBuilder.InsertData(
                table: "Supplements",
                columns: new[] { "Id", "DoseUnit", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "pills", "L-Theanine 250mg", "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 2, "grams", "Glycine", "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 3, "minutes", "Light Hygiene", "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 4, "pills", "Magnesium L-Threonate", "31e2241d-da6a-4825-883e-6b6bd0e37db0" }
                });

            migrationBuilder.InsertData(
                table: "Doses",
                columns: new[] { "Id", "DoseAmount", "SleepId", "SupplementId", "UserId" },
                values: new object[,]
                {
                    { 10, 5.0, 5, 2, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 11, 30.0, 5, 3, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 12, 5.0, 6, 2, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 13, 30.0, 6, 3, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 14, 5.0, 7, 2, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 15, 60.0, 7, 3, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 16, 30.0, 8, 3, "31e2241d-da6a-4825-883e-6b6bd0e37db0" },
                    { 17, 30.0, 9, 3, "31e2241d-da6a-4825-883e-6b6bd0e37db0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Supplements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Supplements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sleeps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Supplements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Supplements",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
