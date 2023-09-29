using CS.Contracts.Users;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrimeSceneDesktop;

public partial class CommonPage : ContentPage
{
    private IEnumerable<User> _persons = Enumerable.Empty<User>();

    public CommonPage() {
        InitializeComponent();
    }

    private IEnumerable<User> GetPersonRecords() {
        _persons = new List<User>();

        return _persons;
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