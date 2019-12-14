using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;
using System.Collections;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public TagsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // GET: api/Tags/Maker/Without/5
        [HttpGet("Maker/Without/{id}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllMakerTagsWithoutMakerId(long id)
        {
            var tags = _context.MakerTags
                .Include(x => x.Tag)
                .Where(x => x.MakerID == id);

            var allTags = await _context.Tags.ToListAsync();

            var tagIds = new ArrayList();

            Tag newTag = new Tag();

            foreach (var tag in tags)
            {
                tagIds.Add(tag.TagID);
            }

            foreach (var tagId in tagIds)
            {
                var tag = await _context.Tags.FindAsync(tagId);
                allTags.Remove(tag);
            }

            if (tags == null)
            {
                return NotFound();
            }

            return allTags;
        }

        // GET: api/Tags/Company/Without/5
        [HttpGet("Company/Without/{id}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllMakerTagsWithoutCompanyId(long id)
        {
            var tags = _context.CompanyTags
                .Include(x => x.Tag)
                .Where(x => x.CompanyID == id);

            var allTags = await _context.Tags.ToListAsync();

            var tagIds = new ArrayList();

            Tag newTag = new Tag();

            foreach (var tag in tags)
            {
                tagIds.Add(tag.TagID);
            }

            foreach (var tagId in tagIds)
            {
                var tag = await _context.Tags.FindAsync(tagId);
                allTags.Remove(tag);
            }

            if (tags == null)
            {
                return NotFound();
            }

            return allTags;
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(long id, Tag tag)
        {
            if (id != tag.TagID)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = tag.TagID }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        private bool TagExists(long id)
        {
            return _context.Tags.Any(e => e.TagID == id);
        }
    }
}
