using CS.Contracts.Users;

namespace CS.Common.ViewModels;
public class UserViewModel : BaseViewModel
{
    private long _id;
    private string _firstName;
    private string _lastName;
    private string _paternalName;
    private long? _sceneId;
    private string _sceneName;
    private RoleType _role;
    private string _login;

    public long Id {
        set => SetProperty(ref _id, value);
        get => _id;
    }

    public string FirstName {
        set => SetProperty(ref _firstName, value);
        get => _firstName;
    }

    public string LastName {
        set => SetProperty(ref _lastName, value);
        get => _lastName;
    }

    public string PaternalName {
        set => SetProperty(ref _paternalName, value);
        get => _paternalName;
    }

    public long? SceneId {
        set => SetProperty(ref _sceneId, value);
        get => _sceneId;
    }

    public string SceneName {
        set => SetProperty(ref _sceneName, value);
        get => _sceneName;
    }

    public RoleType Role {
        set => SetProperty(ref _role, value);
        get => _role;
    }

    public string Login {
        set => SetProperty(ref _login, value);
        get => _login;
    }

    public UserViewModel() { }

    public UserViewModel(User user) {
        _id = user.Id;
        _firstName = user.FirstName;
        _lastName = user.LastName;
        _paternalName = user.PaternalName;
        _sceneId = user.SceneId;
        _sceneName = user.SceneName;
        _role = user.Role;
        _login = user.Login;
    }
}
