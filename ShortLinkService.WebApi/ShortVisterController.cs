using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortLinkService.WebApi
{
    [Route("/",Order =99999)]
    public class ShortVisterController: Controller,INeedService
    {
        [HttpGet("{key:alpha:length(3,8)}")]
        public ActionResult Get(string key)
        {
            var keyValueService = this.Get<IkeyValueService>();
            var url = keyValueService.GetValue(key);
            if (!string.IsNullOrEmpty(url))
            {
               return this.Redirect(url);
            }
            else
            {
               return this.NotFound();
            }
        }
    }
}
