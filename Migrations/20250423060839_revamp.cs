using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class revamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseDate",
                table: "Dose");

            migrationBuilder.AddColumn<int>(
                name: "SleepId",
                table: "Dose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sleeps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoursOfSleep = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleeps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sleeps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dose_SleepId",
                table: "Dose",
                column: "SleepId");

            migrationBuilder.CreateIndex(
                name: "IX_Sleeps_UserId",
                table: "Sleeps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose",
                column: "SleepId",
                principalTable: "Sleeps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose");

            migrationBuilder.DropTable(
                name: "Sleeps");

            migrationBuilder.DropIndex(
                name: "IX_Dose_SleepId",
                table: "Dose");

            migrationBuilder.DropColumn(
                name: "SleepId",
                table: "Dose");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoseDate",
                table: "Dose",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
