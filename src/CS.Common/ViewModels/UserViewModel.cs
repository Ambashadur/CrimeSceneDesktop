using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts.Users;

namespace CS.Common.ViewModels;

public class UserViewModel : BaseViewModel
{
    private readonly UserService _userService;
    private readonly User _user = new();

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
                _user.SceneName = string.IsNullOrEmpty(value) ? "-" : value;
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

    public IAsyncRelayCommand UpdateUserCommand { get; set; }

    public UserViewModel() {
        _userService = new UserService();

        UpdateUserCommand = new AsyncRelayCommand<long?>(
            execute: (sceneId) => _exceptionHandler.Handle(() => SetUserScene(sceneId)));
    }

    public UserViewModel(User user) : this() {
        _user = user;

        if (string.IsNullOrEmpty(_user.SceneName)) {
            _user.SceneName = "-";
        }
    }

    private async Task SetUserScene(long? sceneId) {
        if (sceneId.HasValue) {
            SceneId = sceneId.Value == -1 ? null : sceneId.Value;

            await _userService.SetUserSceneAsync(_user.Id, SceneId);
            await Application.Current.MainPage.DisplayAlert("Уведомление", "Запись успешно обнавлена", "ОК");
        }
    }
}
