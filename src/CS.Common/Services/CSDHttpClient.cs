using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CS.Common.Services;

public static class CSDHttpClient
{
    private const string PATH_TEMPLATE = "{0}/{1}";
    private const string BASE_PATH = "http://localhost:5197";

    private readonly static Uri _baseAddress = new(BASE_PATH);
    private static HttpClient _httpClient;
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

    public static string GetLink(string relativePath) => string.Format(PATH_TEMPLATE, BASE_PATH, relativePath);
}
