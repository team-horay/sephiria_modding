<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# 专门属性

在 `HorayModAPI.OnLoadSpecialStatDatabase` 中通过 `RegisterSpecialStatCategory` 和 `RegisterSpecialStat` 注册的属性显示类型。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | 传给值读取函数的目标角色信息。 |
| [`SpecialStatValue`](#specialstatvalue) | 一行专门属性的值。 |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | 属性 UI 中专门属性分类的数据。 |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | 标识分类何时显示。 |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | `StatusEntity.specialStat` 的详细数据。 |
| [`SpecialStatValueSource`](#specialstatvaluesource) | 标识专门属性的显示值从何处读取。 |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | 专门属性行名称的颜色。 |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | 不依赖 StatusInstance、只读取显示值的资产。 |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

传给值读取函数的目标角色信息。

### 属性

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

目标单位


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

目标的 AvatarStatsHooker。没有时为 null。


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

目标的武器控制器。没有时为 null。

### 构造函数

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

若省略 hooker 或 weapons，则从 avatar 中查找。


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

一行专门属性的值。数值类型的值请用 `Number` 创建。

### 常量与字段

#### text

```csharp
public string text
```

显示字符串


#### hasStat

```csharp
public bool hasStat
```

是否有值


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

增加/减少的颜色


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue` 是否有效


#### numericValue

```csharp
public double numericValue
```

用于显示的数字


#### numberFormat

```csharp
public string numberFormat
```

数字显示格式


#### symbolKey

```csharp
public string symbolKey
```

单位符号的本地化键


#### symbol

```csharp
public string symbol
```

单位符号字符串


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

提示框的关键字替换值

### 方法

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

创建一个数值类型的值。


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

属性 UI 中专门属性分类的数据。

### 常量与字段

#### id

```csharp
public string id
```

标识符


#### enabled

```csharp
public bool enabled
```

是否显示该分类


#### displayName

```csharp
public LocalizedString displayName
```

标题的本地化键


#### fallbackName

```csharp
public string fallbackName
```

没有翻译时使用的标题


#### headerColor

```csharp
public Color headerColor
```

标题颜色


#### order

```csharp
public int order
```

排序顺序


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

显示条件


#### requireWeapon

```csharp
public bool requireWeapon
```

是否仅在装备特定武器时显示


#### weaponType

```csharp
public EWeaponType weaponType
```

设置了 `requireWeapon` 时所需的武器类型

### 属性

#### DisplayName

```csharp
public string DisplayName { get; }
```

翻译后的标题


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

标识分类何时显示。

| 值 | 说明 |
|---|---|
| `Always` | 始终显示 |
| `WhenAnyStatHasValue` | 仅当至少有一行有值时显示 |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

`StatusEntity.specialStat` 的详细数据。决定该属性在属性 UI 和探险记录 UI 中如何显示。

### 常量与字段

#### enabled

```csharp
public bool enabled
```

是否在面板中显示


#### category

```csharp
public SpecialStatCategoryEntity category
```

所属分类（必需）


#### order

```csharp
public int order
```

分类内的排序顺序


#### showWhenZero

```csharp
public bool showWhenZero
```

没有值时是否仍显示该行


#### labelOverride

```csharp
public LocalizedString labelOverride
```

仅在专门属性面板中使用的名称的本地化键


#### fallbackLabel

```csharp
public string fallbackLabel
```

没有翻译时使用的名称


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

名称颜色的选择方式


#### labelColor

```csharp
public Color labelColor
```

`SpecialStatLabelColor.Override` 时的名称颜色


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

值的读取方式


#### valueId

```csharp
public string valueId
```

StatHook 查找 ID 或自定义属性键。为空时使用 StatusEntity.id。


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

`SpecialStatValueSource.Provider` 时用于读取值的资产


#### divideForDisplay

```csharp
public float divideForDisplay
```

显示用的除数


#### displayOffset

```csharp
public float displayOffset
```

显示时附加的偏移量


#### numberFormat

```csharp
public string numberFormat
```

数字显示格式


#### includeSign

```csharp
public bool includeSign
```

是否显示正负号


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

标识专门属性的显示值从何处读取。

| 值 | 说明 |
|---|---|
| `StatHook` | `AvatarStatsHooker.HookStat(valueId)` 的结果 |
| `CustomStat` | `UnitAvatar.GetCustomStat(valueId)` 的值 |
| `Provider` | `valueProvider` 资产的 `Read` 结果 |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

专门属性行名称的颜色。

| 值 | 说明 |
|---|---|
| `Default` | 白色 |
| `Keyword` | 相关关键字的颜色 |
| `Override` | `labelColor` 的值 |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

不依赖 StatusInstance、只读取显示值的资产。

### 方法

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

读取 status 当前的显示值。
