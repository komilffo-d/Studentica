using Studentica.Api.Client;

namespace Studentica.Api.User
{
    public class UserApi<T> : IdentityApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public UserApi(IApiClient<T> client) : base(client)
        {
        }
    }
}
