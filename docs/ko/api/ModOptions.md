<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 모드 옵션

기기별로 저장되는 모드 설정값과, 게임 옵션 화면에 토글·선택 상자·슬라이더를 추가하는 API입니다. `Context.Options` 또는 `HorayModAPI.GetModOptions(modId)`로 얻습니다.

게임 버전 **1.0.34** 기준, 2026-10-22 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`ModOptions`](#modoptions) | A mod's local options in DeviceOptions.xml. |
| [`ModOptionsPage`](#modoptionspage) | UI definitions only; stored values survive removing this page or unregistering a mod. |
| [`ModOptionDefinition`](#modoptiondefinition) |  |
| [`ModOption<T>`](#modoptiont) | Retain this handle to read the registered default, or change a value and notify listeners. |
| [`ModToggleOption`](#modtoggleoption) |  |
| [`ModSelectionOption`](#modselectionoption) |  |
| [`ModSliderOption`](#modslideroption) |  |
| [`ModOptionText`](#modoptiontext) | Plain text by default; pass a LocalizedString to resolve a localization key on display. |
| [`ModOptionKind`](#modoptionkind) |  |
| [`ModOptionsRegistry`](#modoptionsregistry) |  |


## ModOptions

```csharp
public sealed class ModOptions
```

A mod's local options in DeviceOptions.xml. Use on the Unity main thread. Set/Remove/Reset change memory only; call Save to persist a batch of changes.

### 프로퍼티

#### ModId

```csharp
public string ModId { get; }
```

*설명이 아직 없습니다.*


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

*설명이 아직 없습니다.*


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Plain mod name, or a LocalizedString. Workshop defaults to its manifest display name.

### 메서드

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Missing values or values saved with a different type return the fallback without changing storage.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

*설명이 아직 없습니다.*


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

*설명이 아직 없습니다.*


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

*설명이 아직 없습니다.*


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

*설명이 아직 없습니다.*


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

*설명이 아직 없습니다.*


#### SetInt

```csharp
public void SetInt(string key, int value)
```

*설명이 아직 없습니다.*


#### SetLong

```csharp
public void SetLong(string key, long value)
```

*설명이 아직 없습니다.*


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

*설명이 아직 없습니다.*


#### SetString

```csharp
public void SetString(string key, string value)
```

*설명이 아직 없습니다.*


#### HasKey

```csharp
public bool HasKey(string key)
```

*설명이 아직 없습니다.*


#### Remove

```csharp
public bool Remove(string key)
```

Remove an override so subsequent reads use their fallback. Call Save to persist.


#### Reset

```csharp
public int Reset()
```

Remove only this mod's options. Returns the number removed. Call Save to persist.


#### Save

```csharp
public bool Save()
```

Flush the shared DeviceOptions file, including pending changes from other mods. Returns false if storage is unavailable, saving is disabled, or the file write fails. Does not save cloud Options or invoke OptionsBinding.OnOptionsChanged.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

*설명이 아직 없습니다.*


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

*설명이 아직 없습니다.*


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

*설명이 아직 없습니다.*


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

*설명이 아직 없습니다.*


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Remove this mod's UI registrations on unload. Saved values are retained.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

UI definitions only; stored values survive removing this page or unregistering a mod.

### 프로퍼티

#### Options

```csharp
public ModOptions Options { get; }
```

*설명이 아직 없습니다.*


#### ModId

```csharp
public string ModId { get; }
```

*설명이 아직 없습니다.*


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

*설명이 아직 없습니다.*


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

*설명이 아직 없습니다.*


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Options

```csharp
public ModOptions Options { get; }
```

*설명이 아직 없습니다.*


#### Key

```csharp
public string Key { get; }
```

*설명이 아직 없습니다.*


#### Label

```csharp
public ModOptionText Label { get; }
```

*설명이 아직 없습니다.*


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

*설명이 아직 없습니다.*

### 생성자

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

*설명이 아직 없습니다.*


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Retain this handle to read the registered default, or change a value and notify listeners.

### 이벤트

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

*설명이 아직 없습니다.*

### 프로퍼티

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

*설명이 아직 없습니다.*


#### Value

```csharp
public T Value { get; set; }
```

*설명이 아직 없습니다.*

### 생성자

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

*설명이 아직 없습니다.*

### 메서드

#### Read

```csharp
protected abstract T Read()
```

*설명이 아직 없습니다.*


#### Write

```csharp
protected abstract void Write(T value)
```

*설명이 아직 없습니다.*


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

*설명이 아직 없습니다.*


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

*설명이 아직 없습니다.*

### 메서드

#### Read

```csharp
protected override bool Read()
```

*설명이 아직 없습니다.*


#### Write

```csharp
protected override void Write(bool value)
```

*설명이 아직 없습니다.*


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

*설명이 아직 없습니다.*


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

*설명이 아직 없습니다.*

### 메서드

#### Read

```csharp
protected override int Read()
```

*설명이 아직 없습니다.*


#### Normalize

```csharp
protected override int Normalize(int value)
```

*설명이 아직 없습니다.*


#### Write

```csharp
protected override void Write(int value)
```

*설명이 아직 없습니다.*


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

*설명이 아직 없습니다.*


#### Min

```csharp
public float Min { get; }
```

*설명이 아직 없습니다.*


#### Max

```csharp
public float Max { get; }
```

*설명이 아직 없습니다.*


#### Step

```csharp
public float Step { get; }
```

*설명이 아직 없습니다.*

### 메서드

#### Read

```csharp
protected override float Read()
```

*설명이 아직 없습니다.*


#### Normalize

```csharp
protected override float Normalize(float value)
```

*설명이 아직 없습니다.*


#### Write

```csharp
protected override void Write(float value)
```

*설명이 아직 없습니다.*


## ModOptionText

```csharp
public readonly struct ModOptionText
```

Plain text by default; pass a LocalizedString to resolve a localization key on display.

### 메서드

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

*설명이 아직 없습니다.*


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

*설명이 아직 없습니다.*


## ModOptionKind

```csharp
public enum ModOptionKind
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Toggle` |  |
| `SelectionBox` |  |
| `Slider` |  |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

*설명이 아직 없습니다.*

### 메서드

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

*설명이 아직 없습니다.*
