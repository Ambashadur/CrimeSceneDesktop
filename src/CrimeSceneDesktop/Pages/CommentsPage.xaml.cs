using System;
using System.Collections.Generic;
using System.Linq;
using CS.Common.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace CrimeSceneDesktop.Pages;

public partial class CommentsPage : ContentPage, IQueryAttributable
{
    private readonly CommentsViewModel _commentsViewModel;

    private UserViewModel _user;

    public CommentsPage() {
        InitializeComponent();

        _commentsViewModel = new CommentsViewModel();
        BindingContext = _commentsViewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query) {
        _user = query["user"] as UserViewModel;

        _commentsViewModel.UserId = _user.Id;
        await _commentsViewModel.UpdatePageCommand.ExecuteAsync(null);

        if (_commentsViewModel.Comments.Any()) {
            _commentsViewModel.CurrentComment = _commentsViewModel.Comments.First();
        } else {
            var layout = new VerticalStackLayout() {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            layout.Add(new Border() {
                Stroke = new SolidColorBrush(Color.FromArgb("212529")),
                StrokeShape = new RoundRectangle() { CornerRadius = 8 },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 320,
                Content = new Label() {
                    Text = "Список комментариев пуст!",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                }
            });

            Content = layout;
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("..", true);
}