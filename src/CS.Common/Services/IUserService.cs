using CS.Contracts;
using CS.Contracts.Users;

namespace CS.Common.Services;

public interface IUserService
{
    Task<PageResult<User>> GetUsersAsync(GetUsersPageContext context);
}
