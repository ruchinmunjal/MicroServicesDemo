using AutoMapper;
using MicroServicesDemo.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicesDemo.Api.Orders.Provider
{
    public class OrdersProvider:IOrdersProvider
    {
        private readonly OrderDbContext orderDbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrderDbContext orderDbContext,ILogger logger,IMapper mapper)
        {
            this.orderDbContext = orderDbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<(bool IsSuccess,IEnumerable<Models.Order> Orders,string ErrorMessage)> GetOrders()
        {
            try
            {
                var orders = await orderDbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var model = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, model, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);

                return (false,null,ex.Message);
            }
        }

        public async Task<(bool IsSuccess,Models.Order Order,string ErrorMessage)> GetOrder(int id)
        {
            try
            {
                var order = await orderDbContext.Orders.FirstOrDefaultAsync(x=>x.OrderId==id);
                if (order !=null)
                {
                    var model = mapper.Map<Db.Order,Models.Order>(order);
                    return (true,model,null);
                }
                return (false,null,"Not Found");

            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                return (false,null,ex.Message);
            }
        }
    }
}
