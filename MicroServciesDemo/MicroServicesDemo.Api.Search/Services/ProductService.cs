using MicroServicesDemo.Api.Search.Interfaces;
using MicroServicesDemo.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IHttpClientFactory httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var client = _httpClient.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Product, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _httpClient.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(
                              await response.Content.ReadAsByteArrayAsync(),
                              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return (false, null, exception.Message);
            }
        }
    }
}
