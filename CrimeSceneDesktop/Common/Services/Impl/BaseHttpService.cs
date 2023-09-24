using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CrimeSceneDesktop.Common.Services.Impl;

public abstract class BaseHttpService
{
    protected readonly HttpClient _httpClient;
    protected readonly JsonSerializerOptions _options;

    protected BaseHttpService() {
        _httpClient = new HttpClient();
        _options = new();

        _options.Converters.Add(new JsonStringEnumConverter());
    }
}
