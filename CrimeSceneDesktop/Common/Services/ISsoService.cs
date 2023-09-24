using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts.Sso;
using CrimeSceneDesktop.Contracts.Users;

namespace CrimeSceneDesktop.Common.Services;

public interface ISsoService
{
    string JWTToken { get; }

    Task LoginAsync(LoginContract loginContract);

    Task LogoutAsync();

    Task<User> RegisterUserAsync(RegisterUser registerUser);
}
