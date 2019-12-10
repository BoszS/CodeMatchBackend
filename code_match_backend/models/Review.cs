using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Review
    {
        public long ReviewID { get; set; }
        public string Description { get; set; }
        public Assignment Assignment { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
