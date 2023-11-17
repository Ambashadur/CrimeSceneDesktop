using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts;

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
        set => SetProperty(ref _currentScene, value);
    }

    public List<SceneViewModel> Scenes {
        get => _scenes;
        set => SetProperty(ref _scenes, value);
    }

    public int Page {
        get => _page;
        set => SetProperty(ref _page, value);
    }

    public int Count {
        get => _count;
        set => SetProperty(ref _count, value);
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

        _scenes = new List<SceneViewModel> { new SceneViewModel { Id = -1, Name = "Не выбранно", Link = "not_select.jpg" } };
        _scenes.AddRange(pageResult.Data.Select(scene => new SceneViewModel {
            Id = scene.Id,
            Name = scene.Name,
            Link = CSDHttpClient.GetLink(scene.Link)
        }).ToList());
        OnPropertyChanged(nameof(Scenes));
    }
}
