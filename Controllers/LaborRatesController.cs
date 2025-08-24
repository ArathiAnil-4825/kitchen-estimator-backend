using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaborRatesController : ControllerBase
    {
        private readonly LaborRateService _service;

        public LaborRatesController(LaborRateService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<LaborRate>>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<LaborRate>> Get(string id)
        {
            var rate = await _service.GetAsync(id);
            if (rate is null) return NotFound();
            return rate;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LaborRate laborRate)
        {
            await _service.CreateAsync(laborRate);
            return CreatedAtAction(nameof(Get), new { id = laborRate.Id }, laborRate);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, LaborRate laborRate)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            laborRate.Id = id;
            await _service.UpdateAsync(id, laborRate);
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
