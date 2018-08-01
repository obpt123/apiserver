using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyValueService.WebApi
{
    [Authorize]
    [Route("api/[controller]")]
    public class KeyValuesController : Controller, IkeyValueService, INeedService
    {

        public KeyValuesController()
        {

        }
        [HttpDelete("{key}")]
        public void Delete(string key)
        {

        }
        [HttpGet("{key}")]
        public string GetValue(string key)
        {
            var v = this.Get<IkeyValueService>().GetValue(key);
            return v;
        }
        [HttpPost("{key}")]
        public void Set(string key, [FromQuery]string value)
        {
            var service = this.Get<IkeyValueService>();
            service.Set(key, value);
        }
    }
}
