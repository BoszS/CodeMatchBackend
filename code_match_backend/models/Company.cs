using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Company
    {
        public long CompanyID { get; set; }
        public string Location { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<CompanyTag> CompanyTags { get; set; }
    }
}
