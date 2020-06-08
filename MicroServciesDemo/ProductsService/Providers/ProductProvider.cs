using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using ProductsService.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ILogger<ProductProvider> _logger;
        private readonly IMapper _mapper;
        private readonly ProductsDbContext _dbContext;
        public ProductProvider(ProductsDbContext productDbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            _dbContext = productDbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                var seedProducts = new List<Product>()
                {
                    new Product
                    {
                        Id = 1, Name = "Keyboard", Price = 20, Inventory = 100
                    },
                    new Product
                    {
                        Id = 2, Name = "Mouse", Price = 5, Inventory = 1000
                    },
                    new Product
                    {
                        Id = 3, Name = "Keyboard", Price = 367, Inventory = 900
                    },
                    new Product
                    {
                        Id = 4, Name = "CPU", Price = 2900, Inventory = 89
                    }
                };
                _dbContext.Products.AddRange(seedProducts);
                _dbContext.SaveChanges();
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMesage)> GetProductsAsync()
        {
            try
            {
                var allProducts = await _dbContext.Products.ToListAsync();

                if (allProducts != null && allProducts.Any())
                {
                    _logger.LogInformation("received products");
                    var products = _mapper.Map<IEnumerable<Models.Product>>(allProducts);
                    return (true, products, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProduct(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product != null)
                {
                    var prod = _mapper.Map<Product, Models.Product>(product);
                    return (true, prod, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }


    }
}
