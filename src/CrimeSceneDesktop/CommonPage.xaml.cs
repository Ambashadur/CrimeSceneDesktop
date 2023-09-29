using CS.Common.Services;
using CS.Contracts;
using CS.Contracts.Users;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrimeSceneDesktop;

public partial class CommonPage : ContentPage
{
    private readonly IUserService _userService;

    private PageResult<User> _usersPage = new();

    public CommonPage(IUserService userService) {
        _userService = userService;

        InitializeComponent();
        GetUsersAsync();
    }

    private async void GetUsersAsync() {
        _usersPage = await _userService.GetUsersAsync(new GetUsersPageContext() {
            Page = 1,
            Count = 10,
            Role = RoleType.Default
        });

        UsersView.ItemsSource = _usersPage.Data;
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
}