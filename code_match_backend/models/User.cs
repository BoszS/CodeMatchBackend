using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class User
    {
        public long UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string Biography { get; set; }
        public Role Role { get; set; }
        public long? MakerID { get; set; }
        public Maker Maker { get; set; }
        public long? CompanyID { get; set; }
        public Company Company { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
