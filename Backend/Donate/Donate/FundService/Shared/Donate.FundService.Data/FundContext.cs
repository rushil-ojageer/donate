using System;
using Donate.FundService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donate.FundService.Data
{
    public class FundContext : DbContext
    {
        public FundContext(DbContextOptions<FundContext> options) : base(options)
        {

        }

        public DbSet<DonorTransactionSource> DonorTransactionSources { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonorTransactionSource>().ToTable("DonorTransactionSource");
            modelBuilder.Entity<Merchant>().ToTable("Merchant");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }
    }
}
