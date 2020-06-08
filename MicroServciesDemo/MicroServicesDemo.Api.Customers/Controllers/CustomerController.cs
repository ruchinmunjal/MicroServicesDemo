using MicroServicesDemo.Api.Customers.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }


        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            var result = await _customerProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            var result = await _customerProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }
    }
}
