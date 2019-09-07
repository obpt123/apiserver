using AuthService.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.WebApi
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost()]
        public LoginResult DoLogin([FromBody]LoginInfo login)
        {
            return authService.DoLogin(login);
        }
    }
}
