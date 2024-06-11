using Studentica.Common.DTOs.User;
using Studentica.Common.Enums;
using Studentica.Common.Interfaces;

namespace Studentica.Common.DTOs.Project
{
    public record ProjectDto<T>(T Id, string Name, StatusProject Status, DateTimeOffset BeginDate, DateTimeOffset EndDate, string Description, List<UserDto<T>>? Members=null) : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>;
}
