﻿using RestSharp;
using Studentica.Api.Client.Models.Tokens;
using Studentica.Api.Exceptions;
using Studentica.Api.Helpers;
using Studentica.Api.Identity;
using Studentica.Api.Project;
using Studentica.Api.Request;
using Studentica.Api.User;

namespace Studentica.Api.Client
{
    public abstract class ApiClientBase<T> : IApiClient<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public IJwtTokenModel Token { get; }

        public IRestClient RestClient { get; }
        public IProjectApi<T> ProjectApi { get; protected set; }
        public IRequestApi<T> RequestApi { get; protected set; }
        public IUserApi<T> UserApi { get; protected set; }
        public IIdentityApi<T> IdentityApi { get; protected set; }

        private ApiClientBase(string gatewayPath)
        {
            if (!Uri.TryCreate($"{gatewayPath}/api", UriKind.Absolute, out var path))
                throw new InvalidUrlException();

            ProjectApi = this.GetDefaultProjectApi<T>();
            RequestApi = this.GetDefaultRequestApi<T>();
            UserApi = this.GetDefaultUserApi<T>();
            IdentityApi = this.GetDefaultIdentityApi<T>();
            RestClient = new RestClient(path);

            Token = new DefaultJwtTokenModel("");
        }

        protected ApiClientBase(string gatewayPath, string accessToken) : this(gatewayPath)
        {
            Token.ChangeToken(accessToken);
        }

        public virtual void SetProjectApi(IProjectApi<T> identityApi)
        {
            ProjectApi = identityApi;
        }

        public void SetRequestApi(IRequestApi<T> requestApi)
        {
            RequestApi= requestApi;
        }

        public void SetUserApi(IUserApi<T> userApi)
        {
            UserApi = userApi;
        }

        public void SetIdentityApi(IIdentityApi<T> identityApi)
        {
            IdentityApi = identityApi;
        }
    }
}
