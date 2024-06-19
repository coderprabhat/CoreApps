using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDB.Db;
using TestDB.Models;

namespace TestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressesController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public ShippingAddressesController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/ShippingAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingAddress>>> GetShippingAddresses()
        {
            return await _context.ShippingAddresses.ToListAsync();
        }

        // GET: api/ShippingAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingAddress>> GetShippingAddress(Guid id)
        {
            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);

            if (shippingAddress == null)
            {
                return NotFound();
            }

            return shippingAddress;
        }

        // PUT: api/ShippingAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingAddress(Guid id, ShippingAddress shippingAddress)
        {
            if (id != shippingAddress.Id)
            {
                return BadRequest();
            }

            _context.Entry(shippingAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingAddressExists(id))
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

        // POST: api/ShippingAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShippingAddress>> PostShippingAddress(ShippingAddress shippingAddress)
        {   
            shippingAddress.CreatedOn = DateTime.Now;
            _context.ShippingAddresses.Add(shippingAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingAddress", new { id = shippingAddress.Id }, shippingAddress);
        }

        // DELETE: api/ShippingAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingAddress(Guid id)
        {
            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            _context.ShippingAddresses.Remove(shippingAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingAddressExists(Guid id)
        {
            return _context.ShippingAddresses.Any(e => e.Id == id);
        }
    }
}
