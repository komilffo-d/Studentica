using RestSharp;
using Studentica.Api.Client;
using Studentica.Api.Exceptions;
using Studentica.Api.Helpers;
using System.Net;

namespace Studentica.Api
{
    internal static class Extensions
    {
        internal static void AddAuthorization(this RestRequest request, string jwtToken)
        {
            request.AddHeader("Authorization", $"Bearer {jwtToken}");
        }
    }
    public abstract class ApiBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly string _serviceName;
        protected readonly IApiClient<T> Client;

        protected ApiBase(IApiClient<T> client, string serviceName)
        {
            Client = client;
            _serviceName = serviceName;
        }

        protected static Exception? ProcessResponseStatusCode(RestResponse response)
        {
            Exception? ex = null;
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Created:
                    break;
                case HttpStatusCode.NoContent:
                    break;
                default:
                    break;
            }
            return ex;

            string GetMessage()
            {
                string? message;

                try
                {
                    message = response.Deserialize<ExceptionMessage>().Detail;
                }
                catch
                {
                    message = response.ErrorMessage;
                }

                return message ?? "";
            }
        }

        protected RestRequest CreateRequest(Method method = Method.Get, string? additional = "", bool isUseAuthorization = true)
        {
            if (additional == null) ArgumentNullException.ThrowIfNull(additional);
            if (additional.Length > 0 && additional[0] == '/') throw new Exception("Additional can't start with '/'!");

            var request = new RestRequest($"{_serviceName}{(additional == "" ? additional : additional[0] == '?' ? $"{additional}" : $"/{additional}")}",
                method);
            request.AddHeader("Accept", "application/json");

            if (!isUseAuthorization) return request;

            /*            if (Client.Token == null || Client.Token.RefreshToken == "")
                            throw new NullTokenException();*/

            /*            request.AddAuthorization(Client.Token.AccessToken);*/

            return request;
        }

        /*        private static Task<LoginDto>? RefreshTokenTask { get; set; }
                private static DateTime RefreshingDate { get; set; }
                private async Task ProcessUnauthorized()
                {
                    if (RefreshingDate.AddSeconds(15) > DateTime.Now)
                    {
                        return;
                    }

                    if (RefreshTokenTask != null)
                    {
                        RefreshTokenTask?.GetAwaiter().OnCompleted(() =>
                        {
                        });
                        return;
                    }

                    RefreshTokenTask = Client.RefreshToken();
                    var token = await RefreshTokenTask;
                    RefreshingDate = DateTime.Now;
                    RefreshTokenTask = null;
                    Client.Token.ChangeToken(token.JwtAccessToken, token.JwtRefreshToken);
                }*/

        protected async Task<RestResponse> ExecuteRequest(RestRequest request, bool isAuthenticationRequest = false)
        {
            var response = await Client.RestClient.ExecuteAsync(request);
            var ex = ProcessResponseStatusCode(response);
            /*            try
                        {
                            if (ex != null)
                            {
                                throw ex;
                            }
                        }
                        catch (Status401Exception)
                        {
                            if (isAuthenticationRequest)
                                throw new InvalidAuthorizationDataException();

                            await ProcessUnauthorized();
                            request.ChangeAuthorization(Client.Token.AccessToken);
                            response = await Client.RestClient.ExecuteAsync(request);
                        }*/
            return response;
        }
    }
}
