using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;

namespace Studentica.Api.Project
{
    public interface IProjectApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T projectId);

        Task<IReadOnlyCollection<ProjectDto<T>>> GetAll(int count = int.MaxValue);

        Task<ProjectDto<T>> Create(ProjectCreateRequest projectCreateRequest);
    }
}
