using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly MaterialService _service;

        public MaterialsController(MaterialService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Material>>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Material>> Get(string id)
        {
            var material = await _service.GetAsync(id);
            if (material is null) return NotFound();
            return material;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Material material)
        {
            await _service.CreateAsync(material);
            return CreatedAtAction(nameof(Get), new { id = material.Id }, material);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Material material)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            material.Id = id;
            await _service.UpdateAsync(id, material);
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
