using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Maker
    {
        public long MakerID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Dob { get; set; }
        public string LinkedIn { get; set; }
        public string Experience { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<MakerTag> MakerTags { get; set; }
    }
}
