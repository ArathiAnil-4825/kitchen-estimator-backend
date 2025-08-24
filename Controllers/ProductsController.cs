using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _service.GetAsync(id);
            if (product is null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await _service.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Product product)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            product.Id = id;
            await _service.UpdateAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
