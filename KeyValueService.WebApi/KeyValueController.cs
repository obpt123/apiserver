using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyValueService.WebApi
{
    [Route("api/[controller]")]
    public class KeyValuesController : Controller, IkeyValueService
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

            return DateTime.Now.ToString();
        }
        [HttpPost("{key}")]
        public void Set(string key, string value)
        {

        }
    }
}
