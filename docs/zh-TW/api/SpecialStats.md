<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# 專精屬性

在 `HorayModAPI.OnLoadSpecialStatDatabase` 中以 `RegisterSpecialStatCategory` 與 `RegisterSpecialStat` 註冊的能力值顯示型別。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | 傳給讀值函式的目標角色資訊。 |
| [`SpecialStatValue`](#specialstatvalue) | 一列專精屬性的值。 |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | 能力值 UI 中專精屬性分類的資料。 |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | 指定分類何時顯示。 |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | `StatusEntity.specialStat` 的詳細資料。 |
| [`SpecialStatValueSource`](#specialstatvaluesource) | 指定專精屬性的顯示值從何處讀取。 |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | 專精屬性列名稱的顏色。 |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | 不經 StatusInstance、只讀取顯示值的資產。 |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

傳給讀值函式的目標角色資訊。

### 屬性

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

目標單位


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

目標的 AvatarStatsHooker。沒有時為 null。


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

目標的武器控制器。沒有時為 null。

### 建構函式

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

若省略 hooker 或 weapons，會從 avatar 查詢。


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

一列專精屬性的值。以 `Number` 建立數值。

### 常數與欄位

#### text

```csharp
public string text
```

顯示字串


#### hasStat

```csharp
public bool hasStat
```

是否有值


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

增加／減少的顏色


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue` 是否有效


#### numericValue

```csharp
public double numericValue
```

用於顯示的數字


#### numberFormat

```csharp
public string numberFormat
```

數字顯示格式


#### symbolKey

```csharp
public string symbolKey
```

單位符號的在地化鍵


#### symbol

```csharp
public string symbol
```

單位符號字串


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

提示框的關鍵字替換值

### 方法

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

建立一個數值。


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

能力值 UI 中專精屬性分類的資料。

### 常數與欄位

#### id

```csharp
public string id
```

識別碼


#### enabled

```csharp
public bool enabled
```

是否顯示該分類


#### displayName

```csharp
public LocalizedString displayName
```

標題的在地化鍵


#### fallbackName

```csharp
public string fallbackName
```

沒有翻譯時使用的標題


#### headerColor

```csharp
public Color headerColor
```

標題顏色


#### order

```csharp
public int order
```

排序順序


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

顯示條件


#### requireWeapon

```csharp
public bool requireWeapon
```

是否只在裝備特定武器時顯示


#### weaponType

```csharp
public EWeaponType weaponType
```

設定 `requireWeapon` 時所需的武器類型

### 屬性

#### DisplayName

```csharp
public string DisplayName { get; }
```

翻譯後的標題


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

指定分類何時顯示。

| 值 | 說明 |
|---|---|
| `Always` | 一律顯示 |
| `WhenAnyStatHasValue` | 只在至少有一列有值時顯示 |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

`StatusEntity.specialStat` 的詳細資料。決定此能力值在能力值 UI 與遊玩紀錄 UI 中的顯示方式。

### 常數與欄位

#### enabled

```csharp
public bool enabled
```

是否在面板中顯示


#### category

```csharp
public SpecialStatCategoryEntity category
```

所屬分類（必要）


#### order

```csharp
public int order
```

分類內的排序順序


#### showWhenZero

```csharp
public bool showWhenZero
```

沒有值時是否仍顯示該列


#### labelOverride

```csharp
public LocalizedString labelOverride
```

只在專精屬性面板中使用的名稱的在地化鍵


#### fallbackLabel

```csharp
public string fallbackLabel
```

沒有翻譯時使用的名稱


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

名稱顏色的選擇


#### labelColor

```csharp
public Color labelColor
```

`SpecialStatLabelColor.Override` 時的名稱顏色


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

值的讀取方式


#### valueId

```csharp
public string valueId
```

StatHook 查詢 ID 或自訂能力值鍵。若為空，使用 StatusEntity.id。


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

`SpecialStatValueSource.Provider` 時讀取值的資產


#### divideForDisplay

```csharp
public float divideForDisplay
```

顯示用的除數


#### displayOffset

```csharp
public float displayOffset
```

顯示時加上的偏移量


#### numberFormat

```csharp
public string numberFormat
```

數字顯示格式


#### includeSign

```csharp
public bool includeSign
```

是否顯示正負號


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

指定專精屬性的顯示值從何處讀取。

| 值 | 說明 |
|---|---|
| `StatHook` | `AvatarStatsHooker.HookStat(valueId)` 的結果 |
| `CustomStat` | `UnitAvatar.GetCustomStat(valueId)` 的值 |
| `Provider` | `valueProvider` 資產的 `Read` 結果 |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

專精屬性列名稱的顏色。

| 值 | 說明 |
|---|---|
| `Default` | 白色 |
| `Keyword` | 相關關鍵字的顏色 |
| `Override` | `labelColor` 的值 |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

不經 StatusInstance、只讀取顯示值的資產。

### 方法

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

讀取 status 目前的顯示值。
