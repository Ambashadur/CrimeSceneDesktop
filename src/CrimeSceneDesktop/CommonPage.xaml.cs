using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CS.Common.Exceptions;
using CS.Common.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CrimeSceneDesktop;

public partial class CommonPage : ContentPage
{
    private readonly SsoService _ssoService;
    private readonly ExceptionHandler _exceptionHandler;

    public CommonPage() {
        InitializeComponent();

        _ssoService = new SsoService();
        _exceptionHandler = new ExceptionHandler();
    }

    private async void Logout(object sender, EventArgs e) {
        await _exceptionHandler.Handle(async () => {
            await _ssoService.LogoutAsync();
            await Shell.Current.GoToAsync("//mainPage", true);
        });
    }

    private async void CreateScene(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("//createScenePage", true);
    }
}