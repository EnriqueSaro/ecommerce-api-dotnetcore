using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;
        public ProductsController(IProductsProvider productsProvider)
        {
            this._productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsProvider.GetProductsAsync();

            if (result.isSuccess)
            {
                return Ok(result.Products);
            }
            
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsyc(int id)
        {
            var result = await _productsProvider.GetProductAsync(id);

            if (result.isSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
