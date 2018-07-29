using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
    public interface IAppContext
    {
        string UserName { get; }
        CultureInfo UICulture { get; }
    }

}
