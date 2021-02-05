using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPI_Database.Migrations
{
    public partial class EfficiencyImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true,
                filter: "[Token] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Token",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                table: "Teams");
        }
    }
}
