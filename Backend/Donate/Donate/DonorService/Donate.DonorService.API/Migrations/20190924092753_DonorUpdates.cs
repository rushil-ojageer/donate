using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.DonorService.API.Migrations
{
    public partial class DonorUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhysicalAddress",
                table: "Donor");

            migrationBuilder.DropColumn(
                name: "PostalAddress",
                table: "Donor");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Charity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Charity",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Charity");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Charity");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalAddress",
                table: "Donor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalAddress",
                table: "Donor",
                nullable: false,
                defaultValue: "");
        }
    }
}
