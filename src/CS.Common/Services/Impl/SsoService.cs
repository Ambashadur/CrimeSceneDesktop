using System.Net.Http.Json;
using CS.Contracts.Sso;
using CS.Contracts.Users;

namespace CS.Common.Services.Impl;

public class SsoService : BaseHttpService, ISsoService
{
    public string JWTToken => _token;

    private string _token;

    private const string SSO_LOGIN = "api/sso/login";
    private const string SSO_LOGOUT = "api/sso/logout";
    private const string SSO_REGISTER = "api/sso/register";

    public async Task LoginAsync(LoginContract loginContract) {
        var request = new HttpRequestMessage(HttpMethod.Post, SSO_LOGIN) {
            Content = JsonContent.Create(loginContract, options: _options),
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        _token = await response.Content.ReadAsStringAsync();
    }

    public async Task LogoutAsync() {
        var request = new HttpRequestMessage(HttpMethod.Post, SSO_LOGOUT);

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<User> RegisterUserAsync(RegisterUser registerUser) {
        var request = new HttpRequestMessage(HttpMethod.Post, SSO_REGISTER) {
            Content = JsonContent.Create(registerUser)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<User>();
    }
}
