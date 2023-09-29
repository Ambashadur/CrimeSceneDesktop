namespace CS.Common.Exceptions;

public interface IExceptionHandler
{
    public event Func<string, string, string, Task> DisplayException;

    Task Handle(Func<Task> func);
}
