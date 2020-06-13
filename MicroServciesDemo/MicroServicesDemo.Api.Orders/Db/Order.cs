using System.Collections.Generic;

namespace MicroServicesDemo.Api.Orders.Db
{
    public class Order
    {
        public int OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
