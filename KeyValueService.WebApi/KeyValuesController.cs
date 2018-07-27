using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    [Route("api/[controller]")]
    public class KeyValuesController : Controller,IkeyValueService
    {
        [HttpDelete("{key}")]
        public void Delete(string key)
        {
        }
        [HttpGet("{key}")]
        public string GetValue(string key)
        {
            return DateTime.Now.ToString();
        }
        [HttpPost("{key}")]
        public void Set(string key, string value)
        {
           
        }
    }
}
