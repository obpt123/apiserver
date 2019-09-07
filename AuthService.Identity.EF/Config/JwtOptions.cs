using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Identity.EF.Config
{
    [ConfigClass(nameof(JwtOptions))]
    public class JwtOptions
    {
        public string Issuer { get; set; } = "api";
        public string Audience { get; set; } = "api.user";
        public string SecretKey { get; set; } = nameof(JwtOptions);
        public int ExpireMinutes { get; set; } = 60*24*7;
    }
}
