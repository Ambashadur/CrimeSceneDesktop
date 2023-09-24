using System;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnLoginClicked(object? sender, EventArgs e) {
        //await _ssoService.LoginAsync(new LoginContract() {
        //    Login = loginEntry.Text,
        //    Password = passwordEntry.Text
        //});

        Shell.Current.GoToAsync("//commonPage", true);
    }
}
