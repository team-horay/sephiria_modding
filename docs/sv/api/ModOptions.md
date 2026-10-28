<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# Modalternativ

API för modinställningar som sparas per enhet, och för att lägga till reglage, valrutor och skjutreglage på spelets alternativskärm. Hämta det från `Context.Options` eller `HorayModAPI.GetModOptions(modId)`.

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`ModOptions`](#modoptions) | Alternativvärden som varje mod kan sätta. |
| [`ModOptionsPage`](#modoptionspage) | En mods sida på alternativskärmen. |
| [`ModOptionDefinition`](#modoptiondefinition) | Gemensam definition av en post på alternativskärmen. |
| [`ModOption<T>`](#modoptiont) | En alternativpost som håller ett värde. |
| [`ModToggleOption`](#modtoggleoption) | Ett på/av-alternativ. |
| [`ModSelectionOption`](#modselectionoption) | Ett valruta-alternativ. |
| [`ModSliderOption`](#modslideroption) | Ett skjutreglage-alternativ. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Typ av rad på alternativskärmen. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Lista över modsidor som alternativskärmen läser. |


## ModOptions

```csharp
public sealed class ModOptions
```

Alternativvärden som varje mod kan sätta.

### Egenskaper

#### ModId

```csharp
public string ModId { get; }
```

modId för den här alternativuppsättningen


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Om lagringen är initierad


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Modnamnet som visas på alternativskärmen. För Steam Workshop-moddar är standardvärdet manifestets visningsnamn.

### Metoder

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Sparat bool-värde. Returnerar fallback om det saknas eller har en annan typ.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Sparat int-värde. Returnerar fallback om det saknas eller har en annan typ.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Sparat long-värde. Returnerar fallback om det saknas eller har en annan typ.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Sparat float-värde. Returnerar fallback om det saknas eller har en annan typ.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Sparat string-värde. Returnerar fallback om det saknas eller har en annan typ.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Skriver ett bool-värde till minnet.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Skriver ett int-värde till minnet.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Skriver ett long-värde till minnet.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Skriver ett float-värde till minnet.


#### SetString

```csharp
public void SetString(string key, string value)
```

Skriver ett string-värde till minnet. Accepterar inte null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Om ett värde är sparat under key


#### Remove

```csharp
public bool Remove(string key)
```

Tar bort det sparade värdet så att senare läsningar returnerar fallback.


#### Reset

```csharp
public int Reset()
```

Rensar alla alternativ för den här modden. Returnerar antalet borttagna.


#### Save

```csharp
public bool Save()
```

Sparar alternativfilen (DeviceOptions). Set*/Remove/Reset ändrar bara minnet, så anropa den här för att skriva dem till filen. Returnerar false om det inte finns någon lagring eller om skrivningen misslyckas.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Lägger till ett reglage på den här moddens sida på alternativskärmen. Ersätter en eventuell post med samma nyckel.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Lägger till en valruta. Värdet är ett index i choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Lägger till en valruta vars val anges som lokaliseringsnycklar.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Lägger till ett skjutreglage. Om step är 0 är värdet kontinuerligt.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Tar bort alla den här moddens poster på alternativskärmen. Sparade värden finns kvar.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

En mods sida på alternativskärmen. Innehåller bara UI-definitioner; sparade värden hålls separat.

### Egenskaper

#### Options

```csharp
public ModOptions Options { get; }
```

Den här sidans modalternativ


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Modnamnet som visas på alternativskärmen


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Poster i registreringsordning


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Gemensam definition av en post på alternativskärmen. Skapas av `ModOptions.Add*`.

### Egenskaper

#### Options

```csharp
public ModOptions Options { get; }
```

Modalternativen som den här posten tillhör


#### Key

```csharp
public string Key { get; }
```

Lagringsnyckel


#### Label

```csharp
public ModOptionText Label { get; }
```

Visningsnamn. Om det är tomt visas nyckeln.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Typ av rad

### Konstruktorer

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Validerar key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

En alternativpost som håller ett värde. Behåll handtaget för att läsa och ändra värdet från kod.

### Händelser

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Anropas med det nya värdet efter att värdet har ändrats.

### Egenskaper

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Standardvärdet som angavs vid registreringen


#### Value

```csharp
public T Value { get; set; }
```

Aktuellt värde. Att sätta det lagrar värdet i minnet och utlöser `ValueChanged` om det ändrades.

### Konstruktorer

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged blir den första prenumeranten på `ValueChanged`.

### Metoder

#### Read

```csharp
protected abstract T Read()
```

Läser värdet från lagringen.


#### Write

```csharp
protected abstract void Write(T value)
```

Skriver värdet till lagringens minne.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Begränsar värdet till det tillåtna intervallet innan det skrivs.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Ett på/av-alternativ.

### Egenskaper

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Alltid Toggle

### Metoder

#### Read

```csharp
protected override bool Read()
```

Läser bool-värdet.


#### Write

```csharp
protected override void Write(bool value)
```

Skriver bool-värdet.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Ett valruta-alternativ. Värdet är ett index i `Choices`.

### Egenskaper

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Alltid SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Lista över val

### Metoder

#### Read

```csharp
protected override int Read()
```

Sparat index. Standardvärdet om det ligger utanför intervallet.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Begränsar till valens intervall.


#### Write

```csharp
protected override void Write(int value)
```

Skriver indexet.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Ett skjutreglage-alternativ.

### Egenskaper

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Alltid Slider


#### Min

```csharp
public float Min { get; }
```

Minimivärde


#### Max

```csharp
public float Max { get; }
```

Maximivärde


#### Step

```csharp
public float Step { get; }
```

Stegstorlek. Kontinuerligt om 0.

### Metoder

#### Read

```csharp
protected override float Read()
```

Sparat värde, normaliserat


#### Normalize

```csharp
protected override float Normalize(float value)
```

Begränsar till intervallet och snäpper till steget.


#### Write

```csharp
protected override void Write(float value)
```

Skriver float-värdet.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Ingen beskrivning ännu.*

### Metoder

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Visar strängen som den är.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Översätter LocalizedString-nyckeln vid visning.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Typ av rad på alternativskärmen.

| Värde | Beskrivning |
|---|---|
| `Toggle` | På/av |
| `SelectionBox` | Valruta |
| `Slider` | Skjutreglage |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Lista över modsidor som alternativskärmen läser.

### Metoder

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Returnerar sidorna som ska visas, sorterade efter modId.
