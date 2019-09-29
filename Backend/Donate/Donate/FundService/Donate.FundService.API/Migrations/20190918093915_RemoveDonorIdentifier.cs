using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.FundService.API.Migrations
{
    public partial class RemoveDonorIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonorIdentifier",
                table: "DonorTransactionSource");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DonorIdentifier",
                table: "DonorTransactionSource",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
