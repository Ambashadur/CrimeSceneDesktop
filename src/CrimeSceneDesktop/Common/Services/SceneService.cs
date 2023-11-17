using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CS.Contracts;
using CS.Contracts.Scenes;
using Microsoft.AspNetCore.WebUtilities;

namespace CS.Common.Services;

public class SceneService
{
    private const string CREATE_SCENE = "api/scenes";
    private const string SCENE_PAGE = "api/scenes/page";

    public async Task<Scene> CreateSceneAsync(string name, Stream filestream, string contentType, string filename) {
        using var multipartFormContent = new MultipartFormDataContent();
        multipartFormContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

        var streamContent = new StreamContent(filestream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        multipartFormContent.Add(streamContent, "formFile", filename);
        multipartFormContent.Add(new StringContent(name), "name");

        var request = new HttpRequestMessage(HttpMethod.Post, CREATE_SCENE) {
            Content = multipartFormContent
        };

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Scene>(options: CSDHttpClient.JsonOptions);
    }

    public async Task<PageResult<Scene>> GetScenePageAsync(GetPageContext context) {
        var query = new Dictionary<string, string>() {
            [nameof(GetPageContext.Page)] = context.Page.ToString(),
            [nameof(GetPageContext.Count)] = context.Count.ToString()
        };

        var uri = QueryHelpers.AddQueryString(SCENE_PAGE, query);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<Scene>>(options: CSDHttpClient.JsonOptions);
    }
}
