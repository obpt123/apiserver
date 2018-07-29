using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static class AppContext
    {
        public static IAppContext Provider { get; set; } = new DefaultAppContext();

        public static string UserName
        {
            get
            {
                return Provider.UserName;
            }
        }
        public static CultureInfo UICulture
        {
            get
            {
                return Provider.UICulture;
            }
        }
    }
}
