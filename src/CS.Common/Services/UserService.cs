using CS.Contracts;
using CS.Contracts.Users;
using System.Net.Http.Json;

namespace CS.Common.Services;

public class UserService : BaseHttpService
{
    private const string USERS_PAGE = "api/users/page";

    public async Task<PageResult<User>> GetUsersAsync(GetUsersPageContext context) {
        var request = new HttpRequestMessage(HttpMethod.Post, USERS_PAGE) {
            Content = JsonContent.Create(context, options: _options)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<User>>(options: _options);
    }
}
