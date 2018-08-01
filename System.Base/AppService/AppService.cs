using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class AppService
    {
        public static IAppServiceProvider Provider { get; set; }

        public static T Get<T>(Type hostType, bool required)
        {
            return Provider.Get<T>(hostType,required);
        }

        public static T Get<T>(this INeedService host,bool required=true)
        {
            return Get<T>(host.GetType(),required);
        }

    }

}
