using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSupplementTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class dosesdbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dose_AspNetUsers_UserId",
                table: "Dose");

            migrationBuilder.DropForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose");

            migrationBuilder.DropForeignKey(
                name: "FK_Dose_Supplements_SupplementId",
                table: "Dose");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dose",
                table: "Dose");

            migrationBuilder.RenameTable(
                name: "Dose",
                newName: "Doses");

            migrationBuilder.RenameIndex(
                name: "IX_Dose_UserId",
                table: "Doses",
                newName: "IX_Doses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dose_SupplementId",
                table: "Doses",
                newName: "IX_Doses_SupplementId");

            migrationBuilder.RenameIndex(
                name: "IX_Dose_SleepId",
                table: "Doses",
                newName: "IX_Doses_SleepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doses",
                table: "Doses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_AspNetUsers_UserId",
                table: "Doses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Sleeps_SleepId",
                table: "Doses",
                column: "SleepId",
                principalTable: "Sleeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Supplements_SupplementId",
                table: "Doses",
                column: "SupplementId",
                principalTable: "Supplements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doses_AspNetUsers_UserId",
                table: "Doses");

            migrationBuilder.DropForeignKey(
                name: "FK_Doses_Sleeps_SleepId",
                table: "Doses");

            migrationBuilder.DropForeignKey(
                name: "FK_Doses_Supplements_SupplementId",
                table: "Doses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doses",
                table: "Doses");

            migrationBuilder.RenameTable(
                name: "Doses",
                newName: "Dose");

            migrationBuilder.RenameIndex(
                name: "IX_Doses_UserId",
                table: "Dose",
                newName: "IX_Dose_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Doses_SupplementId",
                table: "Dose",
                newName: "IX_Dose_SupplementId");

            migrationBuilder.RenameIndex(
                name: "IX_Doses_SleepId",
                table: "Dose",
                newName: "IX_Dose_SleepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dose",
                table: "Dose",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_AspNetUsers_UserId",
                table: "Dose",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Sleeps_SleepId",
                table: "Dose",
                column: "SleepId",
                principalTable: "Sleeps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Supplements_SupplementId",
                table: "Dose",
                column: "SupplementId",
                principalTable: "Supplements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
