using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeeData();
        }

        private void SeeData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Enrique", Address = "Del arbol 22"});
                _dbContext.Customers.Add(new Db.Customer { Id = 2, Name = "Marley", Address = "Del arbol 23" });
                _dbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Kyra", Address = "Del arbol 24" });
                _dbContext.Customers.Add(new Db.Customer { Id = 4, Name = "Rex", Address = "Del arbol 25" });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null)
                {
                    var result = _mapper.Map<Models.Customer>(customer);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }

        }
    }
}
