using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{

    public interface IkeyValueService
    {
        string GetValue(string key);

        void Set(string key, string value);
    }

    
}
