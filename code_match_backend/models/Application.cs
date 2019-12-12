using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Application
    {
        public long ApplicationID { get; set; }
        public Boolean IsAccepted { get; set; }
        public long AssignmentID { get; set; }
        public long MakerID { get; set; }
        public Maker Maker { get; set; }
        public Assignment Assignment { get; set; }
    }
}
