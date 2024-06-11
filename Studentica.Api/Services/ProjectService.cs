using Microsoft.EntityFrameworkCore;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.Project;
using Studentica.Infrastructure.Database.Repository.ThirdEntity;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.Common.Exceptions;
using Studentica.Services.Models;

namespace Studentica.Api.Services
{
    public interface IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T projectId);
        Task<IReadOnlyCollection<ProjectDto<T>>> GetAllAsync(T userId, int count = int.MaxValue);
        Task<ProjectDto<T>> Create(T userId, ProjectCreateRequest<T> request, HttpContext context);
    }
    public class ProjectService<T> : IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IProjectRepository<T> _projectRepository;
        private readonly IProjectUserRepository<T> _projectUserRepository;
        private readonly IUserRepository<T> _userRepository;
        public ProjectService(IProjectRepository<T> projectRepository, IUserRepository<T> userRepository, IProjectUserRepository<T> projectUserRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectUserRepository=projectUserRepository;
        }

        public async Task<ProjectDto<T>> Get(T projectId)
        {
            var project = await _projectRepository.GetAll(p => p.Id.Equals(projectId), p => p.Id, 0, int.MaxValue).Include(p => p.Members).FirstOrDefaultAsync() ?? throw new NotFoundException(ExceptionMessages.ProjectNotFound);

            return project.AsDto(project.Members.Select(m => (IUserBase<T>)m).Select(m => m.AsDto()).ToList());
        }
        public async Task<IReadOnlyCollection<ProjectDto<T>>> GetAllAsync(T userId, int count = int.MaxValue)
        {
            var projectsQuery = _projectRepository
                .GetAll(p => p.OwnerId.Equals(userId) || p.Members.Any(m => m.Id.Equals(userId)), p => p.Id, 0, count).Include(p => p.Members)
                .Select(p => p.AsDto(p.Members.Select(m => (IUserBase<T>)m).Select(m => m.AsDto()).ToList()));

            return await projectsQuery.ToListAsync();
        }

        public async Task<ProjectDto<T>> Create(T userId, ProjectCreateRequest<T> request, HttpContext context)
        {
            var users = await _userRepository.GetAll(u => request.UserIds.Contains(u.Id), u => u.Id, 0, int.MaxValue).ToListAsync();

            var project = new ProjectPostgreBase<T>()
            {
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                Name = request.Name,
                Description = request.Description,
                OwnerId = userId,
                Status = StatusProject.NEW
            };

            await _projectRepository.CreateAsync(project);

            foreach(var user in users)
            {
                var relation = new ProjectUserPostgreBase<T>()
                {
                    ProjectId = project.Id,
                    UserId = user.Id,
                };
                await _projectUserRepository.CreateAsync(relation);
            }

            return project.AsDto();
        }
    }
}
