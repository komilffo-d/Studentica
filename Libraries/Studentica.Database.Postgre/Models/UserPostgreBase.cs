using Microsoft.AspNetCore.Identity;
using Studentica.Services.Models;

namespace Studentica.Database.Postgre.Models
{
    public interface IUserPostgreBase<T> : IUserBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        List<ProjectPostgreBase<T>> Projects { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
        T IdentityId { get; set; }
        bool IsActive { get; set; }
    }

    public class UserPostgreBase<T> : UserBase<T>, IUserPostgreBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public List<ProjectPostgreBase<T>> Projects { get; set; } = new();
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public T IdentityId { get; set; }
        public bool IsActive { get; set; }
    }
}
