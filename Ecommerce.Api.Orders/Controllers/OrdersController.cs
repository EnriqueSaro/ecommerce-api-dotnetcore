using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _productsProvider;
        public OrdersController(IOrdersProvider productsProvider)
        {
            this._productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsProvider.GetOrdersAsync();

            if (result.isSuccess)
            {
                return Ok(result.Orders);
            }
            
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsyc(int id)
        {
            var result = await _productsProvider.GetOrderAsync(id);

            if (result.isSuccess)
            {
                return Ok(result.Order);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
