using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentTagsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public AssignmentTagsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/AssignmentTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentTag>>> GetAssignmentTags()
        {
            return await _context.AssignmentTags.ToListAsync();
        }

        // GET: api/AssignmentTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentTag>> GetAssignmentTag(long id)
        {
            var assignmentTag = await _context.AssignmentTags.FindAsync(id);

            if (assignmentTag == null)
            {
                return NotFound();
            }

            return assignmentTag;
        }

        // PUT: api/AssignmentTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignmentTag(long id, AssignmentTag assignmentTag)
        {
            if (id != assignmentTag.AssignmentTagID)
            {
                return BadRequest();
            }

            _context.Entry(assignmentTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentTagExists(id))
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

        // POST: api/AssignmentTags
        [HttpPost]
        public async Task<ActionResult<AssignmentTag>> PostAssignmentTag(AssignmentTag assignmentTag)
        {
            _context.AssignmentTags.Add(assignmentTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignmentTag", new { id = assignmentTag.AssignmentTagID }, assignmentTag);
        }

        // DELETE: api/AssignmentTags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignmentTag>> DeleteAssignmentTag(long id)
        {
            var assignmentTag = await _context.AssignmentTags.FindAsync(id);
            if (assignmentTag == null)
            {
                return NotFound();
            }

            _context.AssignmentTags.Remove(assignmentTag);
            await _context.SaveChangesAsync();

            return assignmentTag;
        }

        private bool AssignmentTagExists(long id)
        {
            return _context.AssignmentTags.Any(e => e.AssignmentTagID == id);
        }
    }
}
