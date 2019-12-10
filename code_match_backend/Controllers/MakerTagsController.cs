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
    public class MakerTagsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public MakerTagsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/MakerTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MakerTag>>> GetMakerTags()
        {
            return await _context.MakerTags.ToListAsync();
        }

        // GET: api/MakerTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MakerTag>> GetMakerTag(long id)
        {
            var makerTag = await _context.MakerTags.FindAsync(id);

            if (makerTag == null)
            {
                return NotFound();
            }

            return makerTag;
        }

        // PUT: api/MakerTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMakerTag(long id, MakerTag makerTag)
        {
            if (id != makerTag.MakerTagID)
            {
                return BadRequest();
            }

            _context.Entry(makerTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakerTagExists(id))
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

        // POST: api/MakerTags
        [HttpPost]
        public async Task<ActionResult<MakerTag>> PostMakerTag(MakerTag makerTag)
        {
            _context.MakerTags.Add(makerTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMakerTag", new { id = makerTag.MakerTagID }, makerTag);
        }

        // DELETE: api/MakerTags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MakerTag>> DeleteMakerTag(long id)
        {
            var makerTag = await _context.MakerTags.FindAsync(id);
            if (makerTag == null)
            {
                return NotFound();
            }

            _context.MakerTags.Remove(makerTag);
            await _context.SaveChangesAsync();

            return makerTag;
        }

        private bool MakerTagExists(long id)
        {
            return _context.MakerTags.Any(e => e.MakerTagID == id);
        }
    }
}
