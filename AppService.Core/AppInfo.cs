using System;

namespace AppService.Core
{
    public class AppInfo
    {
        public Guid Id { get; set; }

        public string AppKey { get; set; }

        public string AppName { get; set; }

        public string Host { get; set; }

        public string CustomCssUrl { get; set; }
    }
}
