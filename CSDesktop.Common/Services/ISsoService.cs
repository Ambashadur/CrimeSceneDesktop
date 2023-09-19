using CSDesktop.Contracts.Sso;

namespace CSDesktop.Common.Services;

public interface ISsoService
{
    string JWTToken { get; }

    Task LoginAsync(LoginContract loginContract);

    Task LogoutAsync();
}
