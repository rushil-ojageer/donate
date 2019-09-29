using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.DonorService.API.Migrations
{
    public partial class DonationTransactionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DonationDate",
                table: "Donation",
                newName: "TransactionDateTimeUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "DonationDateTimeUtc",
                table: "Donation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MerchantName",
                table: "Donation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionAmount",
                table: "Donation",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionDonationPercentage",
                table: "Donation",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationDateTimeUtc",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "MerchantName",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "TransactionAmount",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "TransactionDonationPercentage",
                table: "Donation");

            migrationBuilder.RenameColumn(
                name: "TransactionDateTimeUtc",
                table: "Donation",
                newName: "DonationDate");
        }
    }
}
