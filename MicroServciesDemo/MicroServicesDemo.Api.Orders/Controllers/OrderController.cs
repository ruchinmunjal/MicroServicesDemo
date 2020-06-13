using MicroServicesDemo.Api.Orders.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersProvider orders;

        public OrderController(IOrdersProvider orders)
        {
            this.orders = orders;
        }
        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var result = await orders.GetOrders();
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var result = await orders.GetOrder(id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound();
            }
    }
}