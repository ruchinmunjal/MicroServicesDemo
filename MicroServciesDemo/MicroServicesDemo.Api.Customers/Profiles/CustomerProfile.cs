using AutoMapper;
using MicroServicesDemo.Api.Customers.Db;

namespace MicroServicesDemo.Api.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, Models.Customer>()
                .ForMember(x => x.FullName, src => src.MapFrom(m => m.FirstName + " " + m.LastName));
        }
    }
}
