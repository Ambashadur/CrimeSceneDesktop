﻿using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts.Scenes;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CS.Common.ViewModels;

public class SceneViewModel : BaseViewModel
{
    private readonly SceneService _sceneService;

    private Scene _scene = new();
    private FileResult _file;

    public IAsyncRelayCommand CreateSceneCommand { get; private set; }
    public IAsyncRelayCommand PickFileCommand { get; private set; }

    public long Id {
        get => _scene.Id;
        set {
            if (_scene.Id != value) {
                _scene.Id = value;
                OnPropertyChanged();
            }
        }
    }

    public string Name {
        get => _scene.Name;
        set {
            if (_scene.Name != value) {
                _scene.Name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Link {
        get => _scene.Link;
        set {
            if (_scene.Link != value) {
                _scene.Link = value;
                OnPropertyChanged();
            }
        }
    }

    public FileResult File {
        get => _file;
        set {
            if (_file != value) {
                _file = value;
                OnPropertyChanged();
            }
        }
    }

    public SceneViewModel() {
        _sceneService = new SceneService();

        CreateSceneCommand = new AsyncRelayCommand(
            execute: () => _exceptionHandler.Handle(CreateScene));

        PickFileCommand = new AsyncRelayCommand(
            execute: async () => File = await FilePicker.Default.PickAsync(PickOptions.Images));
    }

    private async Task CreateScene() {
        if (string.IsNullOrEmpty(_scene.Name)) throw new ArgumentException("Имя не должно быть пустым!");
        if (_file is null) throw new ArgumentException("Файл сцены не выбран!");

        using var fileStream = await _file.OpenReadAsync();
        var scene = await _sceneService.CreateSceneAsync(_scene.Name, fileStream, _file.ContentType, _file.FileName);

        await Application.Current.MainPage.DisplayAlert("Уведомление", "Сцена успешно создана!", "OK");

        Name = null;
        File = null;
    }
}