using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyValueService.WebApi
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class KeyValuesController : Controller, IkeyValueService
    {
        
        IkeyValueService keyValueService;
        public KeyValuesController(IkeyValueService keyValueService)
        {
            this.keyValueService = keyValueService;
        }
        [HttpDelete("{key}")]
        public void Delete(string key)
        {

        }
        [HttpGet("{key}")]
        public string GetValue(string key)
        {
            return this.keyValueService.GetValue(key);
        }
        [HttpPost("{key}")]
        public void Set(string key, [FromQuery]string value)
        {
            this.keyValueService.Set(key, value);
        }
    }
}
