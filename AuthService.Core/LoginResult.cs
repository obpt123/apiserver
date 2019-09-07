using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Core
{
    public class LoginResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
