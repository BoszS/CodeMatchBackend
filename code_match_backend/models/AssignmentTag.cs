using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class AssignmentTag
    {
        public long AssignmentTagID { get; set; }
        public Assignment Assignment { get; set; }
        public Tag Tag { get; set; }
    }
}
