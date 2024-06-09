using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Studentica.Api.Client;
using Studentica.Api.Helpers;
using Studentica.Identity.Common.Models;
using Studentica.Services.Common;
using System.Text.Json;

namespace Studentica.Api.Identity
{
    public abstract class IdentityApiBase<T> : ApiBase<T>, IIdentityApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        protected IdentityApiBase(IApiClient<T> client) : base(client, ServiceNames.Identity)
        {

        }

        public async Task<ObjectResult> Login(LoginModel model)
        {
            var request = CreateRequest(Method.Post, additional: "login");
            request.AddJsonBody(model);
            var response = await ExecuteRequest(request);
            if (response.IsSuccessStatusCode)
                return new OkObjectResult(response.Deserialize<ResponseModel>());
            else
                return new UnauthorizedObjectResult(response.Deserialize<ResponseModel>());
        }

        public async Task<ObjectResult> Register(RegisterModel model)
        {
            var request = CreateRequest(Method.Post, additional: "register");
            request.AddJsonBody(model);
            var response = await ExecuteRequest(request);
            if (response.IsSuccessStatusCode)
                return new OkObjectResult(response.Deserialize<ResponseModel>());
            else
                return new BadRequestObjectResult(response.Deserialize<ResponseModel>());
            
        }

        public async Task<ObjectResult> Validate(string token)
        {
            var request = CreateRequest(additional: $"validate?{nameof(token)}={token}");
            var response = await ExecuteRequest(request);
            if (response.IsSuccessStatusCode)
                return new OkObjectResult(response.Deserialize<ResponseModel>());
            else
                return new BadRequestObjectResult(response.Deserialize<ResponseModel>());

        }
    }
}
