using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider>  logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeeData();
        }

        private void SeeData()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Db.Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                _dbContext.Products.Add(new Db.Product { Id = 2, Name = "Mouse", Price = 30, Inventory = 300 });
                _dbContext.Products.Add(new Db.Product { Id = 3, Name = "Monitor", Price = 40, Inventory = 5600 });
                _dbContext.Products.Add(new Db.Product { Id = 4, Name = "Apple", Price = 50, Inventory = 10 });

                _dbContext.SaveChanges();
            }
        }
        async Task<(bool isSuccess, IEnumerable<Models.Product> Products,
            string ErrorMessage)> IProductsProvider.GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
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
    }
}
