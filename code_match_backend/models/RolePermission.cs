using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class RolePermission
    {
        public long RolePermissionID { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
