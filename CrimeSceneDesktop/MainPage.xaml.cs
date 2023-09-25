using System;
using CrimeSceneDesktop.Common.Services;
using CrimeSceneDesktop.Contracts.Sso;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class MainPage : ContentPage
{
    private readonly ISsoService _ssoService;

    public MainPage(ISsoService ssoService)
    {
        _ssoService = ssoService;

        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e) {
        try {
            await _ssoService.LoginAsync(new LoginContract {
                Login = loginEntry.Text,
                Password = passwordEntry.Text
            });

            await Shell.Current.GoToAsync("//commonPage", true);
        } catch (Exception ex) {
            await DisplayAlert("Ошибка", "Ошибка при работе с сетью", "ОК");
        }
    }
}
