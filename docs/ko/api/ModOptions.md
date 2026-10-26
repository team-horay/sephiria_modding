<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 모드 옵션

기기별로 저장되는 모드 설정값과, 게임 옵션 화면에 토글·선택 상자·슬라이더를 추가하는 API입니다. `Context.Options` 또는 `HorayModAPI.GetModOptions(modId)`로 얻습니다.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`ModOptions`](#modoptions) | 모드별 지정할 수 있는 옵션 값입니다. |
| [`ModOptionsPage`](#modoptionspage) | 옵션 화면에 표시되는 모드 한 개의 페이지입니다. |
| [`ModOptionDefinition`](#modoptiondefinition) | 옵션 화면 항목의 공통 정의입니다. |
| [`ModOption<T>`](#modoptiont) | 값이 있는 옵션 항목입니다. |
| [`ModToggleOption`](#modtoggleoption) | 켜기/끄기 옵션입니다. |
| [`ModSelectionOption`](#modselectionoption) | 선택 상자 옵션입니다. |
| [`ModSliderOption`](#modslideroption) | 슬라이더 옵션입니다. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | 옵션 화면 행의 종류입니다. |
| [`ModOptionsRegistry`](#modoptionsregistry) | 옵션 화면이 읽는 모드 페이지 목록입니다. |


## ModOptions

```csharp
public sealed class ModOptions
```

모드별 지정할 수 있는 옵션 값입니다.

### 프로퍼티

#### ModId

```csharp
public string ModId { get; }
```

이 옵션 집합의 modId


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

저장소가 초기화돼 있는지


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

옵션 화면에 보이는 모드 이름입니다. 창작마당 모드는 매니페스트 표시 이름이 기본값입니다.

### 메서드

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

저장된 bool 값입니다. 없거나 타입이 다르면 fallback을 돌려줍니다.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

저장된 int 값입니다. 없거나 타입이 다르면 fallback을 돌려줍니다.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

저장된 long 값입니다. 없거나 타입이 다르면 fallback을 돌려줍니다.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

저장된 float 값입니다. 없거나 타입이 다르면 fallback을 돌려줍니다.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

저장된 string 값입니다. 없거나 타입이 다르면 fallback을 돌려줍니다.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

bool 값을 메모리에 씁니다.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

int 값을 메모리에 씁니다.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

long 값을 메모리에 씁니다.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

float 값을 메모리에 씁니다.


#### SetString

```csharp
public void SetString(string key, string value)
```

string 값을 메모리에 씁니다. null은 받지 않습니다.


#### HasKey

```csharp
public bool HasKey(string key)
```

key로 저장된 값이 있는지


#### Remove

```csharp
public bool Remove(string key)
```

저장된 값을 지워 이후 읽기가 fallback을 돌려주게 합니다.


#### Reset

```csharp
public int Reset()
```

이 모드의 옵션을 모두 지웁니다. 지운 개수를 돌려줍니다.


#### Save

```csharp
public bool Save()
```

옵션 파일(DeviceOptions)을 저장합니다. Set*/Remove/Reset은 메모리만 바꾸므로 파일에 반영하려면 이 메서드를 불러야 합니다. 저장소가 없거나 쓰기에 실패하면 false를 돌려줍니다.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

옵션 화면의 이 모드 페이지에 토글을 추가합니다. 같은 key가 있으면 교체합니다.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

선택 상자를 추가합니다. 값은 choices의 인덱스입니다.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

선택지를 번역 키로 주는 선택 상자를 추가합니다.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

슬라이더를 추가합니다. step이 0이면 연속 값입니다.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

이 모드의 옵션 화면 항목을 모두 지웁니다. 저장값은 남습니다.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

옵션 화면에 표시되는 모드 한 개의 페이지입니다. UI 정의만 담으며 저장값은 따로 남습니다.

### 프로퍼티

#### Options

```csharp
public ModOptions Options { get; }
```

이 페이지의 모드 옵션


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

옵션 화면에 보이는 모드 이름


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

등록 순서의 항목


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

옵션 화면 항목의 공통 정의입니다. `ModOptions.Add*`가 만듭니다.

### 프로퍼티

#### Options

```csharp
public ModOptions Options { get; }
```

이 항목이 속한 모드 옵션


#### Key

```csharp
public string Key { get; }
```

저장 키


#### Label

```csharp
public ModOptionText Label { get; }
```

표시 이름입니다. 비우면 key가 표시됩니다.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

행의 종류

### 생성자

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

key를 검증합니다.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

값이 있는 옵션 항목입니다. 핸들을 보관하면 코드에서 값을 읽고 바꿀 수 있습니다.

### 이벤트

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

값이 바뀐 뒤 새 값으로 호출됩니다.

### 프로퍼티

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

등록 시 지정한 기본값


#### Value

```csharp
public T Value { get; set; }
```

현재 값입니다. 쓰면 메모리에 저장하고, 바뀌었으면 `ValueChanged`를 발생시킵니다.

### 생성자

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged는 `ValueChanged`의 첫 구독자가 됩니다.

### 메서드

#### Read

```csharp
protected abstract T Read()
```

저장소에서 값을 읽습니다.


#### Write

```csharp
protected abstract void Write(T value)
```

값을 저장소 메모리에 씁니다.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

쓰기 전에 값을 허용 범위로 맞춥니다.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

켜기/끄기 옵션입니다.

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

항상 Toggle

### 메서드

#### Read

```csharp
protected override bool Read()
```

bool 값을 읽습니다.


#### Write

```csharp
protected override void Write(bool value)
```

bool 값을 씁니다.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

선택 상자 옵션입니다. 값은 `Choices`의 인덱스입니다.

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

항상 SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

선택지 목록

### 메서드

#### Read

```csharp
protected override int Read()
```

저장된 인덱스입니다. 범위 밖이면 기본값.


#### Normalize

```csharp
protected override int Normalize(int value)
```

선택지 범위로 자릅니다.


#### Write

```csharp
protected override void Write(int value)
```

인덱스를 씁니다.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

슬라이더 옵션입니다.

### 프로퍼티

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

항상 Slider


#### Min

```csharp
public float Min { get; }
```

최솟값


#### Max

```csharp
public float Max { get; }
```

최댓값


#### Step

```csharp
public float Step { get; }
```

눈금 간격입니다. 0이면 연속 값.

### 메서드

#### Read

```csharp
protected override float Read()
```

저장값을 정규화한 값


#### Normalize

```csharp
protected override float Normalize(float value)
```

범위로 자르고 눈금에 맞춥니다.


#### Write

```csharp
protected override void Write(float value)
```

float 값을 씁니다.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*설명이 아직 없습니다.*

### 메서드

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

문자열을 그대로 표시합니다.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

LocalizedString의 키를 표시 시점에 번역합니다.


## ModOptionKind

```csharp
public enum ModOptionKind
```

옵션 화면 행의 종류입니다.

| 값 | 설명 |
|---|---|
| `Toggle` | 켜기/끄기 |
| `SelectionBox` | 선택 상자 |
| `Slider` | 슬라이더 |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

옵션 화면이 읽는 모드 페이지 목록입니다.

### 메서드

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

표시할 페이지를 modId 순으로 돌려줍니다.
