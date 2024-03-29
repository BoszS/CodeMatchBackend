﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Review
    {
        public long ReviewID { get; set; }
        public string Description { get; set; }
        public long? AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
        public long? UserIDSender { get; set; }
        public User Sender { get; set; }
        public long? UserIDReceiver { get; set; }
        public User Receiver { get; set; }


    }
}
