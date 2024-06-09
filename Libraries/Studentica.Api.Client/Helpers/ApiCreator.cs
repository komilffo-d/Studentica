using Studentica.Api.Client;
using Studentica.Api.Identity;
using Studentica.Api.Project;
using Studentica.Api.Request;
using Studentica.Api.User;

namespace Studentica.Api.Helpers
{
    public static class ApiCreator
    {
        public static IProjectApi<T> GetDefaultProjectApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new ProjectApi<T>(client);
        public static IRequestApi<T> GetDefaultRequestApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new RequestApi<T>(client);
        public static IUserApi<T> GetDefaultUserApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new UserApi<T>(client);
        public static IIdentityApi<T> GetDefaultIdentityApi<T>(this IApiClient<T> client) where T : struct, IEquatable<T>, IComparable<T> =>
            new IdentityApi<T>(client);

    }
}
