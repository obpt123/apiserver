using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class KeyValueContext:DbContext
    {
        public DbSet<KeyValue> KeyValues { get; set; }
    }
}
