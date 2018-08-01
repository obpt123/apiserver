using System;

namespace AuthService.Core
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }

    }
    public class LoginInitInfo
    {
        public string Status { get; set; }

    }

    public class LoginResult
    {
        public bool Success { get; set; }

        public string Action { get; set; }

        public object ActionData { get; set; }
    }


}
