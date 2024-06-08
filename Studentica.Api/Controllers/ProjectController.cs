using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentica.Api.Services;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;

namespace Studentica.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Student, Teacher")]
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

        [HttpGet]
        public async Task<IReadOnlyCollection<ProjectDto<Guid>>> GetAllAsync([FromQuery] int count = int.MaxValue)
        {
            return await _projectService.GetAllAsync(GetUserId(), count);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto<Guid>>> PostAsync(ProjectCreateRequest request)
        {
            var project = await _projectService.Create(GetUserId(), request, HttpContext);

            var actionName = nameof(GetByIdAsync);

            return CreatedAtAction(actionName, new { projectId = project.Id }, project);
        }
    }
}
