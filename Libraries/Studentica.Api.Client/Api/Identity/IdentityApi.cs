using Studentica.Api.Client;

namespace Studentica.Api.Identity
{
    public class IdentityApi<T> : IdentityApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public IdentityApi(IApiClient<T> client) : base(client)
        {

        }
    }
}
