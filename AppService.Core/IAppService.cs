using System;
using System.Collections.Generic;
using System.Text;

namespace AppService.Core
{
    public interface IAppService
    {
        AppInfo GetAppInfoByKey(string appKey);
    }
}
