using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortLinkService.WebApi
{
    [Route("/s/",Order =99999)]
    public class ShortVisterController: Controller
    {
        IkeyValueService keyValueService;
        [HttpGet("{key:alpha:length(3,10)}")]
        public ActionResult Get(string key)
        {
            var url = keyValueService.GetValue(key);
            if (!string.IsNullOrEmpty(url))
            {
               return this.RedirectPermanent(url);
            }
            else
            {
               return this.NotFound();
            }
        }
    }
}
