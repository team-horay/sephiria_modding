<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# Opcje modów

API do ustawień moda zapisywanych na urządzeniu oraz do dodawania przełączników, pól wyboru i suwaków do ekranu opcji gry. Pobierz je przez `Context.Options` lub `HorayModAPI.GetModOptions(modId)`.

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`ModOptions`](#modoptions) | Wartości opcji, które może ustawić każdy mod. |
| [`ModOptionsPage`](#modoptionspage) | Strona jednego moda na ekranie opcji. |
| [`ModOptionDefinition`](#modoptiondefinition) | Wspólna definicja elementu ekranu opcji. |
| [`ModOption<T>`](#modoptiont) | Element opcji przechowujący wartość. |
| [`ModToggleOption`](#modtoggleoption) | Opcja włącz/wyłącz. |
| [`ModSelectionOption`](#modselectionoption) | Opcja z polem wyboru. |
| [`ModSliderOption`](#modslideroption) | Opcja z suwakiem. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Rodzaj wiersza na ekranie opcji. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Lista stron modów odczytywana przez ekran opcji. |


## ModOptions

```csharp
public sealed class ModOptions
```

Wartości opcji, które może ustawić każdy mod.

### Właściwości

#### ModId

```csharp
public string ModId { get; }
```

modId tego zestawu opcji


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Czy magazyn jest zainicjalizowany


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Nazwa moda wyświetlana na ekranie opcji. Dla modów z Warsztatu Steam domyślnie jest to wyświetlana nazwa z manifestu.

### Metody

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Zapisana wartość bool. Zwraca fallback, jeśli brak wartości lub jest innego typu.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Zapisana wartość int. Zwraca fallback, jeśli brak wartości lub jest innego typu.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Zapisana wartość long. Zwraca fallback, jeśli brak wartości lub jest innego typu.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Zapisana wartość float. Zwraca fallback, jeśli brak wartości lub jest innego typu.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Zapisana wartość string. Zwraca fallback, jeśli brak wartości lub jest innego typu.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Zapisuje wartość bool w pamięci.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Zapisuje wartość int w pamięci.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Zapisuje wartość long w pamięci.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Zapisuje wartość float w pamięci.


#### SetString

```csharp
public void SetString(string key, string value)
```

Zapisuje wartość string w pamięci. Nie przyjmuje null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Czy pod kluczem key zapisana jest wartość


#### Remove

```csharp
public bool Remove(string key)
```

Usuwa zapisaną wartość, tak że kolejne odczyty zwracają fallback.


#### Reset

```csharp
public int Reset()
```

Czyści wszystkie opcje tego moda. Zwraca liczbę usuniętych.


#### Save

```csharp
public bool Save()
```

Zapisuje plik opcji (DeviceOptions). Set*/Remove/Reset zmieniają tylko pamięć, więc wywołaj tę metodę, aby zapisać je do pliku. Zwraca false, jeśli nie ma magazynu lub zapis się nie powiedzie.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Dodaje przełącznik do strony tego moda na ekranie opcji. Zastępuje każdy element o tym samym kluczu.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Dodaje pole wyboru. Wartość to indeks w choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Dodaje pole wyboru, którego opcje są podane jako klucze lokalizacji.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Dodaje suwak. Jeśli step wynosi 0, wartość jest ciągła.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Usuwa wszystkie elementy tego moda z ekranu opcji. Zapisane wartości pozostają.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

Strona jednego moda na ekranie opcji. Przechowuje tylko definicje interfejsu; zapisane wartości są trzymane osobno.

### Właściwości

#### Options

```csharp
public ModOptions Options { get; }
```

Opcje moda tej strony


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Nazwa moda wyświetlana na ekranie opcji


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Elementy w kolejności rejestracji


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Wspólna definicja elementu ekranu opcji. Tworzona przez `ModOptions.Add*`.

### Właściwości

#### Options

```csharp
public ModOptions Options { get; }
```

Opcje moda, do których należy ten element


#### Key

```csharp
public string Key { get; }
```

Klucz magazynu


#### Label

```csharp
public ModOptionText Label { get; }
```

Wyświetlana nazwa. Jeśli pusta, wyświetlany jest klucz.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Rodzaj wiersza

### Konstruktory

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Weryfikuje key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Element opcji przechowujący wartość. Zachowaj uchwyt, aby odczytywać i zmieniać wartość z kodu.

### Zdarzenia

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Wywoływane z nową wartością po zmianie wartości.

### Właściwości

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Wartość domyślna podana przy rejestracji


#### Value

```csharp
public T Value { get; set; }
```

Bieżąca wartość. Ustawienie jej zapisuje wartość w pamięci i wywołuje `ValueChanged`, jeśli się zmieniła.

### Konstruktory

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged staje się pierwszym subskrybentem `ValueChanged`.

### Metody

#### Read

```csharp
protected abstract T Read()
```

Odczytuje wartość z magazynu.


#### Write

```csharp
protected abstract void Write(T value)
```

Zapisuje wartość w pamięci magazynu.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Ogranicza wartość do dozwolonego zakresu przed zapisem.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Opcja włącz/wyłącz.

### Właściwości

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Zawsze Toggle

### Metody

#### Read

```csharp
protected override bool Read()
```

Odczytuje wartość bool.


#### Write

```csharp
protected override void Write(bool value)
```

Zapisuje wartość bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Opcja z polem wyboru. Wartość to indeks w `Choices`.

### Właściwości

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Zawsze SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Lista opcji do wyboru

### Metody

#### Read

```csharp
protected override int Read()
```

Zapisany indeks. Wartość domyślna, jeśli poza zakresem.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Ogranicza do zakresu opcji.


#### Write

```csharp
protected override void Write(int value)
```

Zapisuje indeks.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Opcja z suwakiem.

### Właściwości

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Zawsze Slider


#### Min

```csharp
public float Min { get; }
```

Wartość minimalna


#### Max

```csharp
public float Max { get; }
```

Wartość maksymalna


#### Step

```csharp
public float Step { get; }
```

Wielkość kroku. Ciągła, jeśli 0.

### Metody

#### Read

```csharp
protected override float Read()
```

Zapisana wartość, znormalizowana


#### Normalize

```csharp
protected override float Normalize(float value)
```

Ogranicza do zakresu i przyciąga do kroku.


#### Write

```csharp
protected override void Write(float value)
```

Zapisuje wartość float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Brak opisu.*

### Metody

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Wyświetla ciąg znaków bez zmian.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Tłumaczy klucz LocalizedString w momencie wyświetlania.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Rodzaj wiersza na ekranie opcji.

| Wartość | Opis |
|---|---|
| `Toggle` | Włącz/wyłącz |
| `SelectionBox` | Pole wyboru |
| `Slider` | Suwak |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Lista stron modów odczytywana przez ekran opcji.

### Metody

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Zwraca strony do wyświetlenia, uporządkowane według modId.
