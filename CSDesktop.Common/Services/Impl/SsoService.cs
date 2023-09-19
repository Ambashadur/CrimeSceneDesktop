using System.Net.Http.Json;
using CSDesktop.Contracts.Sso;

namespace CSDesktop.Common.Services.Impl;

public class SsoService : BaseHttpService, ISsoService
{
    public string JWTToken => _token;

    private string _token;

    private const string SSO_LOGIN = "api/sso/login";
    private const string SSO_LOGOUT = "api/sso/logout";

    public async Task LoginAsync(LoginContract loginContract) {
        var request = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri(SSO_LOGIN),
            Content = JsonContent.Create(loginContract, options: _options),
        };

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        _token = await response.Content.ReadAsStringAsync();
    }

    public async Task LogoutAsync() {
        var request = new HttpRequestMessage() {
            Method= HttpMethod.Post,
            RequestUri= new Uri(SSO_LOGOUT)
        };

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }
}
