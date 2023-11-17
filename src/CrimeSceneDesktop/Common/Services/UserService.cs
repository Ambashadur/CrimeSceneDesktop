using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CS.Contracts;
using CS.Contracts.Users;
using Microsoft.AspNetCore.WebUtilities;

namespace CS.Common.Services;

public class UserService
{
    private const string USERS_PAGE = "api/users/page";
    private const string USERS_SCENE = "api/users/{0}/scene";

    public async Task<PageResult<User>> GetUsersAsync(GetUsersPageContext context) {
        var query = new Dictionary<string, string>() {
            [nameof(GetUsersPageContext.Page)] = context.Page.ToString(),
            [nameof(GetUsersPageContext.Count)] = context.Count.ToString(),
            [nameof(GetUsersPageContext.Role)] = context.Role.ToString(),
        };

        var uri = QueryHelpers.AddQueryString(USERS_PAGE, query);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<User>>(options: CSDHttpClient.JsonOptions);
    }

    public async Task SetUserSceneAsync(long userId, long? sceneId) {
        var request = new HttpRequestMessage(HttpMethod.Post, string.Format(USERS_SCENE, userId)) {
            Content = JsonContent.Create(new { sceneId }, options: CSDHttpClient.JsonOptions)
        };

        using var response = await CSDHttpClient.Client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
