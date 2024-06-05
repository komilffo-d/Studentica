using Studentica.Common.DTOs.Project;

namespace Studentica.Project.Services
{
    public interface IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T albumId);
    }
    public class ProjectService<T> : IProjectService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public Task<ProjectDto<T>> Get(T albumId)
        {
            throw new NotImplementedException();
        }
    }
}
