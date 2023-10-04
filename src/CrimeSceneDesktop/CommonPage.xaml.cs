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

    private async void SetScene(object sender, EventArgs args) {
        var fileResult = await FilePicker.Default.PickAsync(PickOptions.Images);

        var httpClient = new HttpClient();

        using var multipartFormContent = new MultipartFormDataContent();
        multipartFormContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

        var streamContent = new StreamContent(await fileResult.OpenReadAsync());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(fileResult.ContentType);

        multipartFormContent.Add(streamContent, "formFile", fileResult.FileName);

        var message = new HttpRequestMessage() {
            Method = HttpMethod.Post,
            RequestUri = new Uri("http://localhost:5197/api/scene/1"),
            Content = multipartFormContent
        };

        using var response = await httpClient.SendAsync(message);

        response.EnsureSuccessStatusCode();
    }

    private async void Logout(object sender, EventArgs e) {
        await _exceptionHandler.Handle(async () => {
            await _ssoService.LogoutAsync();
            await Shell.Current.GoToAsync("//mainPage", true);
        });
    }
}