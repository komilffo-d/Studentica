using Studentica.Common.DTOs.User;
using Studentica.Services.Models;

namespace Studentica.Common.DTOs.Converters
{
    public static class UserConverter
    {
        public static UserDto<T> AsDto<T>(this IUserBase<T> user) where T : struct, IEquatable<T>, IComparable<T>
        {
            return new(user.Id, user.LastName, user.FirstName, user.MiddleName, user.NumberCourse, user.Group, user.Major,user.NameUniversity, user.Role, user.Email);
        }
    }
}
