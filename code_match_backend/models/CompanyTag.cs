using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class CompanyTag
    {
        public long CompanyTagID { get; set; }
        public Company Company { get; set; }
        public Tag Tag { get; set; }
    }
}
