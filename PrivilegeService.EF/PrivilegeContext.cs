using Microsoft.EntityFrameworkCore;
using PrivilegeService.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrivilegeService.EF
{
    public class PrivilegeContext:DbContext
    {
        public DbSet<FunctionInfo> Functions { get; set; }
        public DbSet<RoleFunction> RoleFunctions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(this.GetConfig("EFContext:KeyValue:Connection"));

        }
    }
}
