using System.ComponentModel;
using System.Runtime.CompilerServices;
using CS.Common.Exceptions;

namespace CS.Common.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected ExceptionHandler _exceptionHandler = new();

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
        if (Equals(storage, value)) return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
