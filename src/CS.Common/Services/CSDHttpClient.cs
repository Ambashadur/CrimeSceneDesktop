using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CS.Common.Services;

public static class CSDHttpClient
{
    private static HttpClient _httpClient;
    private static Uri _baseAddress = new Uri("http://localhost:5197");
    private static string _jwtToken;
    private static JsonSerializerOptions _options;

    public static HttpClient Client {
        get {
            if (_httpClient is not null) return _httpClient;

            _httpClient = new HttpClient() {
                BaseAddress = _baseAddress
            };

            _options = new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _options.Converters.Add(new JsonStringEnumConverter());

            return _httpClient;
        }
    }

    public static string JwtToken {
        set {
            _jwtToken = value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }
    }

    public static JsonSerializerOptions JsonOptions {
        get => _options;
    }
}
