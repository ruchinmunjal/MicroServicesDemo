using MicroServicesDemo.Api.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Customers.Provider
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}