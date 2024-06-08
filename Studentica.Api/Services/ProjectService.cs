using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.Project;
using Studentica.Services.Common.Exceptions;
using Studentica.UI.Common.Enums;

namespace Studentica.Project.Services
{
    public interface IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T projectId);
        Task<ProjectDto<T>> Create(ProjectCreateRequest request, HttpContext context);
    }
    public class ProjectService<T> : IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IProjectRepository<T> _projectRepository;
        public ProjectService(IProjectRepository<T> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto<T>> Get(T projectId)
        {
            var project = await _projectRepository.GetAsync(projectId) ?? throw new NotFoundException(ExceptionMessages.ProjectNotFound);

            return project.AsDto();
        }

        public async Task<ProjectDto<T>> Create(ProjectCreateRequest request, HttpContext context)
        {
            var project = new ProjectPostgreBase<T>()
            {
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                Name = request.Name,
                Description = request.Description,
                Status = StatusProject.NEW,
            };
            await _projectRepository.CreateAsync(project);

            return project.AsDto();
        }
    }
}
