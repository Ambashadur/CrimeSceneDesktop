using CS.Common.ViewModels;
using CS.Contracts.Scenes;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace CrimeSceneDesktop.Pages;

public partial class UserPage : ContentPage, IQueryAttributable
{
    private UserViewModel _user;
    private ScenesViewModel _scenes;

    public UserPage() {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) {
        _user = query["user"] as UserViewModel;
        _scenes = query["scenes"] as ScenesViewModel;

        ScenePicker.BindingContext = _scenes;
        ScenePicker.SetBinding(Picker.ItemsSourceProperty, nameof(ScenesViewModel.Scenes));
        ScenePicker.SetBinding(Picker.SelectedItemProperty, nameof(ScenesViewModel.CurrentScene));
        ScenePicker.ItemDisplayBinding = new Binding(nameof(Scene.Name));

        UserView.BindingContext = _user;

        _scenes.CurrentScene = _scenes.Scenes.FirstOrDefault(scene => scene.Id == _user.SceneId) ?? _scenes.Scenes.First();
    }
}