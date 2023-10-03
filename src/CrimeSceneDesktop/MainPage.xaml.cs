using System;
using CS.Common.Exceptions;
using CS.Common.Services;
using CS.Contracts.Sso;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class MainPage : ContentPage
{
    private readonly ExceptionHandler _exceptionHandler;
    private readonly SsoService _ssoService;

    public MainPage() {
        InitializeComponent();

        _exceptionHandler = new ExceptionHandler();
        _ssoService = new SsoService();
    }

    private async void OnLoginClicked(object? sender, EventArgs e) {
        await _exceptionHandler.Handle(async () => {
            await _ssoService.LoginAsync(new LoginContract {
                Login = loginEntry.Text,
                Password = passwordEntry.Text
            });

            await Shell.Current.GoToAsync("//commonPage", true);
        });
    }
}
