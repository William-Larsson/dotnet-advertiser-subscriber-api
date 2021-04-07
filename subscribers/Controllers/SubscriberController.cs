using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using subscribers.Data;
using subscribers.Models;

namespace subscribers.Controllers
{
    // Controller class for processing the API-requests. 
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ApiDBContext _context;

        public SubscriberController(ApiDBContext context)
        {
            _context = context;
        }

        // GET: api/Subscriber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetTodoItems()
        {
            return await _context.Subscribers.ToListAsync();
        }

        // GET: api/Subscriber/{id}, where id is a valid long
        // matching with a database entry.  
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(long id)
        {
            var subscriber = await _context.Subscribers.FindAsync(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        // PUT: api/Subscriber/{id}, where id is a valid long
        // matching with a database entry. 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriber(long id, Subscriber subscriber)
        {
            if (id != subscriber.SubscriberId)
            {
                return BadRequest();
            }

            _context.Entry(subscriber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(id))
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

        // POST: api/Subscriber
        [HttpPost]
        public async Task<ActionResult<Subscriber>> PostSubscriber(Subscriber subscriber)
        {
            _context.Subscribers.Add(subscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscriber", new { id = subscriber.SubscriberId }, subscriber);
        }

        // DELETE: api/Subscriber/{id}, where id is a valid long
        // matching with a database entry. 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(long id)
        {
            var subscriber = await _context.Subscribers.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Returns true if there is an entry in the DB with a 
        // primary key that matches with the given id. 
        private bool SubscriberExists(long id)
        {
            return _context.Subscribers.Any(e => e.SubscriberId == id);
        }
    }
}
