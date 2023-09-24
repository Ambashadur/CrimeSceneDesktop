using CrimeSceneDesktop.Contracts.Users;

namespace CrimeSceneDesktop.Contracts.Sso;

public class RegisterUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? PaternalName { get; set; }

    public RoleType Role { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
