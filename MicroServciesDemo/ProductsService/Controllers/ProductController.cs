using Microsoft.AspNetCore.Mvc;
using ProductsService.Providers;
using System.Threading.Tasks;

namespace ProductsService.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _productProvider;

        public ProductController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var allProducts = await _productProvider.GetProductsAsync();
            if (allProducts.IsSuccess)
            {
                return Ok(allProducts.Item2);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _productProvider.GetProduct(id);
            if (product.IsSuccess)
            {
                return Ok(product.Product);
            }

            return NotFound();
        }
    }
}
