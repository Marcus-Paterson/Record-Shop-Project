using RecordShopProject.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RecordShopProject
{
    public class RecordShopDBContext : DbContext
    {

        public DbSet<Record> Records { get; set; }


        public RecordShopDBContext(DbContextOptions<RecordShopDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasKey(r => r.RecordId);

            modelBuilder.Entity<Record>()
                .Property(r => r.RecordId)
                .ValueGeneratedOnAdd();
        }
    }
}
