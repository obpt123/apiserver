using Microsoft.AspNetCore.Mvc;
using Service;
using ShortLinkService.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortLinkService.WebApi
{
    [Route("api/[controller]")]
    public class ShortLinkController : Controller, IShortLinkService, INeedService
    {
        const string SequenceKey = "";

        [HttpPost("{url}")]
        public UrlInfo CreateUrlInfo(string url)
        {
            var sequenceService = this.Get<ISequenceService>();
            var keyValueService = this.Get<IkeyValueService>();
            var val = sequenceService.GetValue(SequenceKey);
            var base64 = val.ToString("X8");
            keyValueService.Set(base64, url);
            string host = this.Request.Host.Host;
            return new UrlInfo()
            {
                Key = base64,
                LongUrl = url,
                ShortUrl = $"{host}{base64}"
            };
        }
        public string QueryUrl(string keyOrShortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
