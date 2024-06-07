using Studentica.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Project
{
    public class ProjectApi<T> : ProjectApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ProjectApi(IApiClient<T> client) : base(client)
        {
        }
    }
}
