<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте его напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# Точка входа и контекст

Базовые классы, от которых наследуется класс точки входа мода, а также типы контекста и манифеста, которые подключает загрузчик. Новые моды должны наследоваться от `WorkshopModBase`.

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | Базовый класс точки входа для модов из Мастерской. |
| [`WorkshopModContext`](#workshopmodcontext) | Контекст только для чтения, передаваемый DLL-модулю из Мастерской. |
| [`HorayModBase`](#horaymodbase) | Базовый класс для классов точки входа DLL-модов. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Один пакет модулей данных, который читает мод-потребитель. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Проверенная спецификация выполнения. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Одна запись в `modules[]` манифеста. |
| [`WorkshopDependency`](#workshopdependency) | Одна запись в `dependencies[]` манифеста. |
| [`WorkshopModuleType`](#workshopmoduletype) | Значение `modules[].type` манифеста. |
| [`WorkshopInstallation`](#workshopinstallation) | Объявление автора о том, должны ли остальные участники устанавливать мод (`multiplayer.installation` манифеста). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

Базовый класс точки входа для модов из Мастерской.

### Свойства

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Действителен с момента вызова OnModLoaded.

### Методы

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Пакеты модулей данных, которые указывают этот мод в качестве потребителя, в порядке загрузки. Вызывайте после завершения активации (после событий баз данных). Возвращает пустой список, если контекста нет (например, путь AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Контекст только для чтения, передаваемый DLL-модулю из Мастерской. Путь установки может отличаться, поэтому моды используют эти значения для поиска собственных файлов.

### Свойства

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Абсолютный путь к корню пакета (папке установки).


#### FileId

```csharp
public ulong FileId { get; }
```

ID файла в Мастерской Steam. Локальные пакеты получают уникальный ID, производный от их пути.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

Проверенная спецификация выполнения.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

Запись dll-модуля, которая создала этот экземпляр.


#### Options

```csharp
public ModOptions Options { get; }
```

Настройки для устройства, привязанные к этому modId. Общие для DLL-модулей пакета, а также для его локальной копии и копии по подписке.

### Конструкторы

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Описания пока нет.*

### Методы

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Преобразует путь относительно корня пакета в абсолютный путь. Пути, выходящие за пределы корня, и абсолютные пути отклоняются (выбрасывается исключение).


## HorayModBase

```csharp
public class HorayModBase
```

Базовый класс для классов точки входа DLL-модов.

### Константы и поля

#### metadata

```csharp
public Metadata metadata
```

Метаданные, полученные в `LoadMod`.

### Методы

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Вызывается загрузчиком. Сохраняет метаданные, затем вызывает `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Точка инициализации мода. Вызывается до инициализации баз данных, поэтому подписывайтесь на события `HorayModAPI` здесь.


#### UnloadMod

```csharp
public void UnloadMod()
```

Вызывает `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Точка выгрузки мода. Размещайте здесь очистку, например отписку от событий.

### Вложенные типы

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Идентификационные данные мода, которые загрузчик передаёт в `LoadMod`.

##### Константы и поля

###### modName

```csharp
public string modName
```

Отображаемое имя.


###### modVersion

```csharp
public string modVersion
```

Версия.


###### modAuthor

```csharp
public string modAuthor
```

Автор.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Один пакет модулей данных, который читает мод-потребитель. Получите его через `WorkshopModBase.GetDataPackages()`.

### Свойства

#### ModId

```csharp
public string ModId { get; }
```

modId пакета.


#### Version

```csharp
public string Version { get; }
```

Версия пакета.


#### FileId

```csharp
public ulong FileId { get; }
```

ID файла в Мастерской Steam.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Отображаемое имя (info.title или modId, если оно отсутствует).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Абсолютный путь к корню пакета.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Модули данных, предназначенные этому потребителю, в порядке объявления.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Абсолютные пути к папкам модулей, в порядке объявления. То же, что Modules[i].FullPath.

### Методы

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Выбирает из записей активации (в порядке загрузки) пакеты, у которых есть модули данных для `consumerModId`. Включает только состояния, в которых зависимости удовлетворены (Active, AlreadyActive, NoDllModules). Данные из пакетов, которые не загрузились или применились частично, не передаются.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Проверенная спецификация выполнения. Неизменяемая.

### Свойства

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Абсолютный путь к корню пакета.


#### ModId

```csharp
public string ModId { get; }
```

modId из манифеста.


#### Version

```csharp
public string Version { get; }
```

Версия из манифеста.


#### Author

```csharp
public string Author { get; }
```

Автор из манифеста. null, если отсутствует.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title или modId, если оно отсутствует.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Требование к установке в многопользовательской игре.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Был ли объект multiplayer объявлен явно. Если false, Installation содержит нормализованное значение по умолчанию.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Список модулей в порядке объявления.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Абсолютные пути к вспомогательным управляемым DLL. Они не получают колбэков запуска.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Список модов, от которых зависит этот мод.


#### HasDll

```csharp
public bool HasDll { get; }
```

Выводится из того, содержит ли modules тип dll. Отдельного флага нет.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Содержит ли modules тип localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Содержит ли modules тип costume.


#### HasData

```csharp
public bool HasData { get; }
```

Содержит ли modules тип data.

### Конструкторы

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Описания пока нет.*

### Методы

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Модули данных, которые получает `consumerModId`, в порядке объявления. Пустой список, если таких нет. Модуль с указанным потребителем достаётся только этому моду; модуль без потребителя достаётся самому пакету и каждому моду, который напрямую от него зависит.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Одна запись в `modules[]` манифеста.

### Свойства

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Тип модуля.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Путь относительно корня пакета (разделители нормализованы к '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Проверенный абсолютный путь.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Полное имя класса точки входа dll-модуля. null для localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId мода, который читает модуль данных. Если не указан (null), это сам пакет плюс каждый напрямую зависящий мод. Для модулей, отличных от data, всегда null.

### Конструкторы

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Создаётся валидатором.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Создаётся валидатором.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Одна запись в `dependencies[]` манифеста.

### Свойства

#### ModId

```csharp
public string ModId { get; }
```

modId мода, от которого есть зависимость.


#### Version

```csharp
public string Version { get; }
```

null означает любую версию. Если задано, принимается только точное совпадение (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Подсказка, откуда скачивать. После установки должна сверяться с фактическими modId/version; сама по себе она не доказывает, что скачан нужный мод.

### Конструкторы

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Создаётся валидатором.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Значение `modules[].type` манифеста.

| Значение | Описание |
|---|---|
| `Dll` | Модуль управляемой DLL |
| `Localization` | Модуль папки локализации |
| `Costume` | Папка пакета костюма без кода (costume.json/skin.json + PNG) |
| `Data` | Универсальная папка данных, которую читает другой мод (потребитель); игра не интерпретирует её содержимое |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

Объявление автора о том, должны ли остальные участники устанавливать мод (`multiplayer.installation` манифеста).

| Значение | Описание |
|---|---|
| `Unknown` | Не объявлено (включается в сравнение конфигурации лобби так же, как Everyone) |
| `LocalOnly` | Работает только на устройстве пользователя; исключается из сравнения конфигурации лобби |
| `Everyone` | Все в комнате должны установить один и тот же пакет |
| `HostOnly` | Установить нужно только хосту |
