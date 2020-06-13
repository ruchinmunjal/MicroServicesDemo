using System;
using System.Collections.Generic;

namespace MicroServicesDemo.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime  OrderDate { get; set; }

        public int Total { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

   
}
