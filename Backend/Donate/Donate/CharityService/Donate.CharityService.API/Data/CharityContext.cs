using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donate.CharityService.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donate.CharityService.API.Data
{
    public class CharityContext : DbContext
    {
        public CharityContext(DbContextOptions<CharityContext> options) : base(options)
        {

        }

        public DbSet<Charity> Charities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charity>()
                .ToTable("Charity")
                .HasIndex(x => x.CharityIdentifier)
                .IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
