﻿using System.Windows.Input;
using CS.Common.Services;
using CS.Contracts;
using CS.Contracts.Scenes;

namespace CS.Common.ViewModels;

public class ScenesViewModel : BaseViewModel
{
    private readonly SceneService _sceneService;

    private Scene _currentScene;
    private IEnumerable<Scene> _scenes;
    private int _page = 1;
    private int _count = 25;

    public ICommand GetScenesPage { get; private set; }
    public ICommand SetSceneByNameCommand { get; private set; }

    public Scene CurrentScene {
        get => _currentScene;
        set {
            if (_currentScene != value) {
                _currentScene = value;
                OnPropertyChanged();
            }
        }
    }

    public IEnumerable<Scene> Scenes {
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

        GetScenesPage = new Command(
            execute: async () => await _exceptionHandler.Handle(UpdatePageAsync));

        SetSceneByNameCommand = new Command(
            execute: (sceneName) => CurrentScene = _scenes.First(x => x.Name == sceneName.ToString()));
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _sceneService.GetScenePageAsync(new GetPageContext() {
            Page = _page,
            Count = _count
        });

        Scenes = pageResult.Data;
    }
}