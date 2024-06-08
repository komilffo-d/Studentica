using Studentica.Common.Enums;
using Studentica.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Database.Postgre.Models
{
    public interface IRequestPostgreBase<T> : IRequestBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        StatusRequest StatusRequest { get; set; }
    }

    public class RequestPostgreBase<T> : RequestBase<T>, IRequestPostgreBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public StatusRequest StatusRequest { get; set; }
    }
}
