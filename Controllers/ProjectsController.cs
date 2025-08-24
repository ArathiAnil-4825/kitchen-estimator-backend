using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService) =>
            _projectService = projectService;

        [HttpGet]
        public async Task<List<Project>> Get() =>
            await _projectService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Project>> Get(string id)
        {
            var project = await _projectService.GetAsync(id);

            if (project is null)
                return NotFound();

            return project;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project newProject)
        {
            await _projectService.CreateAsync(newProject);
            return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Project updatedProject)
        {
            var project = await _projectService.GetAsync(id);

            if (project is null)
                return NotFound();

            updatedProject.Id = project.Id;

            await _projectService.UpdateAsync(id, updatedProject);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var project = await _projectService.GetAsync(id);

            if (project is null)
                return NotFound();

            await _projectService.RemoveAsync(id);

            return NoContent();
        }
    }
}
