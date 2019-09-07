using AuthService.Identity.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Identity.EF
{
    [DbContextClass()]
    public class AuthServiceContext: IdentityDbContext<User, Role, string>
    {
        protected AuthServiceContext()
        {
        }
        public AuthServiceContext(DbContextOptions options):base(options)
        {
        }
    }
}
