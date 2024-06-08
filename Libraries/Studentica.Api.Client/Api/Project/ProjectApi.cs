using Studentica.Api.Client;

namespace Studentica.Api.Project
{
    public class ProjectApi<T> : ProjectApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ProjectApi(IApiClient<T> client) : base(client)
        {
        }
    }
}
