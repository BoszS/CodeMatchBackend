using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_match_backend.models;
using Microsoft.AspNetCore.Authorization;
using code_match_backend.models.Dto;

namespace code_match_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public ReviewsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/sender/5
        [HttpGet("sender/{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsBySender(long id)
        {
            return await _context.Reviews.Where(r => r.UserIDSender == id).Include(r => r.Receiver).ToListAsync();
        }

        // GET: api/Reviews/receiver/5
        [HttpGet("receiver/{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByReceiver(long id)
        {
            return await _context.Reviews.Where(r => r.UserIDReceiver == id).Include(r => r.Sender).ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(long id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        /// <summary>
        /// GET: The review with the given values in the Dto
        /// </summary>
        /// <param name="dto">The dto containing the SenderId, ReceiverId and assignmentId, respectevly</param>   
        /// <returns>The review belonging to the given values in the dto</returns>
        [Authorize]
        [HttpGet("review/{dto}")]
        public async Task<ActionResult<Review>> GetAssignmentsByCompany(ReviewDto dto)
        {
            var review = new Review();
            if (dto.AssignmentID !=0)
            {
                review = await _context.Reviews
                    .Where(r => r.UserIDSender == dto.SenderID && r.AssignmentID == dto.AssignmentID).SingleOrDefaultAsync();
            }

            if (dto.ReceiverID != 0)
            {
                review = await _context.Reviews
                    .Where(r => r.UserIDSender == dto.SenderID && r.UserIDReceiver == dto.ReceiverID).SingleOrDefaultAsync();
            }
            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(long id, Review review)
        {
            if (id != review.ReviewID)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewDto reviewDto)
        {
            var review = new Review();
            if (reviewDto.AssignmentID !=0)
            {
                review = new Review
                {
                    UserIDSender = reviewDto.SenderID,
                    Description = reviewDto.description,
                    AssignmentID = reviewDto.AssignmentID
                };
            } else
            {
                review = new Review
                {
                    UserIDSender = reviewDto.SenderID,
                    Description = reviewDto.description,
                    UserIDReceiver = reviewDto.ReceiverID
                };
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.ReviewID }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(long id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        private bool ReviewExists(long id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
