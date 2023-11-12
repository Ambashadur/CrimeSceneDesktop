using CS.Contracts.Comments;

namespace CS.Common.ViewModels;

public class CommentViewModel : BaseViewModel
{
    private readonly Comment _comment;

    public long Id {
        get => _comment.Id;
        set {
            if (_comment.Id != value) {
                _comment.Id = value;
                OnPropertyChanged();
            }
        }
    }

    public string Scene {
        get => _comment.Scene;
        set {
            if (_comment.Scene != value) {
                _comment.Scene = value;
                OnPropertyChanged();
            }
        }
    }

    public string Text {
        get => _comment.Text;
        set {
            if (_comment.Text != value) {
                _comment.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public string AudioLink {
        get => _comment.AudioLink;
        set {
            if (_comment.AudioLink != value) {
                _comment.AudioLink = value;
                OnPropertyChanged();
            }
        }
    }

    public string PhotoLink {
        get => _comment.PhotoLink;
        set {
            if (_comment.PhotoLink != value) {
                _comment.PhotoLink = value;
                OnPropertyChanged();
            }
        }
    }

    public CommentViewModel() { }

    public CommentViewModel(Comment comment) : this() {
        _comment = comment;
    }
}
