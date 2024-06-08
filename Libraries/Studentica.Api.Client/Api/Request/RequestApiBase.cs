using RestSharp;
using Studentica.Api.Client;
using Studentica.Api.Helpers;
using Studentica.Common.DTOs.Request;
using Studentica.Common.DTOs.Requests.Request;
using Studentica.Services.Common;

namespace Studentica.Api.Request
{
    public abstract class RequestApiBase<T> : ApiBase<T>, IRequestApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        protected RequestApiBase(IApiClient<T> client) : base(client, ServiceNames.Requests)
        {

        }

        public virtual async Task<RequestDto<T>> Get(T requestId)
        {
            var request = CreateRequest(additional: $"{requestId}");

            var response = await ExecuteRequest(request);

            return response.Deserialize<RequestDto<T>>();
        }

        public async Task<IReadOnlyCollection<RequestDto<T>>> GetAll(int count = int.MaxValue)
        {
            var request = CreateRequest(additional: $"?{nameof(count)}={count}");
            var response = await ExecuteRequest(request);
            return response.Deserialize<IReadOnlyCollection<RequestDto<T>>>();

        }
        public virtual async Task<RequestDto<T>> Create(RequestCreateRequest requestCreateRequest)
        {
            var request = CreateRequest(Method.Post);
            request.AddJsonBody(requestCreateRequest);
            var response = await ExecuteRequest(request);
            return response.Deserialize<RequestDto<T>>();
        }

        public virtual async Task<RequestDto<T>> UpdateStatus(RequestUpdateStatusRequest<T> requestUpdateStatusRequest)
        {
            var request = CreateRequest(Method.Post, additional: $"status/update");
            request.AddJsonBody(requestUpdateStatusRequest);
            var response = await ExecuteRequest(request);
            return response.Deserialize<RequestDto<T>>();
        }
    }
}
