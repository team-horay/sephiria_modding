<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# Настройки модов

API для настроек каждого мода, сохраняемых на устройстве, и для добавления переключателей, полей выбора и ползунков на экран настроек игры. Получите его через `Context.Options` или `HorayModAPI.GetModOptions(modId)`.

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`ModOptions`](#modoptions) | Значения настроек, которые может задавать каждый мод. |
| [`ModOptionsPage`](#modoptionspage) | Страница одного мода на экране настроек. |
| [`ModOptionDefinition`](#modoptiondefinition) | Общее определение элемента экрана настроек. |
| [`ModOption<T>`](#modoptiont) | Элемент настройки, хранящий значение. |
| [`ModToggleOption`](#modtoggleoption) | Настройка вкл./выкл. |
| [`ModSelectionOption`](#modselectionoption) | Настройка в виде поля выбора. |
| [`ModSliderOption`](#modslideroption) | Настройка в виде ползунка. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Вид строки на экране настроек. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Список страниц модов, которые читает экран настроек. |


## ModOptions

```csharp
public sealed class ModOptions
```

Значения настроек, которые может задавать каждый мод.

### Свойства

#### ModId

```csharp
public string ModId { get; }
```

modId этого набора настроек


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Инициализировано ли хранилище


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Имя мода, отображаемое на экране настроек. Для модов из Мастерской Steam по умолчанию равно отображаемому имени из манифеста.

### Методы

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Сохранённое значение bool. Возвращает fallback, если значение отсутствует или имеет другой тип.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Сохранённое значение int. Возвращает fallback, если значение отсутствует или имеет другой тип.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Сохранённое значение long. Возвращает fallback, если значение отсутствует или имеет другой тип.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Сохранённое значение float. Возвращает fallback, если значение отсутствует или имеет другой тип.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Сохранённое значение string. Возвращает fallback, если значение отсутствует или имеет другой тип.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Записывает значение bool в память.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Записывает значение int в память.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Записывает значение long в память.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Записывает значение float в память.


#### SetString

```csharp
public void SetString(string key, string value)
```

Записывает значение string в память. Не принимает null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Сохранено ли значение под ключом key


#### Remove

```csharp
public bool Remove(string key)
```

Удаляет сохранённое значение, так что последующие чтения возвращают fallback.


#### Reset

```csharp
public int Reset()
```

Очищает все настройки этого мода. Возвращает количество удалённых.


#### Save

```csharp
public bool Save()
```

Сохраняет файл настроек (DeviceOptions). Set*/Remove/Reset изменяют только память, поэтому вызывайте этот метод, чтобы записать их в файл. Возвращает false, если хранилища нет или запись не удалась.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Добавляет переключатель на страницу этого мода на экране настроек. Заменяет любой элемент с тем же ключом.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Добавляет поле выбора. Значением является индекс в choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Добавляет поле выбора, варианты которого заданы ключами локализации.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Добавляет ползунок. Если step равен 0, значение непрерывное.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Удаляет все элементы этого мода с экрана настроек. Сохранённые значения остаются.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

Страница одного мода на экране настроек. Содержит только определения UI; сохранённые значения хранятся отдельно.

### Свойства

#### Options

```csharp
public ModOptions Options { get; }
```

Настройки мода этой страницы


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Имя мода, отображаемое на экране настроек


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Элементы в порядке регистрации


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Общее определение элемента экрана настроек. Создаётся через `ModOptions.Add*`.

### Свойства

#### Options

```csharp
public ModOptions Options { get; }
```

Настройки мода, которым принадлежит этот элемент


#### Key

```csharp
public string Key { get; }
```

Ключ хранения


#### Label

```csharp
public ModOptionText Label { get; }
```

Отображаемое имя. Если пустое, показывается ключ.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Вид строки

### Конструкторы

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Проверяет key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Элемент настройки, хранящий значение. Сохраните дескриптор, чтобы читать и изменять значение из кода.

### События

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Вызывается с новым значением после его изменения.

### Свойства

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Значение по умолчанию, заданное при регистрации


#### Value

```csharp
public T Value { get; set; }
```

Текущее значение. При присваивании значение сохраняется в памяти и, если оно изменилось, вызывается `ValueChanged`.

### Конструкторы

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged становится первым подписчиком `ValueChanged`.

### Методы

#### Read

```csharp
protected abstract T Read()
```

Читает значение из хранилища.


#### Write

```csharp
protected abstract void Write(T value)
```

Записывает значение в память хранилища.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Ограничивает значение допустимым диапазоном перед записью.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Настройка вкл./выкл.

### Свойства

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Всегда Toggle

### Методы

#### Read

```csharp
protected override bool Read()
```

Читает значение bool.


#### Write

```csharp
protected override void Write(bool value)
```

Записывает значение bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Настройка в виде поля выбора. Значением является индекс в `Choices`.

### Свойства

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Всегда SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Список вариантов

### Методы

#### Read

```csharp
protected override int Read()
```

Сохранённый индекс. Значение по умолчанию, если вне диапазона.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Ограничивает диапазоном вариантов.


#### Write

```csharp
protected override void Write(int value)
```

Записывает индекс.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Настройка в виде ползунка.

### Свойства

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Всегда Slider


#### Min

```csharp
public float Min { get; }
```

Минимальное значение


#### Max

```csharp
public float Max { get; }
```

Максимальное значение


#### Step

```csharp
public float Step { get; }
```

Размер шага. Непрерывное, если 0.

### Методы

#### Read

```csharp
protected override float Read()
```

Сохранённое значение, нормализованное


#### Normalize

```csharp
protected override float Normalize(float value)
```

Ограничивает диапазоном и округляет до шага.


#### Write

```csharp
protected override void Write(float value)
```

Записывает значение float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Описания пока нет.*

### Методы

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Отображает строку как есть.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Переводит ключ LocalizedString в момент отображения.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Вид строки на экране настроек.

| Значение | Описание |
|---|---|
| `Toggle` | Вкл./выкл. |
| `SelectionBox` | Поле выбора |
| `Slider` | Ползунок |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Список страниц модов, которые читает экран настроек.

### Методы

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Возвращает страницы для отображения, упорядоченные по modId.
