using Studentica.Common.DTOs.Project;
using Studentica.Services.Models;

namespace Studentica.Common.DTOs.Converters
{
    public static class ProjectConverter
    {

        public static ProjectDto<T> AsDto<T>(this IProjectBase<T> project) where T : struct, IEquatable<T>, IComparable<T>
        {
            return new(project.Id, project.Name, project.Status, project.BeginDate, project.EndDate, project.Description);
        }
    }
}
