using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models.Dto
{
    public class ApplicationDto
    {
        public Boolean IsAccepted { get; set; }
        public int MakerID { get; set; }
        public Assignment Assignment { get; set; }
    }
}
