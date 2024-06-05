using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentica.Common.DTOs.Project;
using Studentica.Project.Services;
using System.ComponentModel.DataAnnotations;

namespace Studentica.Project.Controllers
{
    [Authorize]
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
            var project = await _projectService.Get(projectId);
            return Ok(project);
        }
    }
}
