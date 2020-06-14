using MicroServicesDemo.Api.Search.Interfaces;
using MicroServicesDemo.Api.Search.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var response = await _orderService.GetOrderAsync(customerId);
            var products = await _productService.GetProductsAsync();
            var customer = await _customerService.GetCustomerAsync(customerId);
            if (response.IsSuccess)
            {
                foreach (var a in response.Orders)
                {

                    foreach (var i in a.OrderItems)
                    {
                        i.ProductName = products.IsSuccess
                            ? products.Product.FirstOrDefault(x => x.Id == i.ProductId)?.Name
                            : "Product Information not available";

                    }
                }
                return (true, new
                {
                    Orders = response.Orders,
                    Customer = customer.IsSuccess
                        ? customer.Customer
                        : new Customer { FullName = "Customer information is not available" }
                });
            }

            return (false, null);

        }
    }
}
