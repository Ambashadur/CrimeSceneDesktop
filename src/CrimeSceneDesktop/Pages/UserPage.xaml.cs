using System;
using System.Collections.Generic;
using System.Linq;
using CS.Common.ViewModels;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop.Pages;

public partial class UserPage : ContentPage, IQueryAttributable
{
    private UserViewModel _user;
    private ScenesViewModel _scenes;

    public UserPage() {
        InitializeComponent();

        _scenes = new ScenesViewModel();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query) {
        _user = query["user"] as UserViewModel;
        await _scenes.GetScenesPage.ExecuteAsync(null);

        UserView.BindingContext = _user;

        _scenes.CurrentScene = _scenes.Scenes.FirstOrDefault(scene => scene.Id == _user.SceneId) ?? _scenes.Scenes.First();

        SceneCollection.BindingContext = _scenes;
        SceneCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(ScenesViewModel.Scenes));
        SceneCollection.SetBinding(CollectionView.SelectedItemProperty, nameof(ScenesViewModel.CurrentScene));

        SceneLabel.SetBinding(Entry.TextProperty, new Binding { Source = _scenes, Path = "CurrentScene.Name" });
        SaveButton.SetBinding(Button.CommandParameterProperty, new Binding { Source = _scenes, Path = "CurrentScene.Id" });
    }

    private async void OnBackButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("..", true);
}