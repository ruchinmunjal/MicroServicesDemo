using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsService.Db;
using ProductsService.Profiles;
using ProductsService.Providers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroServicesDemo.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("Products").Options;
            var dbContext = new ProductsDbContext(dbContextOptions);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var products = await productsProvider.GetProductsAsync();

            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Equal(2, products.Products.Count());
            Assert.Null(products.ErrorMesage);
        }
        [Fact]
        public async Task GetProductReturnWithValidId()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("Products").Options;
            var dbContext = new ProductsDbContext(dbContextOptions);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProduct(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.Equal(1, product.Product.Id);
            Assert.Null(product.ErrorMessage);
        }
        [Fact]
        public async Task GetProductNoReturnWithInvalidId()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("Products").Options;
            var dbContext = new ProductsDbContext(dbContextOptions);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProduct(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }
        private void CreateProducts(ProductsDbContext dbContext)
        {
            dbContext.Products.AddRange(new Product { Id = 1, Name = "Test Product 1", Inventory = 2, Price = 10 },
                new Product { Id = 2, Name = "Test Product 2", Inventory = 21, Price = 310 });
            dbContext.SaveChanges();
        }
    }
}
