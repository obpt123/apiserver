using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Core
{
    public interface IAuthService
    {
        LoginResult DoLogin(LoginInfo login);
    }
}
