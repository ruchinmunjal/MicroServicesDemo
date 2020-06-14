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
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IHttpClientFactory httpClient, ILogger<CustomerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var client = _httpClient.CreateClient("CustomersService");
                var response = await client.GetAsync("api/customers");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<IEnumerable<Customer>>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex, ex.Message);
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = _httpClient.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customer/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<Customer>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return (true, result, null);

                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
