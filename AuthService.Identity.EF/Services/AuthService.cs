using AuthService.Core;
using AuthService.Identity.EF.Config;
using AuthService.Identity.EF.Models;
using IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Identity.EF.Services
{
    [ServiceImplClass(typeof(IAuthService))]
    public class AuthService : IAuthService
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<Role> roleManager;
        JwtOptions jwtOptions;
        public LoginResult DoLogin(LoginInfo login)
        {
           
            
            var user = userManager.FindByNameAsync(login.UserName).Result;
            if (user == null)
            {
                return new LoginResult()
                {
                    Token = string.Empty
                };
            }
           

            var signInResult = signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: true, lockoutOnFailure: true).Result;
            if (signInResult.Succeeded)
            {
                return new LoginResult()
                {
                    Token = CreateJwtTokenAsync(user,userManager,roleManager).Result
                };
            }
            else
            {
                return new LoginResult()
                {
                    Success=false,
                    Token = string.Empty
                };
            }


        }

        private async Task<String> CreateJwtTokenAsync(User user,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            // Create JWT claims
            var claims = new List<Claim>(new[]
            {
                // Issuer
                new Claim(JwtClaimTypes.Issuer, jwtOptions.Issuer),   

                // UserName
                new Claim(JwtClaimTypes.Name, user.UserName),

                new Claim(JwtClaimTypes.NickName, user.NickName),

                // Email is unique
                new Claim(JwtClaimTypes.Email, user.Email),        

                // Unique Id for all Jwt tokes
                new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString()), 

                // Issued at
                new Claim(JwtClaimTypes.IssuedAt, DateTime.Now.AddDays(10).ToString())
              });

            // Add userclaims from storage
            claims.AddRange(await userManager.GetClaimsAsync(user));

            // Add user role, they are converted to claims
            var roleNames = await userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                // Find IdentityRole by name
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    // Convert Identity to claim and add 
                    var roleClaim = new Claim(JwtClaimTypes.Role, role.Name, ClaimValueTypes.String, jwtOptions.Issuer);
                    claims.Add(roleClaim);

                    // Add claims belonging to the role
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }
            var key = Encoding.ASCII.GetBytes(jwtOptions.SecretKey);
            // Prepare Jwt Token
            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(jwtOptions.ExpireMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            // Serialize token
           return new JwtSecurityTokenHandler().WriteToken(jwt);


        }
    }
}
