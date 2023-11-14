using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;
using CS.Contracts.Users;

namespace CS.Common.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private readonly UserService _userService;

    private UserViewModel _user;
    private IEnumerable<UserViewModel> _users;
    private int _page = 1;
    private int _count = 100;

    public IAsyncRelayCommand UpdatePageCommand { get; private set; }

    public int TotalCount { get; private set; }

    public UserViewModel CurrentUser {
        set => SetProperty(ref _user, value);
        get => _user;
    }

    public IEnumerable<UserViewModel> Users {
        set => SetProperty(ref _users, value);
        get => _users;
    }

    public int Page {
        set => SetProperty(ref _page, value);
        get => _page;
    }

    public int Count {
        set => SetProperty(ref _count, value);
        get => _count;
    }

    public UsersViewModel() {
        _userService = new UserService();

        UpdatePageCommand = new AsyncRelayCommand(
            execute: () => _exceptionHandler.Handle(UpdatePageAsync));
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _userService.GetUsersAsync(new() {
            Page = _page,
            Count = _count,
            Role = RoleType.Default
        });

        TotalCount = pageResult.TotalCount;
        Users = pageResult.Data.Select(user => new UserViewModel(user)).ToList();
    }
}
