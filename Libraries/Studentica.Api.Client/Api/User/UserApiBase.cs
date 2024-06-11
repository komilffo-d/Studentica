using RestSharp;
using Studentica.Api.Client;
using Studentica.Api.Helpers;
using Studentica.Common.DTOs.Requests.User;
using Studentica.Common.DTOs.User;
using Studentica.Services.Common;

namespace Studentica.Api.User
{
    public abstract class IdentityApiBase<T> : ApiBase<T>, IUserApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        protected IdentityApiBase(IApiClient<T> client) : base(client, ServiceNames.Users)
        {

        }

        public virtual async Task<UserDto<T>> Get(T userId )
        {
            var request = CreateRequest(additional:  $"{userId}");

            var response = await ExecuteRequest(request);

            return response.Deserialize<UserDto<T>>();
        }

        public virtual async Task<UserDto<T>> GetCurrent()
        {
            var request = CreateRequest(additional: $"current");

            var response = await ExecuteRequest(request);

            return response.Deserialize<UserDto<T>>();
        }

        public virtual async Task<IReadOnlyCollection<UserDto<T>>> GetAll(int count = int.MaxValue, string? searchQuery = null)
        {
            var request = CreateRequest(additional: $"?{nameof(count)}={count}{(searchQuery == null ? "" : $"&{nameof(searchQuery)}={searchQuery}")}");
            var response = await ExecuteRequest(request);
            return response.Deserialize<IReadOnlyCollection<UserDto<T>>>();

        }
        public virtual async Task<UserDto<T>> Create(UserCreateRequest userCreateRequest)
        {
            var request = CreateRequest(Method.Post);
            request.AddJsonBody(userCreateRequest);
            var response = await ExecuteRequest(request);
            return response.Deserialize<UserDto<T>>();
        }


    }
}
