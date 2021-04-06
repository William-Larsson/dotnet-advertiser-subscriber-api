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
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Subscriber/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(int id)
        {
            var subscriber = await _context.TodoItems.FindAsync(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        // PUT: api/Subscriber/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriber(int id, Subscriber subscriber)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscriber>> PostSubscriber(Subscriber subscriber)
        {
            _context.TodoItems.Add(subscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscriber", new { id = subscriber.SubscriberId }, subscriber);
        }

        // DELETE: api/Subscriber/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var subscriber = await _context.TodoItems.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(subscriber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscriberExists(int id)
        {
            return _context.TodoItems.Any(e => e.SubscriberId == id);
        }
    }
}
