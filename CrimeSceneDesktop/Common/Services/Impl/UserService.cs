using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts;
using CrimeSceneDesktop.Contracts.Users;

namespace CrimeSceneDesktop.Common.Services.Impl;

public class UserService : BaseHttpService, IUserService
{
    private const string USERS_PAGE = "api/user/page";

    public async Task<PageResult<User>> GetUsersAsync(GetUsersPageContext context) {
        var request = new HttpRequestMessage(HttpMethod.Post, USERS_PAGE) {
            Content = JsonContent.Create(context, options: _options)
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PageResult<User>>(options: _options);
    }
}
