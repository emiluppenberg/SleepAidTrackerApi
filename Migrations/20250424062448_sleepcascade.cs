using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sleepcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose");

            migrationBuilder.DropForeignKey(
                name: "FK_Sleeps_AspNetUsers_UserId",
                table: "Sleeps");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose",
                column: "SleepId",
                principalTable: "Sleeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sleeps_AspNetUsers_UserId",
                table: "Sleeps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose");

            migrationBuilder.DropForeignKey(
                name: "FK_Sleeps_AspNetUsers_UserId",
                table: "Sleeps");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose",
                column: "SleepId",
                principalTable: "Sleeps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sleeps_AspNetUsers_UserId",
                table: "Sleeps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
