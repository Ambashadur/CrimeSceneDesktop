using System;
using System.Collections.Generic;
using CS.Common.Exceptions;
using CS.Common.Services;
using CS.Common.ViewModels;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop.Pages;

public partial class CommonPage : ContentPage
{
    private readonly SsoService _ssoService;
    private readonly ExceptionHandler _exceptionHandler;
    private readonly UsersViewModel _usersViewModel;
    private readonly ScenesViewModel _scenesViewModel;

    public CommonPage() {
        InitializeComponent();

        _ssoService = new SsoService();
        _exceptionHandler = new ExceptionHandler();
        _usersViewModel = new UsersViewModel();
        _scenesViewModel = new ScenesViewModel();

        BindingContext = _usersViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) {
        base.OnNavigatedTo(args);

        _usersViewModel.UpdatePageCommand.Execute(null);
        _usersViewModel.CurrentUser = null;
    }

    private async void Logout(object sender, EventArgs e) {
        await _exceptionHandler.Handle(async () => {
            await _ssoService.LogoutAsync();
            await Shell.Current.GoToAsync("mainPage", true);
        });
    }

    private async void GoToCreateScenePage(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("createScenePage", true);
    }

    private async void GoToUserPage(object sender, EventArgs e) {
        if (_usersViewModel.CurrentUser is null) {
            await DisplayAlert("Ошибка", "Вы не выбрали пользователя!", "Ок");
            return;
        }

        await _scenesViewModel.GetScenesPage.ExecuteAsync(null);

        await Shell.Current.GoToAsync("userPage", true, new Dictionary<string, object>() {
            ["user"] = _usersViewModel.CurrentUser,
            ["scenes"] = _scenesViewModel
        });
    }

    private async void GoToCommentsPage(object sender, EventArgs e) {
        if (_usersViewModel.CurrentUser is null) {
            await DisplayAlert("Ошибка", "Вы не выбрали пользователя!", "Ок");
            return;
        }

        await Shell.Current.GoToAsync("commentsPage", true, new Dictionary<string, object>() {
            ["user"] = _usersViewModel.CurrentUser,
        });
    }
}