using Microsoft.EntityFrameworkCore;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.Project;
using Studentica.Services.Common.Exceptions;

namespace Studentica.Api.Services
{
    public interface IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T projectId);
        Task<IReadOnlyCollection<ProjectDto<T>>> GetAllAsync(T userId, int count = int.MaxValue);
        Task<ProjectDto<T>> Create(T userId, ProjectCreateRequest request, HttpContext context);
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
        public async Task<IReadOnlyCollection<ProjectDto<T>>> GetAllAsync(T userId, int count = int.MaxValue)
        {
            var projectsQuery = _projectRepository
                .GetAll(p => p.OwnerId.Equals(userId) || p.Members.Any(m => m.Id.Equals(userId)), p => p.Id, 0, count)
                .Select(p => p.AsDto());

            return await projectsQuery.ToListAsync();
        }

        public async Task<ProjectDto<T>> Create(T userId, ProjectCreateRequest request, HttpContext context)
        {
            var project = new ProjectPostgreBase<T>()
            {
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                Name = request.Name,
                Description = request.Description,
                OwnerId = userId,
                Status = StatusProject.NEW,
            };
            await _projectRepository.CreateAsync(project);

            return project.AsDto();
        }
    }
}
