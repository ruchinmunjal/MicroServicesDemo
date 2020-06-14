using MicroServicesDemo.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Services
{
    public class SearchSevice : ISearchService
    {
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            await Task.Delay(1);
            return (true, new { Message = "Hello World" });
        }
    }
}
