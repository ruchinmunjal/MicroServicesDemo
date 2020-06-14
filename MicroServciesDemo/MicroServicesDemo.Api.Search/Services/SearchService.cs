using MicroServicesDemo.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;

        public SearchService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var response = await _orderService.GetOrderAsync(customerId);
            if (response.IsSuccess)
            {
                return (true, new { Orders = response.Orders });
            }

            return (false, null);

        }
    }
}
