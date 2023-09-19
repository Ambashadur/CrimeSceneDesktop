using CSDesktop.Contracts.Users;

namespace CSDesktop.Common.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
}
