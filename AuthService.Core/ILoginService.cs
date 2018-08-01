using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Core
{
    public interface ILoginService
    {
        LoginResult DoLogin(LoginInfo login);
    }
}
