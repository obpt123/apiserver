using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface ISequenceService
    {
        long GetValue(string key);
    }
}
