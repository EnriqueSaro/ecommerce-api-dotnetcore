using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;

        public CustomersController(ICustomerProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customerProvider.GetCustomersAsync();

            if(result.isSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customerProvider.GetCustomerAsync(id);

            if (result.isSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
