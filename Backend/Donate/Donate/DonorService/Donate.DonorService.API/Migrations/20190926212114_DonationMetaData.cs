using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.DonorService.API.Migrations
{
    public partial class DonationMetaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TransactionDonationPercentage",
                table: "Donor",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionIdentifier",
                table: "Donation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDonationPercentage",
                table: "Donor");

            migrationBuilder.DropColumn(
                name: "TransactionIdentifier",
                table: "Donation");
        }
    }
}
