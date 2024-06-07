using Studentica.Common.DTOs.Project;

namespace Studentica.Api.Project
{
    public interface IProjectApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ProjectDto<T>> Get(T artistId);
    }
}
