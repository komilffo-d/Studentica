using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Project.Services;
using System.ComponentModel.DataAnnotations;

namespace Studentica.Project.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService<Guid> _projectService;
        public ProjectController(IProjectService<Guid> projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{projectId:guid}")]
        public async Task<ActionResult<ProjectDto<Guid>>> GetByIdAsync(Guid projectId)
        {
            return await _projectService.Get(projectId);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto<Guid>>> PostAsync(ProjectCreateRequest request)
        {
            var project = await _projectService.Create(request, HttpContext);

            var actionName = nameof(GetByIdAsync);

            return CreatedAtAction(actionName, new { projectId = project.Id }, project);
        }
    }
}
