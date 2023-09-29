namespace CS.Contracts.Users;

public class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? PaternalName { get; set; }

    public long? SceneId { get; set; }

    public string SceneName { get; set; }

    public RoleType Role { get; set; }

    public string Login { get; set;} = string.Empty;
}
