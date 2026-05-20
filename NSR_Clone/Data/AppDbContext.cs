using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NSR_Clone.ObjectFolder;
using NSR_Clone.SampleClass;

namespace NSR_Clone.Data
{
    public class AppDbContext : DbContext
    {        
        public DbSet<BatchClass> Batches { get; set; }
        public DbSet<SampleClass.SampleClass> Samples { get; set; }
        public DbSet<AnalysisObject> Analyses { get; set; }
        public DbSet<CustomerObject> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=batches.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchClass>().HasKey(b => b.DbId);
            modelBuilder.Entity<CustomerObject>().HasKey(c => c.DbId);
            modelBuilder.Entity<AnalysisObject>().HasKey(a => a.DbId);
            modelBuilder.Entity<SampleClass.SampleClass>().HasKey(s => s.DbId);
        }
    }
}