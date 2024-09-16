using CustomersAPI.DAL;
using CustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly MyAppDbContext _context;
        public CustomerController(MyAppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null)
                return NotFound("Customer not found");
            return Ok(customer);

        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer updatedcustomer)
        {
            var dbcustomer = await _context.Customers.FindAsync(updatedcustomer.Id);
            if (dbcustomer is null)
                return NotFound("Customer not found");

            dbcustomer.FirstName = updatedcustomer.FirstName;
            dbcustomer.LastName = updatedcustomer.LastName;
            dbcustomer.Email = updatedcustomer.Email;
            dbcustomer.Phone = updatedcustomer.Phone;
            dbcustomer.Country = updatedcustomer.Country;


            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var dbcustomer = await _context.Customers.FindAsync(id);
            if (dbcustomer is null)
                return NotFound("Customer not found");


            _context.Customers.Remove(dbcustomer);



            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());

        }
    }
}
