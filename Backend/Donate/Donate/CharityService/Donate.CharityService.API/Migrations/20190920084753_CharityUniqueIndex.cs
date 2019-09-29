using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.CharityService.API.Migrations
{
    public partial class CharityUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Charity_CharityIdentifier",
                table: "Charity",
                column: "CharityIdentifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Charity_CharityIdentifier",
                table: "Charity");
        }
    }
}
