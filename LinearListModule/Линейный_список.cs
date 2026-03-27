using System.Collections.ObjectModel;

namespace LinearListModule;

public sealed class Линейный_список<T>
{
    private readonly List<T> _items;
    private int _currentIndex;

    public Линейный_список()
    {
        _items = [];
        _currentIndex = -1;
    }

    public Линейный_список(IEnumerable<T> элементы)
    {
        _items = элементы?.ToList() ?? throw new ArgumentNullException(nameof(элементы));
        _currentIndex = _items.Count > 0 ? 0 : -1;
    }

    public T? ТекущийЭлемент =>
        _currentIndex >= 0 && _currentIndex < _items.Count
            ? _items[_currentIndex]
            : default;

    public int КоличествоЭлементов => _items.Count;

    public bool IsEmpty => _items.Count == 0;

    public int ТекущийИндекс => _currentIndex;

    public IReadOnlyList<T> Элементы => new ReadOnlyCollection<T>(_items);

    public void Добавить(T элемент)
    {
        _items.Add(элемент);

        if (_currentIndex == -1)
        {
            _currentIndex = 0;
        }
    }

    public bool УдалитьТекущий()
    {
        if (IsEmpty || _currentIndex < 0)
        {
            return false;
        }

        _items.RemoveAt(_currentIndex);

        if (_items.Count == 0)
        {
            _currentIndex = -1;
            return true;
        }

        if (_currentIndex >= _items.Count)
        {
            _currentIndex = _items.Count - 1;
        }

        return true;
    }

    public bool ПерейтиКСледующемуЭлементу()
    {
        if (IsEmpty || _currentIndex < 0 || _currentIndex >= _items.Count - 1)
        {
            return false;
        }

        _currentIndex++;
        return true;
    }

    public void ПерейтиВНачало()
    {
        _currentIndex = IsEmpty ? -1 : 0;
    }
}
