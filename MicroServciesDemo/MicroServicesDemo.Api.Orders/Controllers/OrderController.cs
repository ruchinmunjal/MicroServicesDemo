using MicroServicesDemo.Api.Orders.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider orders;

        public OrderController(IOrderProvider orders)
        {
            this.orders = orders;
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orders.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        
    }
}