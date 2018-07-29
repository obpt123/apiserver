using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public interface IAppServiceProvider
    {
        T Get<T>(Type hostType,bool required=true);
    }
}
