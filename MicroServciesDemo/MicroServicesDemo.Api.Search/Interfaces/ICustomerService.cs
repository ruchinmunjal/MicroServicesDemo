using MicroServicesDemo.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
