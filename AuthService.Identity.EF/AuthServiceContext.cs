using AuthService.Identity.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Identity.EF
{
    public class AuthServiceContext: IdentityDbContext<User, Role, string>
    {
        public AuthServiceContext()
        {
        }
        public AuthServiceContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
