using Ecommerce.Api.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
