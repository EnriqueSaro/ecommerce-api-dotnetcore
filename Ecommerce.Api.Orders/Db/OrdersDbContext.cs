using Ecommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Items { get; set; }

        public OrdersDbContext(DbContextOptions options ) : base(options)
        {

        }
    }
}
