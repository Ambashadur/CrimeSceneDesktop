using System;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class CreateScenePage : ContentPage
{
    public CreateScenePage() {
        InitializeComponent();
    }

    private async void GoToCommonPage(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("//commonPage", true);
    }
}