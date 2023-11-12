using CommunityToolkit.Mvvm.Input;
using CS.Common.Services;

namespace CS.Common.ViewModels;

public class CommentsViewModel : BaseViewModel
{
    private readonly CommentService _commentService;

    private CommentViewModel _comment;
    private IEnumerable<CommentViewModel> _comments;
    private long _userId;
    private int _page = 1;
    private int _count = 25;

    public IAsyncRelayCommand UpdatePageCommand { get; private set; }

    public int TotalCount { get; private set; }

    public CommentViewModel CurrentComment {
        get => _comment;
        set => SetProperty(ref _comment, value);
    }

    public IEnumerable<CommentViewModel> Comments {
        get => _comments;
        set => SetProperty(ref _comments, value);
    }

    public int Page {
        get => _page;
        set => SetProperty(ref _page, value);
    }

    public int Count {
        get => _count;
        set => SetProperty(ref _count, value);
    }

    public long UserId {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    public CommentsViewModel() {
        _commentService = new CommentService();

        UpdatePageCommand = new AsyncRelayCommand(
            execute: () => _exceptionHandler.Handle(UpdatePageAsync));
    }

    private async Task UpdatePageAsync() {
        var pageResult = await _commentService.GetCommentsAsync(new() {
            Page = _page,
            Count = _count,
            UserId = _userId
        });

        TotalCount = pageResult.TotalCount;
        Comments = pageResult.Data.Select(comment => new CommentViewModel(comment));
    }
}
