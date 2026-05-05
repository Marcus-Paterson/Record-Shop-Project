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
    }
}
