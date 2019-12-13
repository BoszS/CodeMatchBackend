using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models.Dto
{
    public class NotificationDto
    {
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public int AssignmentID { get; set; }
        public int ReviewID { get; set; }
        public int ApplicationID { get; set; }
    }
}
