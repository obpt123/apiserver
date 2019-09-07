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
        /// <summary>
        /// 用户昵称
        /// </summary>
        [PersonalData]
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [PersonalData]
        public string Avatar { get; set; }
    }
}
