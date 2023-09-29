using System.Text.Json;
using System.Text.Json.Serialization;

namespace CS.Common.Services.Impl;

public abstract class BaseHttpService
{
    protected readonly HttpClient _httpClient;
    protected readonly JsonSerializerOptions _options;

    protected BaseHttpService() {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5197")
        };

        _options = new();
        _options.Converters.Add(new JsonStringEnumConverter());
    }
}
