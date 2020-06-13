using MicroServicesDemo.Api.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Orders.Provider
{
    public interface IOrderProvider
    {
        
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
