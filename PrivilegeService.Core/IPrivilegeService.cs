using System;
using System.Authority;

namespace PrivilegeService.Core
{
    public interface IPrivilegeService
    {
        FunctionInfo GetFunctionByCode(string functionCode);
    }

}
