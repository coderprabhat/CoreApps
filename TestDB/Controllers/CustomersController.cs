using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDB.Db;
using TestDB.Models;

namespace TestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ProjectDbContext dbContext;
        public CustomersController(ProjectDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = dbContext.Customers.ToList<Customer>();
            return Ok(customers);
        }

        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {
            var customer = await dbContext.Customers.Include(c => c.Orders)
                .ThenInclude(o => o.Products)
                .Include(c => c.ShippingAddress)
                .Include(c => c.BillingAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            customer.CreatedOn = DateTime.Now;
            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpPut]
        public async Task<ActionResult<Customer>> EditCustomer(Customer customer)
        {
            dbContext.Customers.Update(customer);
            await dbContext.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            var customer = dbContext.Customers.FirstOrDefault(p => p.Id == id);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                await dbContext.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
