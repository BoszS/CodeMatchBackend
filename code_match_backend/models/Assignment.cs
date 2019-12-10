using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Assignment
    {
        public long AssignmentID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public Company Company { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AssignmentTag> AssignmentTags { get; set; }
    }
}
