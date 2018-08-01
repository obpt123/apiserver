using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Identity.EF.Models
{
    public class Role: IdentityRole<string>
    {
        public Role()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
