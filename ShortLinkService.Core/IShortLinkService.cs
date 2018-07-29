using System;

namespace ShortLinkService.Core
{
    public interface IShortLinkService
    {
        UrlInfo CreateUrlInfo(string longUrl);
        string QueryUrl(string keyOrShortUrl);
    }
    public class UrlInfo
    {
        public string Key { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
    }
}
