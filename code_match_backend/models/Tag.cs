using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Tag
    {
        public long TagID { get; set; }
        public string Name { get; set; }
        public ICollection<AssignmentTag> AssignmentTags { get; set; }
        public ICollection<CompanyTag> CompanyTags { get; set; }
        public ICollection<MakerTag> MakerTags { get; set; }
    }
}
