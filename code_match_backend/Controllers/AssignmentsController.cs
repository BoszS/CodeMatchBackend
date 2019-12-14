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


        /// <summary>
        /// GET: all assignments
        /// </summary>  
        /// <returns>A list of all applications</returns>
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


        /// <summary>
        /// GET: all assignments that belong to a company
        /// </summary>
        /// <param name="id">The company id</param>   
        /// <returns>The list of assignments that belong to the company</returns>
        [Authorize]
        [HttpGet("company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company).Include(a => a.Company)
                .Include(a => a.Applications)
                .Include(a => a.AssignmentTags)
                .ThenInclude(at => at.Tag).ToListAsync();

            return assignments;
        }


        /// <summary>
        /// GET: all assignments that belong to a company that have an intitial state
        /// </summary>
        /// <param name="id">The company id</param>   
        /// <returns>The list of initial assignments that belong to the company</returns>
        [Authorize]
        [HttpGet("initial/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInitialAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "Initial").Include(a => a.Company)
                .Include(a => a.Applications)
                .Include(a => a.AssignmentTags)
                .ThenInclude(at => at.Tag).ToListAsync();

            return assignments;
        }

        /// <summary>
        /// GET: all assignments that belong to a company that have an inProgress state
        /// </summary>
        /// <param name="id">The company id</param>   
        /// <returns>The list of inProgress assignments that belong to the company</returns>
        [Authorize]
        [HttpGet("inProgress/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInProgressAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "InProgress").Include(a => a.Company)
                .Include(a => a.Applications)
                .Include(a => a.AssignmentTags)
                .ThenInclude(at => at.Tag).ToListAsync();

            return assignments;
        }

        /// <summary>
        /// GET: all assignments that belong to a company that have a completed state
        /// </summary>
        /// <param name="id">The company id</param>   
        /// <returns>The list of completed assignments that belong to the company</returns>
        [Authorize]
        [HttpGet("completed/company/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetCompletedAssignmentsByCompany(long id)
        {
            var company = await _context.Companies.FindAsync(id);
            var assignments = await _context.Assignments.Where(m => m.Company == company && m.Status == "Completed").Include(a => a.Company)
                .Include(a => a.Applications)
                .Include(a => a.AssignmentTags)
                .ThenInclude(at => at.Tag).ToListAsync();      

            return assignments;
        }

        /// <summary>
        /// GET: all assignments that a maker is accepted for
        /// </summary>
        /// <param name="id">The maker id</param>   
        /// <returns>The list of assignments that the maker is accepted for</returns>
        [Authorize]
        [HttpGet("maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByMaker(long id)
        {
            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.Company)
                .Include(a => a.Assignment).ThenInclude(a => a.Applications)
                .Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags)
                .ThenInclude(a => a.Tag)
                .ToListAsync();
            var lijst = new List<Assignment>();
            foreach(var app in applications)
            {
                lijst.Add(app.Assignment);
            }
            
            return lijst;
        }


        /// <summary>
        /// GET: all assignments that a maker is accepted for that are inProgress
        /// </summary>
        /// <param name="id">The maker id</param>   
        /// <returns>The list of assignments that the maker is accepted for and are inProgress</returns>
        //[Authorize]
        [HttpGet("inProgress/maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetInProgressAssignmentsByMaker(long id)
        {

            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.Company)
                .Include(a => a.Assignment).ThenInclude(a => a.Applications)
                .Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags)
                .ThenInclude(a => a.Tag).Where(a => a.Assignment.Status=="InProgress").ToListAsync();
            var lijst = new List<Assignment>();
            foreach (var app in applications)
            {
                lijst.Add(app.Assignment);
            }

            return lijst;
        }

        /// <summary>
        /// GET: all assignments that a maker is accepted for that are completed
        /// </summary>
        /// <param name="id">The maker id</param>   
        /// <returns>The list of assignments that the maker is accepted for and are completed</returns>
        [Authorize]
        [HttpGet("completed/maker/{id}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetCompletedAssignmentsByMaker(long id)
        {
            var maker = await _context.Makers.FindAsync(id);
            var applications = await _context.Applications.Where(m => m.Maker == maker && m.IsAccepted == true).Include(a => a.Assignment).ThenInclude(a => a.Company)
                .Include(a => a.Assignment).ThenInclude(a => a.Applications)
                .Include(a => a.Assignment).ThenInclude(a => a.AssignmentTags)
                .ThenInclude(a => a.Tag).Where(a => a.Assignment.Status == "Completed").ToListAsync();
            var lijst = new List<Assignment>();
            foreach (var app in applications)
            {
                lijst.Add(app.Assignment);
            }

            return lijst;
        }

        /// <summary>
        /// GET: the assignment that belongs to the given id
        /// </summary>
        /// <param name="id">The assignment id</param>   
        /// <returns>The found assignment</returns>
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

        /// <summary>
        /// PUT: update the assignment with the given assignment ID
        /// </summary>
        /// <param name="id">The assignment id</param>   
        /// <param name="assignment">The to update assignment</param>   
        /// <returns>null</returns>
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

        /// <summary>
        /// POST: add a new assignment
        /// </summary>
        /// <param name="assignment">The new assignment</param>   
        /// <returns>the new assignment</returns>
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignment", new { id = assignment.AssignmentID }, assignment);
        }

        /// <summary>
        /// DELETE: delete an assignment
        /// </summary>
        /// <param name="id">The to delete assignment</param>   
        /// <returns>the deleted assignment</returns>
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
