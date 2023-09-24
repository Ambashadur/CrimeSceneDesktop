using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts.Sso;
using CrimeSceneDesktop.Contracts.Users;

namespace CrimeSceneDesktop.Common.Services.Impl;

public class SsoService : BaseHttpService, ISsoService
{
    public string JWTToken => _token;

    private string _token;

    private const string SSO_LOGIN = "api/sso/login";
    private const string SSO_LOGOUT = "api/sso/logout";
    private const string SSO_REGISTER = "api/sso/register";

    public async Task LoginAsync(LoginContract loginContract) {
        var request = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri(SSO_LOGIN),
            Content = JsonContent.Create(loginContract, options: _options),
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        _token = await response.Content.ReadAsStringAsync();
    }

    public async Task LogoutAsync() {
        var request = new HttpRequestMessage() {
            Method= HttpMethod.Post,
            RequestUri= new Uri(SSO_LOGOUT)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<User> RegisterUserAsync(RegisterUser registerUser) {
        var request = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri(SSO_REGISTER),
            Content = JsonContent.Create(registerUser)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
