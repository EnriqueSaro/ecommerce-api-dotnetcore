using Ecommerce.Api.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool isSuccess, Order Order, string ErrorMessage)> GetOrderAsync(int id);

    }
}
