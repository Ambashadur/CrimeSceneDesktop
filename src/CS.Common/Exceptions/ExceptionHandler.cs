using System.Text;

namespace CS.Common.Exceptions;

public class ExceptionHandler
{
    private readonly StringBuilder _sb;
    private readonly Page _mainPage;

    public ExceptionHandler() {
        _sb = new StringBuilder();
        _mainPage = Application.Current.MainPage;
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

            await _mainPage.DisplayAlert("Сетевая ошибка", _sb.ToString(), "ОК");
            _sb.Clear();
        } catch (Exception ex) {
            await _mainPage.DisplayAlert("Ошибка", ex.Message, "ОК");
        }
    }
}
