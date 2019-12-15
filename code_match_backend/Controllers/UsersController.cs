using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;
using code_match_backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CodeMatchContext _context;
        private IUserService _userService;

        public UsersController(CodeMatchContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            switch (user.Role.Name)
            {
                case "Maker":
                    var maker = _context.Makers.SingleOrDefault(m => m.MakerID == user.MakerID);
                    user.Maker = maker;
                    break;
                case "Company":
                    var company = _context.Companies.SingleOrDefault(c => c.CompanyID == user.CompanyID);
                    user.Company = company;
                    break;
                default:
                    break;
            }

            return Ok(user);
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.Include(c => c.Company).SingleAsync(i => i.UserID == id);


            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/User/Info
        [HttpGet("User/Info/{id}")]
        public async Task<ActionResult<User>> GetUserWithTypeAndPermissions(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.CompanyID != null)
            {

                var userWithType = await _context.Users
                    .Include(x => x.Company).ThenInclude(x=> x.Assignments).ThenInclude(x => x.Applications).ThenInclude(x => x.Maker).ThenInclude(x => x.User).ThenInclude(x => x.ReceivedReviews)
                    .Include(x => x.Company).ThenInclude(x => x.Assignments).ThenInclude(x => x.Reviews)
                    .Include(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                    .FirstOrDefaultAsync(x => x.CompanyID == user.CompanyID);

                userWithType.Password = null;

                return userWithType;
            }

            if (user.MakerID != null)
            {
                var userWithType = await _context.Users
                    .Include(x => x.Maker).ThenInclude(m => m.Applications).ThenInclude(a => a.Assignment).ThenInclude(a => a.Company).ThenInclude(a => a.User).ThenInclude(a => a.ReceivedReviews)
                    .Include(x => x.Maker).ThenInclude(m => m.Applications).ThenInclude(a => a.Assignment).ThenInclude(a => a.Applications).ThenInclude(a => a.Maker).ThenInclude(a => a.User).ThenInclude(a => a.ReceivedReviews)
                    .Include(x => x.Maker).ThenInclude(m => m.Applications).ThenInclude(a => a.Assignment).ThenInclude(a => a.Reviews)
                    .Include(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                    .FirstOrDefaultAsync(x => x.MakerID == user.MakerID);

                userWithType.Password = null;

                return userWithType;
            }


            return NotFound();
        }

        [HttpGet]
        [Route("checkMail")]
        public async Task<Boolean> checkMail(string mail)
        {
            try
            {
                await _context.Users.SingleAsync(e => e.Email == mail);

                return true;
            } catch
            {
                return false;
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            var updateUser = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.UserID == id);

            user.Password = updateUser.Password;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                _context.Entry(user.Company).State = EntityState.Modified;
            } catch
            {
                _context.Entry(user.Maker).State = EntityState.Modified;
            }

            _context.Entry(user.Role).State = EntityState.Unchanged;
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
