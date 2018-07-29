using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Service
{
    [Route("api/[controller]")]
    public class SequenceController : Controller, ISequenceService
    {
        [HttpGet("{key}")]
        public long GetValue(string key)
        {
            return 0L;
        }
    }
}
