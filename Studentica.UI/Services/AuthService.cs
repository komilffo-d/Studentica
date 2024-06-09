using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Studentica.Api.Client;
using Studentica.Identity.Common.Models;
using System.Net;
using System.Text.Json;
using Cookie = BitzArt.Blazor.Cookies.Cookie;

public class AuthService
{
    private readonly IApiClient<Guid> _apiClient;
    private readonly ICookieService _cookieService;
    private const string COOKIE_TOKEN_NAME = "token";

    public AuthService(ICookieService cookieService, [FromKeyedServices("ApiClientGuid")] IApiClient<Guid> apiClient)
    {
        _apiClient = apiClient;
        _cookieService = cookieService;
    }
    public async Task<HttpResponseMessage> LoginAsync(LoginModel request)
    {
        var response = await _apiClient.IdentityApi.Login(request);

        ResponseModel? reponseToken = response.Value as ResponseModel;

        if (response.StatusCode != StatusCodes.Status200OK)
            return new HttpResponseMessage(HttpStatusCode.BadRequest);


        if (reponseToken is null)
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);

        await SetTokenAsync(reponseToken.Token);
        return new HttpResponseMessage(HttpStatusCode.OK);

    }
    public async Task<HttpResponseMessage> LogoutAsync()
    {
        await RemoveTokenAsync();
        return new HttpResponseMessage(HttpStatusCode.OK);

    }
    public async Task<bool> RegisterAsync(RegisterModel request)
    {
        var response = await _apiClient.IdentityApi.Register(request);
        int? statusCode = response.StatusCode;
        return statusCode == StatusCodes.Status200OK;
    }
    public async Task<bool> ValidateAsync(string token)
    {
        var response = await _apiClient.IdentityApi.Validate(token);
        int? statusCode = response.StatusCode;
        return statusCode == StatusCodes.Status200OK;
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            Cookie? cookie = await _cookieService.GetAsync(COOKIE_TOKEN_NAME);
            ChangeTokenApi(cookie?.Value);
            return cookie?.Value;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

    }
    private async Task<bool> SetTokenAsync(string token)
    {
        try
        {
            await _cookieService.SetAsync(new Cookie(COOKIE_TOKEN_NAME, token));
            ChangeTokenApi(token);
            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

    }

    private async Task<bool> RemoveTokenAsync()
    {
        try
        {
            await _cookieService.RemoveAsync(COOKIE_TOKEN_NAME);
            ChangeTokenApi();
            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

    }

    private void ChangeTokenApi(string? accessToken = null)
    {
        _apiClient.Token.ChangeToken(accessToken ?? "");
    }

}
