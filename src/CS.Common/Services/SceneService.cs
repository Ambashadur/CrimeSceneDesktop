using CS.Contracts;
using CS.Contracts.Scenes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
        var request = new HttpRequestMessage(HttpMethod.Post, SCENE_PAGE) {
            Content = JsonContent.Create(context, options: CSDHttpClient.JsonOptions)
        };

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<Scene>>(options: CSDHttpClient.JsonOptions);
    }
}
