using CS.Common.Exceptions;
using CS.Common.Services;
using CS.Contracts.Sso;
using Microsoft.Maui.Controls;
using System;

namespace CrimeSceneDesktop;

public partial class MainPage : ContentPage
{
    private readonly ISsoService _ssoService;
    private readonly IExceptionHandler _exceptionHandler;

    public MainPage(ISsoService ssoService, IExceptionHandler exceptionHandler)
    {
        _ssoService = ssoService;
        _exceptionHandler = exceptionHandler;

        _exceptionHandler.DisplayException += DisplayAlert;

        InitializeComponent();
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
