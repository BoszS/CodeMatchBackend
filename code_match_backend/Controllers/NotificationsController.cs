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
    public class NotificationsController : ControllerBase
    {
        private readonly CodeMatchContext _context;

        public NotificationsController(CodeMatchContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notifications/receiver
        [HttpGet("receiver/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == false).Include(n => n.Receiver).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Review/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetReviewNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == false && n.ReviewID != null)
                .Include(n => n.Sender)
                .Include(n => n.Review).ThenInclude(r => r.Sender)
                .Include(n => n.Review).ThenInclude(r => r.Assignment).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Review/read/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetReadReviewNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == true && n.ReviewID != null)
                .Include(n => n.Sender)
                .Include(n => n.Review).ThenInclude(r => r.Sender)
                .Include(n => n.Review).ThenInclude(r => r.Assignment).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Application/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetApplicationNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == false && n.ApplicationID != null)
                .Include(n => n.Sender)
                .Include(n => n.Application).ThenInclude(a => a.Assignment)
                .Include(n => n.Application).ThenInclude(a => a.Maker).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Application/read/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetReadApplicationNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == true && n.ApplicationID != null)
                .Include(n => n.Sender)
                .Include(n => n.Application).ThenInclude(a => a.Assignment)
                .Include(n => n.Application).ThenInclude(a => a.Maker).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Assignment/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAssignmentNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == false && n.AssignmentID != null)
                .Include(n => n.Sender)
                .Include(n => n.Assignment).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("receiver/Assignment/read/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetReadAssignmentNotificationsByReceiver(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Receiver == user && n.Read == true && n.AssignmentID != null)
                .Include(n => n.Sender)
                .Include(n => n.Assignment).ToListAsync();
        }

        // GET: api/Notifications
        [HttpGet("sender/Application/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetApplicationNotificationsBySender(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return await _context.Notification.Where(n => n.Sender == user && n.Read == false && n.ApplicationID != null)
                .Include(n => n.Receiver)
                .Include(n => n.Application).ThenInclude(a => a.Assignment).ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(long id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(long id, Notification notification)
        {
            if (id != notification.NotificationID)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(NotificationDto notification)
        {
            Notification newNotification;
            if (notification.AssignmentID!=0) {
                newNotification = new Notification
                {
                    SenderID = notification.SenderID,
                    ReceiverID = notification.ReceiverID,
                    Read = false,
                    AssignmentID = notification.AssignmentID
                };
            }
            else if (notification.ReviewID != 0)
            {
                newNotification = new Notification
                {
                    SenderID = notification.SenderID,
                    ReceiverID = notification.ReceiverID,
                    Read = false,                
                    ReviewID = notification.ReviewID
                };
            }
            else
            {
                newNotification = new Notification
                {
                    SenderID = notification.SenderID,
                    ReceiverID = notification.ReceiverID,
                    Read = false,                    
                    ApplicationID = notification.ApplicationID
                };
            }



            _context.Notification.Add(newNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = newNotification.NotificationID }, newNotification);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> DeleteNotification(long id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        private bool NotificationExists(long id)
        {
            return _context.Notification.Any(e => e.NotificationID == id);
        }
    }
}
