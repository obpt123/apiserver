using MetaDataService.Core;
using System;
using System.Authority;
using System.Collections.Generic;
using System.Data.Meta;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace MetaDataService.Reflection.Impl
{
   
    public class EntityMetaService : IEntityMetaService
    {
        public EntityInfo GetEntityInfo(string entityKey)
        {
            var type = this.GetServiceInterfaceType(entityKey);

            var mgmtServiceAttribute = type.GetCustomAttribute<MgmtServiceAttribute>();

            var entityType = mgmtServiceAttribute.EntityType;

            return GetEntityInfo(mgmtServiceAttribute, entityType);
        }

        private Type GetServiceInterfaceType(string serviceName)
        {
            return Type.GetType(serviceName);
        }

        private EntityInfo GetEntityInfo(MgmtServiceAttribute mgmtAttribute, Type entityType)
        {
            return new EntityInfo()
            {

                Props = GetFieldInfos(entityType)
            };
        }
        private List<PropInfo> GetFieldInfos(Type entityType)
        {
            return entityType.GetProperties().Select(GetFieldInfo).ToList();
        }
        private PropInfo GetFieldInfo(System.Reflection.PropertyInfo propInfo)
        {
            PropInfo fi = new PropInfo();
            if (propInfo.PropertyType.IsEnum)
            {
                fi.EnumItems = GetEnumInfos(propInfo.PropertyType);
            }
            fi.Name = propInfo.Name;
            
            return fi;
        }
        private List<EnumInfo> GetEnumInfos(Type enumType)
        {
            List<EnumInfo> res = new List<EnumInfo>();
            foreach (var val in Enum.GetValues(enumType))
            {
                res.Add(new EnumInfo()
                {
                    Text = Enum.GetName(enumType, val),
                    Value = Enum.GetName(enumType, val)
                });
            }
            return res;
        }
    }
}
