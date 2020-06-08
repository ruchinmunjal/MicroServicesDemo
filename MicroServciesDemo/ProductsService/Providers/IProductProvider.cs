using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Providers
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMesage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProduct(int id);
    }
}
