using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using Service.Models;

namespace Service.Services
{
    [ServiceClass(typeof(IkeyValueService))]
    public class KeyValueService : Service.IkeyValueService
    {
        public KeyValueService(KeyValueContext context)
        {
            this.context = context;
        }
        KeyValueContext context;
        public string GetValue(string key)
        {

            return (from p in context.KeyValues
                    where p.Key == key
                    select p.Value).FirstOrDefault();


        }

        public void Set(string key, string value)
        {
            var entity = context.KeyValues.Where(p => p.Key == key).FirstOrDefault();
            if (entity != null)
            {
                entity.Value = value;
            }
            else
            {
                context.Add(new KeyValue()
                {
                    Id = Guid.NewGuid(),
                    Key = key,
                    Value = value
                });
            }
            context.SaveChanges();

        }
    }
}
