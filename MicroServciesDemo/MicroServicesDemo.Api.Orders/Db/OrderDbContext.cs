using Microsoft.EntityFrameworkCore;

namespace MicroServicesDemo.Api.Orders.Db
{
    public class OrderDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
