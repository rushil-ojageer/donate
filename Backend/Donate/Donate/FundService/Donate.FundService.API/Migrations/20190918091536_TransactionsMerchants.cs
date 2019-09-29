using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donate.FundService.API.Migrations
{
    public partial class TransactionsMerchants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "DonorTransactionSource",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalMerchantIdentifier = table.Column<string>(nullable: false),
                    MerchantName = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionIdentifier = table.Column<Guid>(nullable: false),
                    ExternalTransactionIdentifier = table.Column<string>(nullable: false),
                    DonorTransactionSourceId = table.Column<long>(nullable: false),
                    MerchantId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: false),
                    TransactionDateTimeUtc = table.Column<DateTime>(nullable: false),
                    ReceivedDateTimeUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_DonorTransactionSource_DonorTransactionSourceId",
                        column: x => x.DonorTransactionSourceId,
                        principalTable: "DonorTransactionSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DonorTransactionSourceId",
                table: "Transaction",
                column: "DonorTransactionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MerchantId",
                table: "Transaction",
                column: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "DonorTransactionSource",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);
        }
    }
}
