using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;
using Microsoft.AspNetCore.Authorization;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public AssignmentsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/Assignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            return await _context.Assignments
                .Include(a => a.Company)
                .Include(a => a.Applications)
                .Include(a => a.AssignmentTags)
                .ThenInclude(at => at.Tag)
                .ToListAsync();
        }

        // GET: api/Assignments/company
        [Authorize]
        [HttpGet("company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company).Include(a=> a.AssignmentTags).ToListAsync();

            return assignments;
        }

        // GET: api/Assignments/initial/company
        [Authorize]
        [HttpGet("initial/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInitialAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "Initial").Include(a => a.AssignmentTags).ToListAsync();

            return assignments;
        }

        // GET: api/Assignments/inProgress/company
        [Authorize]
        [HttpGet("inProgress/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInProgressAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "InProgress").Include(a => a.AssignmentTags).ToListAsync();

            return assignments;
        }

        // GET: api/Assignments/completed/company
        [Authorize]
        [HttpGet("completed/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetCompletedAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "Completed").Include(a => a.AssignmentTags).ToListAsync();      

            return assignments;
        }

        // GET: api/Assignments/maker
        [Authorize]
        [HttpGet("maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByMaker(long id)
        {
            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags).ToListAsync();
            var lijst = new List<Assignment>();
            foreach(var app in applications)
            {
                lijst.Add(app.Assignment);
            }

            return lijst;
        }



        // GET: api/Assignments/inProgress/maker
        [Authorize]
        [HttpGet("inProgress/maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInProgressAssignmentsByMaker(long id)
        {

            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags).Where(a => a.Assignment.Status=="InProgress").ToListAsync();
            var lijst = new List<Assignment>();
            foreach (var app in applications)
            {
                lijst.Add(app.Assignment);
            }

            return lijst;
        }

        // GET: api/Assignments/completed/maker
        [Authorize]
        [HttpGet("completed/maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetCompletedAssignmentsByMaker(long id)
        {
            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags).Where(a => a.Assignment.Status == "Completed").ToListAsync();
            var lijst = new List<Assignment>();
            foreach (var app in applications)
            {
                lijst.Add(app.Assignment);
            }

            return lijst;
        }

        // GET: api/Assignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(long id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment;
        }

        // PUT: api/Assignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(long id, Assignment assignment)
        {
            if (id != assignment.AssignmentID)
            {
                return BadRequest();
            }

            _context.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
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

        // POST: api/Assignments
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignment", new { id = assignment.AssignmentID }, assignment);
        }

        // DELETE: api/Assignments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Assignment>> DeleteAssignment(long id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return assignment;
        }

        private bool AssignmentExists(long id)
        {
            return _context.Assignments.Any(e => e.AssignmentID == id);
        }
    }
}
