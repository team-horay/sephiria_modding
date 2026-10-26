<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 전문 능력치

`HorayModAPI.OnLoadSpecialStatDatabase`에서 `RegisterSpecialStatCategory`·`RegisterSpecialStat`으로 등록하는 스탯 표시 타입입니다.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | 값 읽기 함수에 넘기는 대상 아바타 정보입니다. |
| [`SpecialStatValue`](#specialstatvalue) | 전문 능력치 한 행의 값입니다. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | 스탯 UI의 전문 능력 항목 카테고리를 데이터입니다. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | 카테고리가 언제 표시되는지 식별하는 값입니다. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | `StatusEntity.specialStat`의 세부 데이터. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | 전문 능력치의 표시 값을 어디서 읽을지 식별하는 값입니다. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | 전문 능력치 행 이름의 색입니다. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | StatusInstance 없이 표시 값만 읽는 에셋입니다. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

값 읽기 함수에 넘기는 대상 아바타 정보입니다.

### 프로퍼티

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

대상 유닛


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

대상의 AvatarStatsHooker입니다. 없으면 null.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

대상의 무기 컨트롤러입니다. 없으면 null.

### 생성자

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

hooker·weapons를 생략하면 avatar에서 찾습니다.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

전문 능력치 한 행의 값입니다. 숫자 값은 `Number`로 만듭니다.

### 상수와 필드

#### text

```csharp
public string text
```

표시 문자열


#### hasStat

```csharp
public bool hasStat
```

값이 있는지


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

증감 색


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue`가 유효한지


#### numericValue

```csharp
public double numericValue
```

표시용 숫자


#### numberFormat

```csharp
public string numberFormat
```

숫자 표시 형식


#### symbolKey

```csharp
public string symbolKey
```

단위의 번역 키


#### symbol

```csharp
public string symbol
```

단위 문자열


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

툴팁의 키워드 치환값

### 메서드

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

숫자 값을 만듭니다.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

스탯 UI의 전문 능력 항목 카테고리를 데이터입니다.

### 상수와 필드

#### id

```csharp
public string id
```

식별자


#### enabled

```csharp
public bool enabled
```

카테고리를 표시할지


#### displayName

```csharp
public LocalizedString displayName
```

제목의 번역 키


#### fallbackName

```csharp
public string fallbackName
```

번역이 없을 때 제목


#### headerColor

```csharp
public Color headerColor
```

제목 색


#### order

```csharp
public int order
```

정렬 순서


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

표시 조건


#### requireWeapon

```csharp
public bool requireWeapon
```

특정 무기를 들었을 때만 표시할지


#### weaponType

```csharp
public EWeaponType weaponType
```

`requireWeapon`일 때 요구하는 무기 종류

### 프로퍼티

#### DisplayName

```csharp
public string DisplayName { get; }
```

번역된 제목


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

카테고리가 언제 표시되는지 식별하는 값입니다.

| 값 | 설명 |
|---|---|
| `Always` | 항상 |
| `WhenAnyStatHasValue` | 값이 있는 행이 하나라도 있을 때만 |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

`StatusEntity.specialStat`의 세부 데이터. 스탯 UI와 런 히스토리 UI에 이 스탯을 어떻게 표시할지 정합니다.

### 상수와 필드

#### enabled

```csharp
public bool enabled
```

패널에 표시할지


#### category

```csharp
public SpecialStatCategoryEntity category
```

소속 카테고리(필수)


#### order

```csharp
public int order
```

카테고리 안 정렬 순서


#### showWhenZero

```csharp
public bool showWhenZero
```

값이 없을 때도 행을 표시할지


#### labelOverride

```csharp
public LocalizedString labelOverride
```

전문 패널 전용 이름의 번역 키


#### fallbackLabel

```csharp
public string fallbackLabel
```

번역이 없을 때 이름


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

이름 색 선택


#### labelColor

```csharp
public Color labelColor
```

`SpecialStatLabelColor.Override`일 때 이름 색


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

값을 읽는 방법


#### valueId

```csharp
public string valueId
```

StatHook 조회 ID 또는 커스텀 스탯 키입니다. 비우면 StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

`SpecialStatValueSource.Provider`일 때 값을 읽는 에셋


#### divideForDisplay

```csharp
public float divideForDisplay
```

표시용 나눗셈


#### displayOffset

```csharp
public float displayOffset
```

표시용 더하기


#### numberFormat

```csharp
public string numberFormat
```

숫자 표시 형식


#### includeSign

```csharp
public bool includeSign
```

부호를 붙여 표시할지


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

전문 능력치의 표시 값을 어디서 읽을지 식별하는 값입니다.

| 값 | 설명 |
|---|---|
| `StatHook` | `AvatarStatsHooker.HookStat(valueId)`의 결과 |
| `CustomStat` | `UnitAvatar.GetCustomStat(valueId)`의 값 |
| `Provider` | `valueProvider` 에셋의 `Read` 결과 |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

전문 능력치 행 이름의 색입니다.

| 값 | 설명 |
|---|---|
| `Default` | 흰색 |
| `Keyword` | 관련 키워드의 색 |
| `Override` | `labelColor` 값 |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

StatusInstance 없이 표시 값만 읽는 에셋입니다.

### 메서드

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

status의 현재 표시 값을 읽습니다.
