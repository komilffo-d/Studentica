using RestSharp;
using Studentica.Api.Project;

namespace Studentica.Api.Client
{
    public interface IApiClient<T> where T : struct, IEquatable<T>, IComparable<T>
    {


        IRestClient RestClient { get; }
        IProjectApi<T> ProjectApi { get; }

        void SetProjectApi(IProjectApi<T> identityApi);

    }
}
