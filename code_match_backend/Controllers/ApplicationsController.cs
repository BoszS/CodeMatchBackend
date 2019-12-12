using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;
using code_match_backend.models.Dto;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public ApplicationsController(CodeMatchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: all appliactions
        /// </summary>
        /// <returns>All assignments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {

            return await _context.Applications
                .Include(a => a.Maker)
                .ToListAsync();
        }


        /// <summary>
        /// GET: the application with the given id
        /// </summary>
        /// <param name="id">The id from the to retrieve application</param>   
        /// <returns>The retrieved application</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(long id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }


        /// <summary>
        /// PUT: Change the given application that corresponds with the given id
        /// </summary>
        /// <param name="id">The id from the to update application</param>   
        /// <param name="application">The to update application</param>
        /// <returns>null</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(long id, Application application)
        {
            if (id != application.ApplicationID)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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
        /// POST: Post a new application
        /// </summary>  
        /// <param name="applicationDto">The Dto that carries all the info to create a new application</param>
        /// <returns>The new application</returns>
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(ApplicationDto applicationDto)
        {
            Application newApplication = new Application
            {
                AssignmentID = applicationDto.Assignment.AssignmentID,
                IsAccepted = applicationDto.IsAccepted,
                MakerID = applicationDto.MakerID
            };

            _context.Applications.Add(newApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = newApplication.ApplicationID }, newApplication);
        }

        /// <summary>
        /// Delete: Delete the application with the given id
        /// </summary>  
        /// <param name="id">The id of the to delete application</param>
        /// <returns>The deleted application</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(long id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return application;
        }

        private bool ApplicationExists(long id)
        {
            return _context.Applications.Any(e => e.ApplicationID == id);
        }
    }
}
