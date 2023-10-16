using System.Windows.Input;
using CS.Common.Services;
using CS.Contracts.Users;

namespace CS.Common.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private readonly UserService _userService;

    private UserViewModel _user;
    private IEnumerable<UserViewModel> _users;
    private int _page = 1;
    private int _count = 25;

    public ICommand UpdatePageCommand { get; private set; }

    public UserViewModel CurrentScene {
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

        UpdatePageCommand = new Command(
            execute: async () => await _exceptionHandler.Handle(UpdatePageAsync));

        UpdatePageCommand.Execute(null);
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _userService.GetUsersAsync(new GetUsersPageContext() {
            Page = _page,
            Count = _count,
            Role = RoleType.Default
        });

        Users = pageResult.Data.Select(user => new UserViewModel(user));
    }
}
