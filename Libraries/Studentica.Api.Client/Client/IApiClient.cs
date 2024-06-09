using RestSharp;
using Studentica.Api.Client.Models.Tokens;
using Studentica.Api.Identity;
using Studentica.Api.Project;
using Studentica.Api.Request;
using Studentica.Api.User;

namespace Studentica.Api.Client
{
    public interface IApiClient<T> where T : struct, IEquatable<T>, IComparable<T>
    {

        IJwtTokenModel Token { get; }

        IRestClient RestClient { get; }
        IProjectApi<T> ProjectApi { get; }
        IRequestApi<T> RequestApi { get; }
        IUserApi<T> UserApi { get; }
        IIdentityApi<T> IdentityApi { get; }

        void SetProjectApi(IProjectApi<T> identityApi);
        void SetRequestApi(IRequestApi<T> requestApi);
        void SetUserApi(IUserApi<T> userApi);
        void SetIdentityApi(IIdentityApi<T> identityApi);
    }
}
