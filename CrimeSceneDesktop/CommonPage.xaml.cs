using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CrimeSceneDesktop.Contracts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CrimeSceneDesktop;

public partial class CommonPage : ContentPage
{
    private IEnumerable<Person> _persons = Enumerable.Empty<Person>();

    public CommonPage() {
        InitializeComponent();

        MainCollectionView.ItemsSource = GetPersonRecords();
        LogoutBtn.Clicked += (obj, args) => Shell.Current.GoToAsync("//mainPage");
        SetSceneBtn.Clicked += SetScene;
    }

    private IEnumerable<Person> GetPersonRecords() {
        _persons = new List<Person>() {
            new() {
                Id = Random.Shared.Next(0, 100),
                FullName = "Ivan Ivanov Ivanovich",
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now,
                Count= Random.Shared.Next(0, 10)
            }
        };

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