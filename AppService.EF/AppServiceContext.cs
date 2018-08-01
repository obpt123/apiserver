using AppService.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.EF
{
    public class AppServiceContext: DbContext, INeedConfig
    {
        public AppServiceContext()
        {

        }
        public AppServiceContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.GetConnectionString(nameof(AppServiceContext)));

        }
        public virtual DbSet<AppInfo> AppInfos { get; set; }
    }
}
