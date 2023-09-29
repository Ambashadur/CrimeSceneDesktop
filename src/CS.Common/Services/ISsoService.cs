using CS.Contracts.Sso;
using CS.Contracts.Users;

namespace CS.Common.Services;

public interface ISsoService
{
    string JWTToken { get; }

    Task LoginAsync(LoginContract loginContract);

    Task LogoutAsync();

    Task<User> RegisterUserAsync(RegisterUser registerUser);
}
