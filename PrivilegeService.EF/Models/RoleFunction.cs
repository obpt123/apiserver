using System;
using System.Collections.Generic;
using System.Text;

namespace PrivilegeService.EF.Models
{
    public class RoleFunction
    {
        public int Id { get; set; }
        public string FunctionCode { get; set; }
        public string RoleUName { get; set; }
    }
}
