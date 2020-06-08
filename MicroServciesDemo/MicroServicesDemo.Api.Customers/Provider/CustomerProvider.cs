using AutoMapper;
using MicroServicesDemo.Api.Customers.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer = MicroServicesDemo.Api.Customers.Models.Customer;

namespace MicroServicesDemo.Api.Customers.Provider
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly IMapper _mapper;

        public CustomerProvider(CustomerDbContext customerDbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (_customerDbContext.Customers.Any())
            {
                return;

            }
            _customerDbContext.Customers.AddRange(new List<Db.Customer>
            {
                new Db.Customer {Id = 1,FirstName = "Ruchin", LastName = "Munjal", AddressLine = "Delhi"},
                new Db.Customer {Id = 2,FirstName = "Parul", LastName = "Srivastava", AddressLine = "London"},
                new Db.Customer {Id = 3,FirstName = "Mia", LastName = "Munjal", AddressLine = "Chertsey"},
                new Db.Customer {Id = 4,FirstName = "Shray", LastName = "Munjal", AddressLine = "Toronto"}
            });
            _customerDbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var customerModel = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Customer>>(customers);
                    return (true, customerModel, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return (false, null, exception.Message);
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    var customerModel = _mapper.Map<Db.Customer, Customer>(customer);
                    return (true, customerModel, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return (false, null, exception.Message);
            }
        }
    }
}
