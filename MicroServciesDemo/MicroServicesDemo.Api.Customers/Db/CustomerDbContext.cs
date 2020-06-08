using Microsoft.EntityFrameworkCore;

namespace MicroServicesDemo.Api.Customers.Db
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
