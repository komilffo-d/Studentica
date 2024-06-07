using Studentica.Api.Client;
using Studentica.Api.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Helpers
{
    public static class ApiCreator
    {
        public static IProjectApi<T> GetDefaultProjectApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new ProjectApi<T>(client);
    }
}
