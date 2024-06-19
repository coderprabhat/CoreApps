using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDB.Db;
using TestDB.Models;

namespace TestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseContactsController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public CaseContactsController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/CaseContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseContact>>> GetCaseContacts()
        {
            return await _context.CaseContacts.ToListAsync();
        }

        // GET: api/CaseContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseContact>> GetCaseContact(Guid id)
        {
            var caseContact = await _context.CaseContacts.FindAsync(id);

            if (caseContact == null)
            {
                return NotFound();
            }

            return caseContact;
        }

        // POST: api/CaseContacts
        [HttpPost]
        public async Task<ActionResult<CaseContact>> PostCaseContact(CaseContact caseContact)
        {
            _context.CaseContacts.Add(caseContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseContact", new { id = caseContact.Id }, caseContact);
        }

        // PUT: api/CaseContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseContact(Guid id, CaseContact caseContact)
        {
            if (id != caseContact.Id)
            {
                return BadRequest();
            }

            _context.Entry(caseContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseContactExists(id))
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

        // DELETE: api/CaseContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseContact(Guid id)
        {
            var caseContact = await _context.CaseContacts.FindAsync(id);
            if (caseContact == null)
            {
                return NotFound();
            }

            _context.CaseContacts.Remove(caseContact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseContactExists(Guid id)
        {
            return _context.CaseContacts.Any(e => e.Id == id);
        }

        [HttpGet("roles/{caseId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetRolesForCase(Guid caseId)
        {
            var roles = await _context.CaseContacts
                                      .Where(cc => cc.CaseId == caseId && !string.IsNullOrEmpty(cc.Roles))
                                      .Select(cc => cc.Roles)
                                      .ToListAsync();

            var uniqueRoles = roles.SelectMany(r => r.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                   .Distinct()
                                   .ToList();

            return Ok(uniqueRoles);
        }
    }

}
