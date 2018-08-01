using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public interface IAppConfigProvider
    {
        string GetConfig(Type hostType, string configKey,bool required=true);

        string GetConnString(Type hostType, string connectionName);
    }
}
