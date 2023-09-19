using System;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnLoginClicked(object? sender, EventArgs e) => Shell.Current.GoToAsync("//commonPage", true);
}
