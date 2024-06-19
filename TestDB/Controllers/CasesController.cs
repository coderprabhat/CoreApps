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
    public class CasesController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public CasesController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Case>>> GetCases()
        {
            return await _context.Cases.Include(c => c.Address).Include(c => c.Contacts).ToListAsync();
        }

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Case>> GetCase(Guid id)
        {
            var caseItem = await _context.Cases.Include(c => c.Address).Include(c => c.Contacts).FirstOrDefaultAsync(c => c.Id == id);

            if (caseItem == null)
            {
                return NotFound();
            }

            return caseItem;
        }

        // POST: api/Cases
        [HttpPost]
        public async Task<ActionResult<Case>> PostCase(Case caseItem)
        {
            _context.Cases.Add(caseItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCase", new { id = caseItem.Id }, caseItem);
        }

        // PUT: api/Cases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCase(Guid id, Case caseItem)
        {
            if (id != caseItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(caseItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
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

        // DELETE: api/Cases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(Guid id)
        {
            var caseItem = await _context.Cases.FindAsync(id);
            if (caseItem == null)
            {
                return NotFound();
            }

            _context.Cases.Remove(caseItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseExists(Guid id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }
    }

}
