<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# Statystyki specjalne

Typy wyświetlania statystyk rejestrowane przez `RegisterSpecialStatCategory` i `RegisterSpecialStat` w `HorayModAPI.OnLoadSpecialStatDatabase`.

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Informacje o docelowym awatarze przekazywane do funkcji odczytujących wartości. |
| [`SpecialStatValue`](#specialstatvalue) | Wartość jednego wiersza statystyki specjalnej. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Dane kategorii statystyk specjalnych w interfejsie statystyk. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Określa, kiedy kategoria jest wyświetlana. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Szczegółowe dane dla `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Określa, skąd odczytywana jest wyświetlana wartość statystyki specjalnej. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Kolor nazwy wiersza statystyki specjalnej. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Zasób, który odczytuje tylko wyświetlaną wartość, bez StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Informacje o docelowym awatarze przekazywane do funkcji odczytujących wartości.

### Właściwości

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Docelowa jednostka


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

AvatarStatsHooker celu. Null, jeśli brak.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Kontroler broni celu. Null, jeśli brak.

### Konstruktory

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Jeśli hooker lub weapons zostanie pominięty, jest wyszukiwany na podstawie avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Wartość jednego wiersza statystyki specjalnej. Wartości liczbowe twórz przez `Number`.

### Stałe i pola

#### text

```csharp
public string text
```

Wyświetlany ciąg znaków


#### hasStat

```csharp
public bool hasStat
```

Czy istnieje wartość


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Kolor wzrostu/spadku


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Czy `numericValue` jest poprawne


#### numericValue

```csharp
public double numericValue
```

Liczba do wyświetlenia


#### numberFormat

```csharp
public string numberFormat
```

Format wyświetlania liczby


#### symbolKey

```csharp
public string symbolKey
```

Klucz lokalizacji symbolu jednostki


#### symbol

```csharp
public string symbol
```

Ciąg znaków symbolu jednostki


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Wartości podstawiane za słowa kluczowe w podpowiedzi

### Metody

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Tworzy wartość liczbową.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Dane kategorii statystyk specjalnych w interfejsie statystyk.

### Stałe i pola

#### id

```csharp
public string id
```

Identyfikator


#### enabled

```csharp
public bool enabled
```

Czy wyświetlać kategorię


#### displayName

```csharp
public LocalizedString displayName
```

Klucz lokalizacji tytułu


#### fallbackName

```csharp
public string fallbackName
```

Tytuł używany, gdy brak tłumaczenia


#### headerColor

```csharp
public Color headerColor
```

Kolor tytułu


#### order

```csharp
public int order
```

Kolejność sortowania


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Warunek wyświetlania


#### requireWeapon

```csharp
public bool requireWeapon
```

Czy wyświetlać tylko wtedy, gdy wyposażona jest określona broń


#### weaponType

```csharp
public EWeaponType weaponType
```

Typ broni wymagany, gdy ustawione jest `requireWeapon`

### Właściwości

#### DisplayName

```csharp
public string DisplayName { get; }
```

Przetłumaczony tytuł


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Określa, kiedy kategoria jest wyświetlana.

| Wartość | Opis |
|---|---|
| `Always` | Zawsze |
| `WhenAnyStatHasValue` | Tylko wtedy, gdy przynajmniej jeden wiersz ma wartość |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Szczegółowe dane dla `StatusEntity.specialStat`. Określa, jak ta statystyka jest wyświetlana w interfejsie statystyk i w interfejsie historii rozgrywek.

### Stałe i pola

#### enabled

```csharp
public bool enabled
```

Czy wyświetlać w panelu


#### category

```csharp
public SpecialStatCategoryEntity category
```

Kategoria, do której należy (wymagane)


#### order

```csharp
public int order
```

Kolejność sortowania w obrębie kategorii


#### showWhenZero

```csharp
public bool showWhenZero
```

Czy wyświetlać wiersz nawet wtedy, gdy nie ma wartości


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Klucz lokalizacji nazwy używanej tylko w panelu statystyk specjalnych


#### fallbackLabel

```csharp
public string fallbackLabel
```

Nazwa używana, gdy brak tłumaczenia


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Wybór koloru nazwy


#### labelColor

```csharp
public Color labelColor
```

Kolor nazwy przy `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Sposób odczytu wartości


#### valueId

```csharp
public string valueId
```

ID wyszukiwania StatHook lub klucz niestandardowej statystyki. Jeśli puste, używane jest StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Zasób odczytujący wartość przy `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Dzielnik do wyświetlania


#### displayOffset

```csharp
public float displayOffset
```

Przesunięcie dodawane do wyświetlania


#### numberFormat

```csharp
public string numberFormat
```

Format wyświetlania liczby


#### includeSign

```csharp
public bool includeSign
```

Czy wyświetlać znak


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Określa, skąd odczytywana jest wyświetlana wartość statystyki specjalnej.

| Wartość | Opis |
|---|---|
| `StatHook` | Wynik `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Wartość `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Wynik `Read` zasobu `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Kolor nazwy wiersza statystyki specjalnej.

| Wartość | Opis |
|---|---|
| `Default` | Biały |
| `Keyword` | Kolor powiązanego słowa kluczowego |
| `Override` | Wartość `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Zasób, który odczytuje tylko wyświetlaną wartość, bez StatusInstance.

### Metody

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Odczytuje bieżącą wyświetlaną wartość status.
