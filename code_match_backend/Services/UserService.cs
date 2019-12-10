using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using code_match_backend.Helpers;
using code_match_backend.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace code_match_backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly CodeMatchContext _codeMatchContext;

        public UserService(IOptions<AppSettings> appSettings, CodeMatchContext codeMatchContext)
        {
            _appSettings = appSettings.Value;
            _codeMatchContext = codeMatchContext;
        }
        public User Authenticate(string email, string password)
        {
            var user = _codeMatchContext.Users
                .Include(u => u.Role)
                .SingleOrDefault(x => x.Email == email && x.Password == password);
            
            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication succesful so generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email)
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}
