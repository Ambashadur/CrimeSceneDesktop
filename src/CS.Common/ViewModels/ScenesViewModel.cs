using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts;
using CS.Contracts.Scenes;

namespace CS.Common.ViewModels;

public class ScenesViewModel : BaseViewModel
{
    private readonly SceneService _sceneService;

    private SceneViewModel _currentScene;
    private List<SceneViewModel> _scenes;
    private int _page = 1;
    private int _count = 25;

    public IAsyncRelayCommand GetScenesPage { get; private set; }

    public SceneViewModel CurrentScene {
        get => _currentScene;
        set {
            if (_currentScene != value) {
                _currentScene = value;
                OnPropertyChanged();
            }
        }
    }

    public List<SceneViewModel> Scenes {
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
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _sceneService.GetScenePageAsync(new GetPageContext() {
            Page = _page,
            Count = _count
        });

        _scenes = new List<SceneViewModel> { new SceneViewModel { Id = -1, Name = "Не выбранно" } };
        _scenes.AddRange(pageResult.Data.Select(scene => new SceneViewModel { Id = scene.Id, Name = scene.Name }).ToList());
        OnPropertyChanged(nameof(Scenes));
    }
}
