using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class revamp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dose",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Dose_UserId",
                table: "Dose",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_AspNetUsers_UserId",
                table: "Dose",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dose_AspNetUsers_UserId",
                table: "Dose");

            migrationBuilder.DropIndex(
                name: "IX_Dose_UserId",
                table: "Dose");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dose");
        }
    }
}
