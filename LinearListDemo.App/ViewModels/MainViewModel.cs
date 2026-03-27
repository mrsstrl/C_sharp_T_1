using System.Collections.ObjectModel;
using System.Windows.Input;
using LinearListDemo.App.Infrastructure;
using LinearListModule;

namespace LinearListDemo.App.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly Линейный_список<string> _linearList;
    private readonly RelayCommand _addCommand;
    private readonly RelayCommand _removeCurrentCommand;
    private readonly RelayCommand _moveNextCommand;
    private readonly RelayCommand _moveToStartCommand;
    private string _newElement = string.Empty;
    private string _statusMessage = string.Empty;

    public MainViewModel()
    {
        // Начальные значения передаются извне класса списка, что соответствует условию задания.
        _linearList = new Линейный_список<string>(["Первый", "Второй", "Третий"]);

        _addCommand = new RelayCommand(AddElement, CanAddElement);
        _removeCurrentCommand = new RelayCommand(RemoveCurrentElement, () => !_linearList.IsEmpty);
        _moveNextCommand = new RelayCommand(MoveToNextElement, CanMoveNext);
        _moveToStartCommand = new RelayCommand(MoveToStart, () => !_linearList.IsEmpty);

        AddCommand = _addCommand;
        RemoveCurrentCommand = _removeCurrentCommand;
        MoveNextCommand = _moveNextCommand;
        MoveToStartCommand = _moveToStartCommand;

        RefreshViewState();
        StatusMessage = "Список инициализирован.";
    }

    public ObservableCollection<string> Elements { get; } = [];

    public ICommand AddCommand { get; }

    public ICommand RemoveCurrentCommand { get; }

    public ICommand MoveNextCommand { get; }

    public ICommand MoveToStartCommand { get; }

    public string NewElement
    {
        get => _newElement;
        set
        {
            if (_newElement == value)
            {
                return;
            }

            _newElement = value;
            OnPropertyChanged();
            _addCommand.RaiseCanExecuteChanged();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set
        {
            if (_statusMessage == value)
            {
                return;
            }

            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public string CurrentElementText => _linearList.ТекущийЭлемент?.ToString() ?? "Нет текущего элемента";

    public int CurrentIndex => _linearList.ТекущийИндекс;

    public string CurrentPositionText =>
        _linearList.ТекущийИндекс >= 0
            ? $"{_linearList.ТекущийИндекс + 1} из {Count}"
            : "Нет позиции";

    public int Count => _linearList.КоличествоЭлементов;

    public bool IsEmpty => _linearList.IsEmpty;

    private bool CanAddElement() => !string.IsNullOrWhiteSpace(NewElement);

    private bool CanMoveNext() =>
        !_linearList.IsEmpty && _linearList.ТекущийИндекс < _linearList.КоличествоЭлементов - 1;

    private void AddElement()
    {
        var element = NewElement.Trim();
        _linearList.Добавить(element);
        NewElement = string.Empty;

        RefreshViewState();
        StatusMessage = $"Добавлен элемент: {element}";
    }

    private void RemoveCurrentElement()
    {
        var current = _linearList.ТекущийЭлемент?.ToString() ?? "<null>";
        var wasRemoved = _linearList.УдалитьТекущий();

        RefreshViewState();
        StatusMessage = wasRemoved
            ? $"Удалён текущий элемент: {current}"
            : "Удаление невозможно: список пуст.";
    }

    private void MoveToNextElement()
    {
        var moved = _linearList.ПерейтиКСледующемуЭлементу();

        RefreshViewState();
        StatusMessage = moved
            ? "Переход к следующему элементу выполнен."
            : "Переход невозможен: текущий элемент последний или список пуст.";
    }

    private void MoveToStart()
    {
        _linearList.ПерейтиВНачало();

        RefreshViewState();
        StatusMessage = IsEmpty
            ? "Переход в начало невозможен: список пуст."
            : "Текущий указатель перемещён в начало списка.";
    }

    private void RefreshViewState()
    {
        Elements.Clear();

        foreach (var element in _linearList.Элементы)
        {
            Elements.Add(element?.ToString() ?? "<null>");
        }

        OnPropertyChanged(nameof(CurrentElementText));
        OnPropertyChanged(nameof(CurrentIndex));
        OnPropertyChanged(nameof(CurrentPositionText));
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(nameof(IsEmpty));

        _removeCurrentCommand.RaiseCanExecuteChanged();
        _moveNextCommand.RaiseCanExecuteChanged();
        _moveToStartCommand.RaiseCanExecuteChanged();
    }
}
