using System.Collections.Generic;
using System.Linq;
using CS.Common.ViewModels;
using CS.Contracts.Scenes;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class UserPage : ContentPage, IQueryAttributable
{
    private UserViewModel _user;
    private readonly ScenesViewModel _scenes;

    public UserPage() {
        InitializeComponent();

        _scenes = new ScenesViewModel();
        _scenes.GetScenesPage.Execute(null);
        
        ScenePicker.BindingContext = _scenes;
        ScenePicker.SetBinding(Picker.ItemsSourceProperty, nameof(ScenesViewModel.Scenes));
        ScenePicker.SetBinding(Picker.SelectedItemProperty, nameof(ScenesViewModel.CurrentScene));
        ScenePicker.ItemDisplayBinding = new Binding(nameof(Scene.Name));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) {
        _user = query["user"] as UserViewModel;

        UserView.BindingContext = _user;
    }
}