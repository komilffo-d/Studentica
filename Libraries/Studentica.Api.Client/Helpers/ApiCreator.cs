using Studentica.Api.Client;
using Studentica.Api.Project;
using Studentica.Api.Request;

namespace Studentica.Api.Helpers
{
    public static class ApiCreator
    {
        public static IProjectApi<T> GetDefaultProjectApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new ProjectApi<T>(client);
        public static IRequestApi<T> GetDefaultRequestApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new RequestApi<T>(client);
    }
}
