using AutoMapper;

namespace MicroServicesDemo.Api.Orders.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order,Models.Order>();
            CreateMap<Db.OrderItem,Models.OrderItem>()
                .ForMember(x=>x.Id,src=>src.MapFrom(z=>z.ItemId));

        }
    }
}
