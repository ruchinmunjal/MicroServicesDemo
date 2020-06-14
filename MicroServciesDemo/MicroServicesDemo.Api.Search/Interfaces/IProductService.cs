using MicroServicesDemo.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
        Task<(bool IsSuccess, IEnumerable<Product> Product, string ErrorMessage)> GetProductsAsync();
    }
}
