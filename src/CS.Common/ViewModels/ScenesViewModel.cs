using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts;
using CS.Contracts.Scenes;

namespace CS.Common.ViewModels;

public class ScenesViewModel : BaseViewModel
{
    private readonly SceneService _sceneService;

    private Scene _currentScene;
    private List<Scene> _scenes;
    private int _page = 1;
    private int _count = 25;

    public IAsyncRelayCommand GetScenesPage { get; private set; }

    public Scene CurrentScene {
        get => _currentScene;
        set {
            if (_currentScene != value) {
                _currentScene = value;
                OnPropertyChanged();
            }
        }
    }

    public List<Scene> Scenes {
        get => _scenes;
        set {
            if (_scenes != value) {
                _scenes = value;
                OnPropertyChanged();
            }
        }
    }

    public int Page {
        get => _page;
        set {
            if (_page != value) {
                _page = value;
                OnPropertyChanged();
            }
        }
    }

    public int Count {
        get => _count;
        set {
            if (_count != value) {
                _count = value;
                OnPropertyChanged();
            }
        }
    }

    public ScenesViewModel() {
        _sceneService = new SceneService();

        GetScenesPage = new AsyncRelayCommand(
            execute: () => _exceptionHandler.Handle(UpdatePageAsync));

        _scenes = new List<Scene> { new Scene { Id = -1, Name = "Не выбранно" } };
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _sceneService.GetScenePageAsync(new GetPageContext() {
            Page = _page,
            Count = _count
        });

        _scenes.AddRange(pageResult.Data.ToList());
        OnPropertyChanged(nameof(Scenes));
    }
}
