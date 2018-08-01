using AppService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace AppService.EF.Services
{
    public class AppService : IAppService
    {
        private AppServiceContext context;
        public AppService(AppServiceContext context)
        {
            this.context = context;
        }

        public AppInfo GetAppInfoByKey(string appKey)
        {
            return context.AppInfos.Where(p => p.AppKey == appKey).SingleOrDefault();
        }
    }
}
