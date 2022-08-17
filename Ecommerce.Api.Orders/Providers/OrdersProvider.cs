using AutoMapper;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;
        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider>  logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeeData();
        }

        private void SeeData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Db.Order { Id = 1, CustomerId = "1", Total = 20, Items = new List<Db.OrderItem> {
                    new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1 },
                    new Db.OrderItem { Id = 2, OrderId = 1, ProductId = 1 }
                }});
                _dbContext.Orders.Add(new Db.Order { Id = 2, CustomerId = "2", Total = 30, Items = new List<Db.OrderItem> {
                    new Db.OrderItem { Id = 3, OrderId = 2, ProductId = 1 },
                    new Db.OrderItem { Id = 4, OrderId = 2, ProductId = 1 }
                }
                });
                _dbContext.Orders.Add(new Db.Order { Id = 3, CustomerId = "3", Total = 40, Items = new List<Db.OrderItem> {
                    new Db.OrderItem { Id = 5, OrderId = 3, ProductId = 1 },
                    new Db.OrderItem { Id = 6, OrderId = 3, ProductId = 1 }
                }
                });

                // Entity FW doesn't allow orderId = 3 here because of the relationship, it is changed to orderId = 4
                _dbContext.Orders.Add(new Db.Order { Id = 4, CustomerId = "4", Total = 50, Items = new List<Db.OrderItem> {
                    new Db.OrderItem { Id = 7, OrderId = 3, ProductId = 1 },
                    new Db.OrderItem { Id = 8, OrderId = 3, ProductId = 1 }
                }
                });

                _dbContext.SaveChanges();
            }
        }
        async Task<(bool isSuccess, IEnumerable<Models.Order> Orders,
            string ErrorMessage)> IOrdersProvider.GetOrdersAsync()
        {
            try
            {
                var orders = await _dbContext.Orders.ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(p => p.Id == id);

                if (order != null)
                {
                    var result = _mapper.Map<Models.Order>(order);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
