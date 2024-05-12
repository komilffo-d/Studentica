using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Studentica.Identity.Common.Models;
using System.Text.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ICookieService _cookieService;
    private const string COOKIE_TOKEN_NAME = "token";

    public AuthService(IHttpClientFactory httpFactory, ICookieService cookieService)
    {
        _httpClient = httpFactory.CreateClient("Ocelot");
        _cookieService = cookieService;
    }
    public async Task<ResponseModel?> LoginAsync(LoginModel request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/authenticate/login", request);
        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseModel>(content,new JsonSerializerOptions() {PropertyNameCaseInsensitive = true } );
        }
        else
        {
            return null;
        }
    }
    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/authenticate/register", request);
        return result.IsSuccessStatusCode;
    }
    public async Task<bool> ValidateAsync(string token)
    {
        var result = await _httpClient.GetAsync($"api/authenticate/validate?token={token}");
        return result.IsSuccessStatusCode;
    }
    public async Task<bool> SetTokenAsync(string token)
    {
        try
        {
            await _cookieService.SetAsync(new Cookie(COOKIE_TOKEN_NAME, token));
            return true;
        }
        catch(InvalidOperationException)
        {
            throw;
        }

    }

    public async Task<bool> RemoveTokenAsync()
    {
        try
        {
            await _cookieService.RemoveAsync(COOKIE_TOKEN_NAME);
            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

    }
    public async Task<string?> GetTokenAsync()
    {
        try
        {
            Cookie? cookie = await _cookieService.GetAsync(COOKIE_TOKEN_NAME);
            return cookie?.Value;
        }
        catch (InvalidOperationException)
        {
            throw;
        }

    }

}
