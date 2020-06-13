using MicroServicesDemo.Api.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Orders.Provider
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, Order Order, string ErrorMessage)> GetOrder(int id);
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrders();
    }
}
