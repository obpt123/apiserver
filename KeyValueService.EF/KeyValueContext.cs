using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class KeyValueContext:DbContext,INeedConfig
    {
        public KeyValueContext():base()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.GetConfig("EFContext:KeyValue:Connection"));
          
        }
        public DbSet<KeyValue> KeyValues { get; set; }
    }
}
