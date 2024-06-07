using RestSharp;
using Studentica.Api.Client.Models.Tokens;
using Studentica.Api.Exceptions;
using Studentica.Api.Helpers;
using Studentica.Api.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Client
{
    public abstract class ApiClientBase<T> : IApiClient<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public IJwtTokenModel Token { get; }

        public IRestClient RestClient { get; }
        public IProjectApi<T> ProjectApi { get; protected set; }

        private ApiClientBase(string gatewayPath)
        {
            if (!Uri.TryCreate($"{gatewayPath}/api", UriKind.Absolute, out var path))
                throw new InvalidUrlException();

            ProjectApi = this.GetDefaultProjectApi<T>();

            RestClient = new RestClient(path);

            Token = new DefaultJwtTokenModel("");
        }

        protected ApiClientBase(string gatewayPath, string accessToken): this(gatewayPath)
        {
            Token.ChangeToken(accessToken);
        }

        public virtual void SetProjectApi(IProjectApi<T> identityApi)
        {
            ProjectApi = identityApi;
        }
    }
}
