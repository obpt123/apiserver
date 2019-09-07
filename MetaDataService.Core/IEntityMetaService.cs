using System;
using System.Collections.Generic;
using System.Data.Meta;
using System.Text;

namespace MetaDataService.Core
{
    public interface IEntityMetaService
    {
        EntityInfo GetEntityInfo(string entityKey);
    }
}


