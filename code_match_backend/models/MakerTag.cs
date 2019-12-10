using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class MakerTag
    {
        public long MakerTagID { get; set; }
        public Tag Tag { get; set; }
        public Maker Maker { get; set; }
    }
}
