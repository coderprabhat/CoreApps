using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDB.Db;
using TestDB.Models;

namespace TestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseAddressesController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public CaseAddressesController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/CaseAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseAddress>>> GetCaseAddresses()
        {
            return await _context.CaseAddresses.ToListAsync();
        }

        // GET: api/CaseAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseAddress>> GetCaseAddress(Guid id)
        {
            var caseAddress = await _context.CaseAddresses.FindAsync(id);

            if (caseAddress == null)
            {
                return NotFound();
            }

            return caseAddress;
        }

        // POST: api/CaseAddresses
        [HttpPost]
        public async Task<ActionResult<CaseAddress>> PostCaseAddress(CaseAddress caseAddress)
        {
            _context.CaseAddresses.Add(caseAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseAddress", new { id = caseAddress.Id }, caseAddress);
        }

        // PUT: api/CaseAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseAddress(Guid id, CaseAddress caseAddress)
        {
            if (id != caseAddress.Id)
            {
                return BadRequest();
            }

            _context.Entry(caseAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseAddressExists(id))
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

        // DELETE: api/CaseAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseAddress(Guid id)
        {
            var caseAddress = await _context.CaseAddresses.FindAsync(id);
            if (caseAddress == null)
            {
                return NotFound();
            }

            _context.CaseAddresses.Remove(caseAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseAddressExists(Guid id)
        {
            return _context.CaseAddresses.Any(e => e.Id == id);
        }
    }

}
