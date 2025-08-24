using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalsController : ControllerBase
    {
        private readonly ApprovalService _service;

        public ApprovalsController(ApprovalService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Approval>>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Approval>> Get(string id)
        {
            var approval = await _service.GetAsync(id);
            if (approval is null) return NotFound();
            return approval;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Approval approval)
        {
            await _service.CreateAsync(approval);
            return CreatedAtAction(nameof(Get), new { id = approval.Id }, approval);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Approval approval)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            approval.Id = id;
            await _service.UpdateAsync(id, approval);
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

        // Extra endpoint: Approve/Reject
        [HttpPost("{id:length(24)}/approve")]
        public async Task<IActionResult> Approve(string id)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            existing.Status = ApprovalStatus.Approved;
            existing.ReviewedAtUtc = System.DateTime.UtcNow;
            await _service.UpdateAsync(id, existing);
            return Ok(existing);
        }

        [HttpPost("{id:length(24)}/reject")]
        public async Task<IActionResult> Reject(string id, [FromBody] string? comments)
        {
            var existing = await _service.GetAsync(id);
            if (existing is null) return NotFound();

            existing.Status = ApprovalStatus.Rejected;
            existing.ReviewedAtUtc = System.DateTime.UtcNow;
            existing.Comments = comments;
            await _service.UpdateAsync(id, existing);
            return Ok(existing);
        }
    }
}
