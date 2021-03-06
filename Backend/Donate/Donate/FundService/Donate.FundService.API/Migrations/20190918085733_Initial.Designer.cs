﻿// <auto-generated />
using System;
using Donate.FundService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Donate.FundService.API.Migrations
{
    [DbContext(typeof(FundContext))]
    [Migration("20190918085733_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Donate.FundService.Data.Entities.DonorTransactionSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("DonorIdentifier");

                    b.Property<string>("FinancialInstitution")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Identifier")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("TransactionSourceIdentifier");

                    b.HasKey("Id");

                    b.ToTable("DonorTransactionSource");
                });
#pragma warning restore 612, 618
        }
    }
}
