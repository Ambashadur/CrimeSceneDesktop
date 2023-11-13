using System.Collections.Generic;
using CS.Common.ViewModels;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop.Pages;

public partial class CommentsPage : ContentPage, IQueryAttributable
{
    private readonly CommentsViewModel _comments;

    private UserViewModel _user;

    public CommentsPage() {
        InitializeComponent();

        _comments = new CommentsViewModel();
        BindingContext = _comments;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) {
        _user = query["user"] as UserViewModel;

        _comments.UserId = _user.Id;
        _comments.UpdatePageCommand.Execute(null);
    }
}