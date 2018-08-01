using System;
using System.Collections.Generic;
using System.Text;

namespace PrivilegeService.EF.Models
{
    public class FunctionInfo
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}
