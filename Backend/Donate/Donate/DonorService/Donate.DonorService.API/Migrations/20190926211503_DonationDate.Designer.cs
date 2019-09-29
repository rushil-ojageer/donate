﻿// <auto-generated />
using System;
using Donate.DonorService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Donate.DonorService.API.Migrations
{
    [DbContext(typeof(DonorContext))]
    [Migration("20190926211503_DonationDate")]
    partial class DonationDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Donate.DonorService.Data.Entities.Charity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CharityName")
                        .IsRequired();

                    b.Property<Guid>("Identifier");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Charity");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.Donation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasMaxLength(3);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<DateTime>("DonationDate");

                    b.Property<long>("DonorCharityProportionId");

                    b.HasKey("Id");

                    b.HasIndex("DonorCharityProportionId");

                    b.ToTable("Donation");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.Donor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("IdentityType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Donor");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.DonorCharity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CharityId");

                    b.Property<long>("DonorId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.HasIndex("CharityId");

                    b.HasIndex("DonorId");

                    b.ToTable("DonorCharity");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.DonorCharityProportion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DonationPercentage");

                    b.Property<long>("DonorCharityId");

                    b.Property<DateTime>("ValidFromUtc");

                    b.Property<DateTime>("ValidToUtc");

                    b.HasKey("Id");

                    b.HasIndex("DonorCharityId");

                    b.ToTable("DonorCharityProportion");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.TransactionSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DonorId");

                    b.Property<string>("FinancialInstitution")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Identifier")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("TransactionSourceIdentifier");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.ToTable("TransactionSource");
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.Donation", b =>
                {
                    b.HasOne("Donate.DonorService.Data.Entities.DonorCharityProportion", "DonorCharityProportion")
                        .WithMany("Donations")
                        .HasForeignKey("DonorCharityProportionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.DonorCharity", b =>
                {
                    b.HasOne("Donate.DonorService.Data.Entities.Charity", "Charity")
                        .WithMany("DonorCharities")
                        .HasForeignKey("CharityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Donate.DonorService.Data.Entities.Donor", "Donor")
                        .WithMany("DonorCharities")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.DonorCharityProportion", b =>
                {
                    b.HasOne("Donate.DonorService.Data.Entities.DonorCharity", "DonorCharity")
                        .WithMany("DonorCharityProportions")
                        .HasForeignKey("DonorCharityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Donate.DonorService.Data.Entities.TransactionSource", b =>
                {
                    b.HasOne("Donate.DonorService.Data.Entities.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
