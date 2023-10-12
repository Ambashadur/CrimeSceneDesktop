using CS.Contracts.Users;

namespace CS.Common.ViewModels;

public class UserViewModel : BaseViewModel
{
    private User _user = new();

    public long Id {
        get => _user.Id;
        set {
            if (_user.Id != value) {
                _user.Id = value;
                OnPropertyChanged();
            }
        }
    }

    public string FirstName {
        get => _user.FirstName;
        set {
            if (_user.FirstName != value) {
                _user.FirstName = value;
                OnPropertyChanged();
            }
        }
    }

    public string LastName {
        get => _user.LastName;
        set {
            if (_user.LastName != value) {
                _user.LastName = value;
                OnPropertyChanged();
            }
        }
    }

    public string PaternalName {
        get => _user.PaternalName;
        set {
            if (_user.PaternalName != value) {
                _user.PaternalName = value;
                OnPropertyChanged();
            }
        }
    }

    public long? SceneId {
        get => _user.SceneId;
        set {
            if (_user.SceneId != value) {
                _user.SceneId = value;
                OnPropertyChanged();
            }
        }
    }

    public string SceneName {
        get => _user.SceneName;
        set {
            if (_user.SceneName != value) {
                _user.SceneName = value;
                OnPropertyChanged();
            }
        }
    }

    public RoleType Role {
        get => _user.Role;
        set {
            if (_user.Role != value) {
                _user.Role = value;
                OnPropertyChanged();
            }
        }
    }

    public string Login {
        get => _user.Login;
        set {
            if (_user.Login != value) {
                _user.Login = value;
                OnPropertyChanged();
            }
        }
    }

    public UserViewModel() { }

    public UserViewModel(User user) {
        _user = user;
    }
}
