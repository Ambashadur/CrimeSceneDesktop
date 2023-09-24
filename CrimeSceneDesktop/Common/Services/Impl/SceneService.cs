using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts;
using CrimeSceneDesktop.Contracts.Scenes;

namespace CrimeSceneDesktop.Common.Services.Impl;

public class SceneService : BaseHttpService, ISceneService
{
    private const string CREATE_SCENE = "api/scenes";
    private const string SCENE_PAGE = "api/scenes/page";

    public async Task<Scene?> CreateSceneAsync(string name, Stream filestream, string contentType, string filename) {
        using var multipartFormContent = new MultipartFormDataContent();
        multipartFormContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

        var streamContent = new StreamContent(filestream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        multipartFormContent.Add(streamContent, "formFile", filename);
        multipartFormContent.Add(new StringContent(name), "name");

        var request = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri(CREATE_SCENE),
            Content = multipartFormContent
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Scene>(options: _options);
    }

    public async Task<PageResult<Scene>> GetScenePageAsync(GetPageContext context) {
        var request = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri(SCENE_PAGE),
            Content = JsonContent.Create(context, options: _options)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<Scene>>(options: _options);
    }
}
