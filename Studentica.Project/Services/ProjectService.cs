using Microsoft.AspNetCore.Http.HttpResults;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Database.Common.Repository;
using Studentica.Database.MongoDb.Models;
using Studentica.Services.Common.Exceptions;
using Studentica.Services.Models;
using Studentica.UI.Common.Enums;

namespace Studentica.Project.Services
{
    public interface IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T albumId);
        Task<ProjectDto<T>> Create(ProjectCreateRequest request, HttpContext context);
    }
    public class ProjectService<T> : IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IRepository<ProjectMongoBase<T>, T> _projectRepository;
        public ProjectService(IRepository<ProjectMongoBase<T>, T> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto<T>> Get(T albumId)
        {
            var project=await _projectRepository.GetAsync(albumId) ?? throw new NotFoundException(ExceptionMessages.ProjectNotFound);

            return project.AsDto();
        }

        public async Task<ProjectDto<T>> Create(ProjectCreateRequest request, HttpContext context)
        {
            var project = new ProjectMongoBase<T>()
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
