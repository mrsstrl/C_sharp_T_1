# Лабораторная работа: класс `Линейный_список`

## Описание
Проект реализован на C# как оконное приложение (WPF) с демонстрацией работы объектов класса `Линейный_список`.

Решение состоит из двух модулей:
- `LinearListModule` — отдельная библиотека классов с реализацией структуры данных.
- `LinearListDemo.App` — WPF-приложение с UI и MVVM-слоем.

## Реализованный класс
Файл: `LinearListModule/Линейный_список.cs`

Класс обобщённый:
- `Линейный_список<T>`

Свойства:
- `ТекущийЭлемент`
- `КоличествоЭлементов`
- `IsEmpty`

Методы:
- `Добавить(T элемент)`
- `УдалитьТекущий()`
- `ПерейтиКСледующемуЭлементу()`  
  Возвращает `false`, если перейти к следующему элементу нельзя, иначе `true`.
- `ПерейтиВНачало()`

Дополнительно для демонстрации:
- `ТекущийИндекс`
- `Элементы` (только чтение)

## Инициализация вне класса
В соответствии с условием задания значения не зашиваются внутри класса списка.  
Начальная инициализация выполняется снаружи, во `ViewModel`:

```csharp
_linearList = new Линейный_список<string>(["Первый", "Второй", "Третий"]);
```

## MVVM-структура
- `LinearListDemo.App/ViewModels/ViewModelBase.cs` — базовый класс с `INotifyPropertyChanged`.
- `LinearListDemo.App/Infrastructure/RelayCommand.cs` — реализация команд.
- `LinearListDemo.App/ViewModels/MainViewModel.cs` — логика демонстрации.
- `LinearListDemo.App/MainWindow.xaml` — представление.

В окне доступны действия:
- добавление элемента;
- удаление текущего элемента;
- переход к следующему элементу;
- переход в начало;
- отображение текущего элемента, количества элементов и признака `IsEmpty`.

## Сборка и запуск
Требования:
- .NET SDK 10.0+ (или совместимый с `net10.0-windows`);
- Windows (WPF-приложение).

Команды:

```bash
dotnet restore LinearListDemo.App/LinearListDemo.App.csproj
dotnet build LinearListDemo.App/LinearListDemo.App.csproj
dotnet run --project LinearListDemo.App/LinearListDemo.App.csproj
```

## Структура проекта
```text
LinearListDemo.sln
LinearListDemo.slnx
├── LinearListModule
│   ├── LinearListModule.csproj
│   └── Линейный_список.cs
└── LinearListDemo.App
    ├── LinearListDemo.App.csproj
    ├── App.xaml
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
    ├── Infrastructure
    │   └── RelayCommand.cs
    └── ViewModels
        ├── MainViewModel.cs
        └── ViewModelBase.cs
```
