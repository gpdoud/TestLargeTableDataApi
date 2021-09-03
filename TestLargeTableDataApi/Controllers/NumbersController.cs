using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestLargeTableDataLib.Models;

namespace TestLargeTableDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NumbersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Numbers/{pagesize}/{page}
        [HttpGet("{pagesize}/{page}")]
        public async Task<ActionResult<IEnumerable<Number>>> GetNumbers(int pagesize, int page)
        {
            return await _context.Numbers
                                    .Skip(pagesize * (page - 1))
                                    .Take(pagesize)
                                    .ToListAsync();
        }

        // GET: api/Numbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Number>>> GetNumbers()
        {
            return await _context.Numbers.ToListAsync();
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Number>> GetNumber(int id)
        {
            var number = await _context.Numbers.FindAsync(id);

            if (number == null)
            {
                return NotFound();
            }

            return number;
        }

        // PUT: api/Numbers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNumber(int id, Number number)
        {
            if (id != number.Id)
            {
                return BadRequest();
            }

            _context.Entry(number).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumberExists(id))
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

        // POST: api/Numbers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Number>> PostNumber(Number number)
        {
            _context.Numbers.Add(number);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNumber", new { id = number.Id }, number);
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Number>> DeleteNumber(int id)
        {
            var number = await _context.Numbers.FindAsync(id);
            if (number == null)
            {
                return NotFound();
            }

            _context.Numbers.Remove(number);
            await _context.SaveChangesAsync();

            return number;
        }

        private bool NumberExists(int id)
        {
            return _context.Numbers.Any(e => e.Id == id);
        }
    }
}
