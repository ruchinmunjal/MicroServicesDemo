using AutoMapper;

namespace MicroServicesDemo.Api.Orders.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order,Models.Order>()
                .ForMember(x=>x.OrderItems,src=>src.MapFrom(z=>z.Items));

            CreateMap<Db.OrderItem,Models.OrderItem>();
                

        }
    }
}
