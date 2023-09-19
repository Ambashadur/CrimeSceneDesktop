using CSDesktop.Contracts.Users;

namespace CSDesktop.Common.Services.Impl;

public class UserService : BaseHttpService, IUserService
{
    private const string USERS_PAGE = "api/user/page";

    public Task<IEnumerable<User>> GetUsersAsync() {
        var request = new HttpRequestMessage() {
            Method = HttpMethod.Get,
        };
    }
}
