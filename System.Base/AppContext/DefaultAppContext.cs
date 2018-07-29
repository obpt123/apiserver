using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
    class DefaultAppContext: IAppContext
    {
        public CultureInfo UICulture
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
        }

        public string UserName
        {
            get
            {
                var cp = System.Threading.Thread.CurrentPrincipal;
                var id = cp != null ? cp.Identity : null;
                return id != null ? id.Name : string.Empty;
            }
        }
    }
}
