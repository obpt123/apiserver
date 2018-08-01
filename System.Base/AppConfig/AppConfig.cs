using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class AppConfig
    {

        public static IAppConfigProvider Provider { get; set; }

        public static string GetConfig(this INeedConfig host, string configKey,bool required=true)
        {
            return Provider.GetConfig(host.GetType(), configKey, required);
        }
        public static string GetConnectionString(this INeedConfig host, string connName)
        {
            return Provider.GetConnString(host.GetType(), connName);
        }
    }
}
