using RestSharp;
using Studentica.Api.Client.Models.Tokens;
using Studentica.Api.Project;
using Studentica.Api.Request;

namespace Studentica.Api.Client
{
    public interface IApiClient<T> where T : struct, IEquatable<T>, IComparable<T>
    {

        IJwtTokenModel Token { get; }

        IRestClient RestClient { get; }
        IProjectApi<T> ProjectApi { get; }
        IRequestApi<T> RequestApi { get; }

        void SetProjectApi(IProjectApi<T> identityApi);
        void SetRequestApi(IRequestApi<T> requestApi);

    }
}
