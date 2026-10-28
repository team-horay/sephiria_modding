<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit directly; fix the source comments and regenerate. -->
# Mod Options

API for per-mod settings saved per device, and for adding toggles, selection boxes, and sliders to the game's options screen. Get it from `Context.Options` or `HorayModAPI.GetModOptions(modId)`.

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`ModOptions`](#modoptions) | Option values each mod can set. |
| [`ModOptionsPage`](#modoptionspage) | One mod's page on the options screen. |
| [`ModOptionDefinition`](#modoptiondefinition) | Common definition of an options screen item. |
| [`ModOption<T>`](#modoptiont) | An option item that holds a value. |
| [`ModToggleOption`](#modtoggleoption) | An on/off option. |
| [`ModSelectionOption`](#modselectionoption) | A selection box option. |
| [`ModSliderOption`](#modslideroption) | A slider option. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Kind of row on the options screen. |
| [`ModOptionsRegistry`](#modoptionsregistry) | List of mod pages the options screen reads. |


## ModOptions

```csharp
public sealed class ModOptions
```

Option values each mod can set.

### Properties

#### ModId

```csharp
public string ModId { get; }
```

The modId of this option set


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Whether the storage is initialized


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Mod name shown on the options screen. For Steam Workshop mods, defaults to the manifest display name.

### Methods

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Saved bool value. Returns fallback if missing or of a different type.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Saved int value. Returns fallback if missing or of a different type.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Saved long value. Returns fallback if missing or of a different type.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Saved float value. Returns fallback if missing or of a different type.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Saved string value. Returns fallback if missing or of a different type.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Writes a bool value to memory.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Writes an int value to memory.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Writes a long value to memory.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Writes a float value to memory.


#### SetString

```csharp
public void SetString(string key, string value)
```

Writes a string value to memory. Does not accept null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Whether a value is saved under key


#### Remove

```csharp
public bool Remove(string key)
```

Removes the saved value so later reads return fallback.


#### Reset

```csharp
public int Reset()
```

Clears all options for this mod. Returns the number removed.


#### Save

```csharp
public bool Save()
```

Saves the options file (DeviceOptions). Set*/Remove/Reset only change memory, so call this to write them to the file. Returns false if there is no storage or the write fails.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Adds a toggle to this mod's page on the options screen. Replaces any item with the same key.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Adds a selection box. The value is an index into choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Adds a selection box whose choices are given as localization keys.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Adds a slider. If step is 0, the value is continuous.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Removes all of this mod's options screen items. Saved values remain.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

One mod's page on the options screen. Holds only UI definitions; saved values are kept separately.

### Properties

#### Options

```csharp
public ModOptions Options { get; }
```

This page's mod options


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Mod name shown on the options screen


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Items in registration order


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Common definition of an options screen item. Created by `ModOptions.Add*`.

### Properties

#### Options

```csharp
public ModOptions Options { get; }
```

The mod options this item belongs to


#### Key

```csharp
public string Key { get; }
```

Storage key


#### Label

```csharp
public ModOptionText Label { get; }
```

Display name. If empty, the key is shown.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Kind of row

### Constructors

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Validates key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

An option item that holds a value. Keep the handle to read and change the value from code.

### Events

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Called with the new value after the value changes.

### Properties

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Default value given at registration


#### Value

```csharp
public T Value { get; set; }
```

Current value. Setting it stores the value in memory and raises `ValueChanged` if it changed.

### Constructors

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged becomes the first subscriber of `ValueChanged`.

### Methods

#### Read

```csharp
protected abstract T Read()
```

Reads the value from storage.


#### Write

```csharp
protected abstract void Write(T value)
```

Writes the value to storage memory.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Clamps the value to the allowed range before writing.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

An on/off option.

### Properties

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Always Toggle

### Methods

#### Read

```csharp
protected override bool Read()
```

Reads the bool value.


#### Write

```csharp
protected override void Write(bool value)
```

Writes the bool value.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

A selection box option. The value is an index into `Choices`.

### Properties

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Always SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

List of choices

### Methods

#### Read

```csharp
protected override int Read()
```

Saved index. The default value if out of range.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Clamps to the range of choices.


#### Write

```csharp
protected override void Write(int value)
```

Writes the index.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

A slider option.

### Properties

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Always Slider


#### Min

```csharp
public float Min { get; }
```

Minimum value


#### Max

```csharp
public float Max { get; }
```

Maximum value


#### Step

```csharp
public float Step { get; }
```

Step size. Continuous if 0.

### Methods

#### Read

```csharp
protected override float Read()
```

Saved value, normalized


#### Normalize

```csharp
protected override float Normalize(float value)
```

Clamps to the range and snaps to the step.


#### Write

```csharp
protected override void Write(float value)
```

Writes the float value.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*No description yet.*

### Methods

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Displays the string as is.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Translates the LocalizedString's key at display time.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Kind of row on the options screen.

| Value | Description |
|---|---|
| `Toggle` | On/off |
| `SelectionBox` | Selection box |
| `Slider` | Slider |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

List of mod pages the options screen reads.

### Methods

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Returns the pages to display, ordered by modId.
