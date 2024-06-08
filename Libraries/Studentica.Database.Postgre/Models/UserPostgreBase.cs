using Studentica.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Database.Postgre.Models
{
    public interface IUserPostgreBase<T> : IUserBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        List<ProjectPostgreBase<T>> Projects { get; set; }
    }

    public class UserPostgreBase<T> : UserBase<T>, IUserPostgreBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public List<ProjectPostgreBase<T>> Projects { get; set; } = new();
    }
}
