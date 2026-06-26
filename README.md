# Mycolog — Дневник грибного культиватора

**WPF-приложение** для ведения учёта грибных культур (мицелий, плодоношение, урожай и т.д.).

## Возможности

- Добавление, редактирование и удаление грибных культур
- Учёт параметров: вид, штамм, субстрат, ёмкость, стадия, температура, влажность, вес урожая
- Динамические справочники (виды, штаммы, субстраты и т.д.) с возможностью добавления новых значений
- Автоматическая инициализация базы данных с тестовыми данными
- Просмотр всех культур в удобной таблице
- Интеграция с **Entity Framework 6 + SQL Server**

## Структура проекта
Mycolog/
├── Mycolog/                  # Основной WPF-проект
│   ├── MainWindow.xaml       # Главное окно
│   ├── MainWindow.xaml.cs
│   ├── MushroomCulture.cs    # Модель культуры
│   ├── MycologContext.cs     # DbContext
│   ├── DatabaseInitializer.cs # Начальные данные
│   ├── DataDictionaries.cs   # Справочники
│   └── Styles/Styles.xaml
├── Mycolog.Tests/            # Unit-тесты (MSTest)
├── SQL_Code.sql              # SQL-скрипт создания базы данных
└── EntityFramework.dll       # (включены в архив)
## Технологии

- **C# + WPF** (.NET Framework 4.8)
- **Entity Framework 6**
- **Microsoft SQL Server**
- **MSTest** — модульное тестирование

## Установка и запуск

### 1. База данных

1. Выполни скрипт `SQL_Code.sql` в SQL Server Management Studio (или другом клиенте).
2. Создастся база `MycologDB`.

### 2. Настройка подключения

В файле `App.config` (или `Mycolog.exe.config`) убедись, что строка подключения правильная:

```xml
<connectionStrings>
  <add name="MycologDbConnection"
       connectionString="Data Source="ТУТ НАЗВАНИЕ СЕРВЕРА SQL";Initial Catalog=MycologDB;Integrated Security=True;MultipleActiveResultSets=True"  
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

###3. Запуск

1. Открой решение Mycolog.sln в Visual Studio.
2. Установи Mycolog как стартовый проект.
3. Нажми F5.
   
При первом запуске база данных автоматически заполнится тестовыми данными.

### 4. Как работать со справочниками

В форме добавления/редактирования есть кнопки ... напротив полей.
Нажми → введи новое значение → оно сохранится в глобальные списки приложения.

### 5. Тесты

Проект Mycolog.Tests содержит базовые тесты моделей.
Запуск тестов: `dotnet test   # или через Test Explorer в Visual Studio`

### 6. Лицензия

Свободное использование для личных и образовательных целей.
