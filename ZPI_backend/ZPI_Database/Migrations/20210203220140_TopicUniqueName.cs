using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPI_Database.Migrations
{
    public partial class TopicUniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Topics_Name",
                table: "Topics",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topics_Name",
                table: "Topics");
        }
    }
}
