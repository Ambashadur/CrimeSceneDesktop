using System.Text;

namespace CS.Common.Exceptions.Impl;

public class ExceptionHandler : IExceptionHandler
{
    public event Func<string, string, string, Task> DisplayException {
        add => _diplayException += value;
        remove => _diplayException -= value;
    }

    private event Func<string, string, string, Task> _diplayException;

    private readonly StringBuilder _sb;

    public ExceptionHandler() {
        _sb = new StringBuilder();
    }

    public async Task Handle(Func<Task> func) {
        try {
            await func.Invoke();
        } catch (HttpRequestException rex) {
            _sb.AppendLine(rex.Message);

            if (rex.StatusCode.HasValue) {
                _sb.Append("Статус код: ");
                _sb.Append(rex.StatusCode);
            }

            await _diplayException?.Invoke("Сетевая ошибка", _sb.ToString(), "ОК");
            _sb.Clear();
        } catch (Exception ex) {
            await _diplayException?.Invoke("Ошибка", ex.Message, "ОК");
        }
    }
}
