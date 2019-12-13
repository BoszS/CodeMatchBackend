using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class Notification
    {
        public long NotificationID { get; set; }
        public long? SenderID { get; set; }
        public User Sender { get; set; }
        public long? ReceiverID { get; set; }
        public User Receiver { get; set; }
        public long? AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
        public long? ReviewID { get; set; }
        public Review Review { get; set; }
        public long? ApplicationID { get; set; }
        public Application Application { get; set; }
        public bool Read { get; set; }

    }
}
