using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    [DbContextClass]
    public class KeyValueContext:DbContext
    {
        protected KeyValueContext()
        {
            
        }
        public KeyValueContext(DbContextOptions option) :base(option)
        {
            //this.Model.FindEntityType("").GetKeys()
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new KeyValueConfiguration());
            


            base.OnModelCreating(modelBuilder);
            
        }
        public DbSet<KeyValue> KeyValues { get; set; }
    }



  
}
