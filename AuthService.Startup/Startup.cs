using AuthService;
using AuthService.Identity.EF;
using AuthService.Identity.EF.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

//[ServiceInitAttribute
namespace AuthService
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var options = Configuration.GetConfigOrNew<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(options.SecretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer((op) =>
                    {
                        op.TokenValidationParameters = new TokenValidationParameters
                        {
                            // The signing key must match!
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey = new SymmetricSecurityKey(key),

                            // Validate the JWT Issuer (iss) claim
                            ValidateIssuer = true,
                            ValidIssuer = options.Issuer,

                            // Validate the JWT Audience (aud) claim
                            ValidateAudience = true,
                            ValidAudience = options.Audience,

                            // Validate the token expiry
                            ValidateLifetime = true,

                            // If you want to allow a certain amount of clock drift, set that here:
                            ClockSkew = TimeSpan.Zero

                        };
                    });

        }



    }
}
