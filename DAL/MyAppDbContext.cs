using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.DAL
{
    public class MyAppDbContext : DbContext
    {

        public MyAppDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
