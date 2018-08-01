using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Identity.EF.Models
{
    public class User: IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [PersonalData]
        public string FullName { get; set; }

        
    }
}
