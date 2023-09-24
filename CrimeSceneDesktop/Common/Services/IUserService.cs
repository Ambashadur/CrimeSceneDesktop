using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts;
using CrimeSceneDesktop.Contracts.Users;

namespace CrimeSceneDesktop.Common.Services;

public interface IUserService
{
    Task<PageResult<User>> GetUsersAsync(GetUsersPageContext context);
}
