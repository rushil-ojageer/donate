using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.DonorService.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Identifier = table.Column<Guid>(nullable: false),
                    CharityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    IdentityType = table.Column<int>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 20, nullable: false),
                    ContactNumber = table.Column<string>(maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 250, nullable: false),
                    PhysicalAddress = table.Column<string>(nullable: false),
                    PostalAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonorCharity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DonorId = table.Column<long>(nullable: false),
                    CharityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorCharity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorCharity_Charity_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonorCharity_Donor_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fund",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorId = table.Column<long>(nullable: false),
                    ValidFromUtc = table.Column<DateTime>(nullable: false),
                    ValidToUtc = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fund_Donor_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonorCharityProportion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorCharityId = table.Column<long>(nullable: false),
                    DonationPercentage = table.Column<decimal>(nullable: false),
                    ValidFromUtc = table.Column<DateTime>(nullable: false),
                    ValidToUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorCharityProportion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorCharityProportion_DonorCharity_DonorCharityId",
                        column: x => x.DonorCharityId,
                        principalTable: "DonorCharity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorCharityProportionId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(maxLength: 3, nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donation_DonorCharityProportion_DonorCharityProportionId",
                        column: x => x.DonorCharityProportionId,
                        principalTable: "DonorCharityProportion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donation_DonorCharityProportionId",
                table: "Donation",
                column: "DonorCharityProportionId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCharity_CharityId",
                table: "DonorCharity",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCharity_DonorId",
                table: "DonorCharity",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCharityProportion_DonorCharityId",
                table: "DonorCharityProportion",
                column: "DonorCharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fund_DonorId",
                table: "Fund",
                column: "DonorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Fund");

            migrationBuilder.DropTable(
                name: "DonorCharityProportion");

            migrationBuilder.DropTable(
                name: "DonorCharity");

            migrationBuilder.DropTable(
                name: "Charity");

            migrationBuilder.DropTable(
                name: "Donor");
        }
    }
}
