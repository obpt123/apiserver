using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Entity;
using Service.Models;
using System.Data.Store;

namespace KeyValueService.Entity.Impl
{
    [ServiceImplClass(typeof(IkeyValueService))]
    public class KeyValueService : Service.IkeyValueService
    {
        public KeyValueService(IEntityStore<KeyValue> store)
        {
            this.keyValueStore = store;
        }
        private IEntityStore<KeyValue> keyValueStore;
        public string GetValue(string key)
        {
            var res = keyValueStore.FindSingleOrDefault(p => p.Key == key);
            if(res !=null)
                return res.Value;
            return string.Empty;
        }

        public void Set(string key, string value)
        {
            using (var unitofwork = new UnitOfWork(this.keyValueStore))
            {
                var entity = this.keyValueStore.FindSingleOrDefault(p => p.Key == key);
                if (entity == null)
                {
                    this.keyValueStore.Add(new KeyValue() {
                         Id=Guid.NewGuid(),
                         Key=key,
                         Value=value
                    });
                }
                else
                {
                    entity.Value = value;
                    this.keyValueStore.Update(entity);
                }
                unitofwork.SaveChanges();
            }
        }
    }
}
