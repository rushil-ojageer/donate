using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.CharityService.API.Migrations
{
    public partial class CharityConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "Charity");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Charity",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharityName",
                table: "Charity",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Charity",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Charity");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Charity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharityName",
                table: "Charity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "Charity",
                nullable: true);
        }
    }
}
