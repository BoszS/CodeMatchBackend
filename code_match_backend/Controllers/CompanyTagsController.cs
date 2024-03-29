﻿using System;
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
    public class CompanyTagsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public CompanyTagsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/CompanyTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyTag>>> GetCompanyTags()
        {
            return await _context.CompanyTags.ToListAsync();
        }

        // GET: api/CompanyTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyTag>> GetCompanyTag(long id)
        {
            var companyTag = await _context.CompanyTags.FindAsync(id);

            if (companyTag == null)
            {
                return NotFound();
            }

            return companyTag;
        }

        // GET: api/MakerTags/Company/5
        [HttpGet("Company/{id}")]
        public async Task<ActionResult<IEnumerable<CompanyTag>>> GetCompanyTagsByCompanyId(long id)
        {
            var tags = _context.CompanyTags
                .Include(x => x.Tag)
                .Where(x => x.CompanyID == id);

            if (tags == null)
            {
                return NotFound();
            }

            return await tags.ToListAsync();
        }

        // GET: api/MakerTags/Company/Without/5
        [HttpGet("Company/Without/{id}")]
        public async Task<ActionResult<IEnumerable<CompanyTag>>> GetAllCompanyTagsWithoutCompanyId(long id)
        {
            var tags = _context.CompanyTags
                .Include(x => x.Tag)
                .Where(x => x.CompanyID != id);

            if (tags == null)
            {
                return NotFound();
            }

            return await tags.ToListAsync();
        }

        // PUT: api/CompanyTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyTag(long id, CompanyTag companyTag)
        {
            if (id != companyTag.CompanyTagID)
            {
                return BadRequest();
            }

            _context.Entry(companyTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyTagExists(id))
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

        // POST: api/CompanyTags
        [HttpPost]
        public async Task<ActionResult<CompanyTag>> PostCompanyTag(CompanyTag companyTag)
        {
            _context.CompanyTags.Add(companyTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyTag", new { id = companyTag.CompanyTagID }, companyTag);
        }

        // DELETE: api/CompanyTags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyTag>> DeleteCompanyTag(long id)
        {
            var companyTag = await _context.CompanyTags.FindAsync(id);
            if (companyTag == null)
            {
                return NotFound();
            }

            _context.CompanyTags.Remove(companyTag);
            await _context.SaveChangesAsync();

            return companyTag;
        }

        // DELETE: api/CompanyTags/5/2
        [HttpDelete("{companyId}/{tagId}")]
        public async Task<ActionResult<CompanyTag>> DeleteCompanyTagByMakedIdAndTagId(long companyId, long tagId)
        {
            var compantTag = await _context.CompanyTags.FirstOrDefaultAsync(x => x.CompanyID == companyId && x.TagID == tagId);
            if (compantTag == null)
            {
                return NotFound();
            }

            _context.CompanyTags.Remove(compantTag);
            await _context.SaveChangesAsync();

            return compantTag;
        }

        private bool CompanyTagExists(long id)
        {
            return _context.CompanyTags.Any(e => e.CompanyTagID == id);
        }
    }
}
