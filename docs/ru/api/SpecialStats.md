<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# Особые характеристики

Типы отображения характеристик, регистрируемые через `RegisterSpecialStatCategory` и `RegisterSpecialStat` в `HorayModAPI.OnLoadSpecialStatDatabase`.

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Сведения о целевом аватаре, передаваемые функциям чтения значения. |
| [`SpecialStatValue`](#specialstatvalue) | Значение одной строки особой характеристики. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Данные категории особых характеристик в UI характеристик. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Определяет, когда категория отображается. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Подробные данные для `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Определяет, откуда читается отображаемое значение особой характеристики. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Цвет имени строки особой характеристики. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Ассет, который читает только отображаемое значение, без StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Сведения о целевом аватаре, передаваемые функциям чтения значения.

### Свойства

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Целевой юнит


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

AvatarStatsHooker цели. Null, если отсутствует.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Контроллер оружия цели. Null, если отсутствует.

### Конструкторы

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Если hooker или weapons не указаны, они ищутся по avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Значение одной строки особой характеристики. Числовые значения создавайте через `Number`.

### Константы и поля

#### text

```csharp
public string text
```

Отображаемая строка


#### hasStat

```csharp
public bool hasStat
```

Есть ли значение


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Цвет увеличения/уменьшения


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Действительно ли `numericValue`


#### numericValue

```csharp
public double numericValue
```

Число для отображения


#### numberFormat

```csharp
public string numberFormat
```

Формат отображения числа


#### symbolKey

```csharp
public string symbolKey
```

Ключ локализации для символа единицы измерения


#### symbol

```csharp
public string symbol
```

Строка символа единицы измерения


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Значения подстановки ключевых слов для подсказки

### Методы

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Создаёт числовое значение.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Данные категории особых характеристик в UI характеристик.

### Константы и поля

#### id

```csharp
public string id
```

Идентификатор


#### enabled

```csharp
public bool enabled
```

Показывать ли категорию


#### displayName

```csharp
public LocalizedString displayName
```

Ключ локализации для заголовка


#### fallbackName

```csharp
public string fallbackName
```

Заголовок, используемый при отсутствии перевода


#### headerColor

```csharp
public Color headerColor
```

Цвет заголовка


#### order

```csharp
public int order
```

Порядок сортировки


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Условие отображения


#### requireWeapon

```csharp
public bool requireWeapon
```

Показывать ли только при экипированном определённом оружии


#### weaponType

```csharp
public EWeaponType weaponType
```

Тип оружия, требуемый при установленном `requireWeapon`

### Свойства

#### DisplayName

```csharp
public string DisplayName { get; }
```

Переведённый заголовок


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Определяет, когда категория отображается.

| Значение | Описание |
|---|---|
| `Always` | Всегда |
| `WhenAnyStatHasValue` | Только когда хотя бы у одной строки есть значение |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Подробные данные для `StatusEntity.specialStat`. Определяет, как эта характеристика отображается в UI характеристик и в UI истории забегов.

### Константы и поля

#### enabled

```csharp
public bool enabled
```

Показывать ли в панели


#### category

```csharp
public SpecialStatCategoryEntity category
```

Категория, к которой относится (обязательно)


#### order

```csharp
public int order
```

Порядок сортировки внутри категории


#### showWhenZero

```csharp
public bool showWhenZero
```

Показывать ли строку даже при отсутствии значения


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Ключ локализации имени, используемого только в панели особых характеристик


#### fallbackLabel

```csharp
public string fallbackLabel
```

Имя, используемое при отсутствии перевода


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Выбор цвета имени


#### labelColor

```csharp
public Color labelColor
```

Цвет имени при `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Как читается значение


#### valueId

```csharp
public string valueId
```

ID для поиска StatHook или ключ пользовательской характеристики. Если пусто, используется StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Ассет, читающий значение при `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Делитель для отображения


#### displayOffset

```csharp
public float displayOffset
```

Смещение, добавляемое для отображения


#### numberFormat

```csharp
public string numberFormat
```

Формат отображения числа


#### includeSign

```csharp
public bool includeSign
```

Показывать ли знак


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Определяет, откуда читается отображаемое значение особой характеристики.

| Значение | Описание |
|---|---|
| `StatHook` | Результат `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Значение `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Результат `Read` ассета `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Цвет имени строки особой характеристики.

| Значение | Описание |
|---|---|
| `Default` | Белый |
| `Keyword` | Цвет связанного ключевого слова |
| `Override` | Значение `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Ассет, который читает только отображаемое значение, без StatusInstance.

### Методы

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Читает текущее отображаемое значение status.
