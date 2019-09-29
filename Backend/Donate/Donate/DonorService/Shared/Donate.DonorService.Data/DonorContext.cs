using Donate.DonorService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.Data
{
    public class DonorContext : DbContext
    {
        public DonorContext(DbContextOptions<DonorContext> options) : base(options)
        {

        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<DonorCharity> DonorCharities { get; set; }
        public DbSet<DonorCharityProportion> DonorCharityProportions { get; set; }
        public DbSet<TransactionSource> TransactionSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor>().ToTable("Donor");
            modelBuilder.Entity<Donation>().ToTable("Donation");
            modelBuilder.Entity<Charity>().ToTable("Charity");
            modelBuilder.Entity<DonorCharity>().ToTable("DonorCharity");
            modelBuilder.Entity<DonorCharityProportion>().ToTable("DonorCharityProportion");
            modelBuilder.Entity<TransactionSource>().ToTable("TransactionSource");
        }
    }
}
