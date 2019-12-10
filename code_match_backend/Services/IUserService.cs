using code_match_backend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}
