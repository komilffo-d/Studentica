using Studentica.Api.Client;

namespace Studentica.Api.Request
{
    public class RequestApi<T> : RequestApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public RequestApi(IApiClient<T> client) : base(client)
        {
        }
    }
}
