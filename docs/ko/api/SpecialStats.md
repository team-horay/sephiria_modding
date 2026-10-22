<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 전문 능력치

`HorayModAPI.OnLoadSpecialStatDatabase`에서 `RegisterSpecialStatCategory`·`RegisterSpecialStat`으로 등록하는 스탯 표시 타입입니다.

게임 버전 **1.0.34** 기준, 2026-10-22 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) |  |
| [`SpecialStatValue`](#specialstatvalue) |  |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) |  |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) |  |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) |  |
| [`SpecialStatValueSource`](#specialstatvaluesource) |  |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) |  |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Reads a display value without applying or creating a StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

*설명이 아직 없습니다.*


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

*설명이 아직 없습니다.*


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

*설명이 아직 없습니다.*

### 생성자

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

*설명이 아직 없습니다.*


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

*설명이 아직 없습니다.*

### 상수와 필드

#### text

```csharp
public string text
```

*설명이 아직 없습니다.*


#### hasStat

```csharp
public bool hasStat
```

*설명이 아직 없습니다.*


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

*설명이 아직 없습니다.*


#### hasNumericValue

```csharp
public bool hasNumericValue
```

*설명이 아직 없습니다.*


#### numericValue

```csharp
public double numericValue
```

*설명이 아직 없습니다.*


#### numberFormat

```csharp
public string numberFormat
```

*설명이 아직 없습니다.*


#### symbolKey

```csharp
public string symbolKey
```

*설명이 아직 없습니다.*


#### symbol

```csharp
public string symbol
```

*설명이 아직 없습니다.*


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

*설명이 아직 없습니다.*

### 메서드

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

*설명이 아직 없습니다.*


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

*설명이 아직 없습니다.*

### 상수와 필드

#### id

```csharp
public string id
```

*설명이 아직 없습니다.*


#### enabled

```csharp
public bool enabled
```

*설명이 아직 없습니다.*


#### displayName

```csharp
public LocalizedString displayName
```

*설명이 아직 없습니다.*


#### fallbackName

```csharp
public string fallbackName
```

*설명이 아직 없습니다.*


#### headerColor

```csharp
public Color headerColor
```

*설명이 아직 없습니다.*


#### order

```csharp
public int order
```

*설명이 아직 없습니다.*


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

*설명이 아직 없습니다.*


#### requireWeapon

```csharp
public bool requireWeapon
```

*설명이 아직 없습니다.*


#### weaponType

```csharp
public EWeaponType weaponType
```

*설명이 아직 없습니다.*

### 프로퍼티

#### DisplayName

```csharp
public string DisplayName { get; }
```

*설명이 아직 없습니다.*


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Always` |  |
| `WhenAnyStatHasValue` |  |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

*설명이 아직 없습니다.*

### 상수와 필드

#### enabled

```csharp
public bool enabled
```

*설명이 아직 없습니다.*


#### category

```csharp
public SpecialStatCategoryEntity category
```

*설명이 아직 없습니다.*


#### order

```csharp
public int order
```

*설명이 아직 없습니다.*


#### showWhenZero

```csharp
public bool showWhenZero
```

*설명이 아직 없습니다.*


#### labelOverride

```csharp
public LocalizedString labelOverride
```

*설명이 아직 없습니다.*


#### fallbackLabel

```csharp
public string fallbackLabel
```

*설명이 아직 없습니다.*


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

*설명이 아직 없습니다.*


#### labelColor

```csharp
public Color labelColor
```

*설명이 아직 없습니다.*


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

*설명이 아직 없습니다.*


#### valueId

```csharp
public string valueId
```

*설명이 아직 없습니다.*


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

*설명이 아직 없습니다.*


#### divideForDisplay

```csharp
public float divideForDisplay
```

*설명이 아직 없습니다.*


#### displayOffset

```csharp
public float displayOffset
```

*설명이 아직 없습니다.*


#### numberFormat

```csharp
public string numberFormat
```

*설명이 아직 없습니다.*


#### includeSign

```csharp
public bool includeSign
```

*설명이 아직 없습니다.*


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `StatHook` |  |
| `CustomStat` |  |
| `Provider` |  |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Default` |  |
| `Keyword` |  |
| `Override` |  |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Reads a display value without applying or creating a StatusInstance.

### 메서드

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

*설명이 아직 없습니다.*
