<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# 模组选项

用于按设备保存各模组设置、并向游戏选项画面添加开关、选择框和滑块的 API。通过 `Context.Options` 或 `HorayModAPI.GetModOptions(modId)` 获取。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`ModOptions`](#modoptions) | 各模组可以设置的选项值。 |
| [`ModOptionsPage`](#modoptionspage) | 选项画面上一个模组的页面。 |
| [`ModOptionDefinition`](#modoptiondefinition) | 选项画面条目的通用定义。 |
| [`ModOption<T>`](#modoptiont) | 持有值的选项条目。 |
| [`ModToggleOption`](#modtoggleoption) | 开/关选项。 |
| [`ModSelectionOption`](#modselectionoption) | 选择框选项。 |
| [`ModSliderOption`](#modslideroption) | 滑块选项。 |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | 选项画面上的行类型。 |
| [`ModOptionsRegistry`](#modoptionsregistry) | 选项画面读取的模组页面列表。 |


## ModOptions

```csharp
public sealed class ModOptions
```

各模组可以设置的选项值。

### 属性

#### ModId

```csharp
public string ModId { get; }
```

此选项集的 modId


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

存储是否已初始化


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

选项画面上显示的模组名称。Steam 创意工坊模组默认使用清单中的显示名称。

### 方法

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

已保存的 bool 值。缺失或类型不同时返回 fallback。


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

已保存的 int 值。缺失或类型不同时返回 fallback。


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

已保存的 long 值。缺失或类型不同时返回 fallback。


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

已保存的 float 值。缺失或类型不同时返回 fallback。


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

已保存的 string 值。缺失或类型不同时返回 fallback。


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

把 bool 值写入内存。


#### SetInt

```csharp
public void SetInt(string key, int value)
```

把 int 值写入内存。


#### SetLong

```csharp
public void SetLong(string key, long value)
```

把 long 值写入内存。


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

把 float 值写入内存。


#### SetString

```csharp
public void SetString(string key, string value)
```

把 string 值写入内存。不接受 null。


#### HasKey

```csharp
public bool HasKey(string key)
```

key 下是否保存有值


#### Remove

```csharp
public bool Remove(string key)
```

移除已保存的值，之后的读取将返回 fallback。


#### Reset

```csharp
public int Reset()
```

清除此模组的所有选项。返回移除的数量。


#### Save

```csharp
public bool Save()
```

保存选项文件（DeviceOptions）。Set*/Remove/Reset 只修改内存，所以需要调用此方法才能写入文件。没有存储或写入失败时返回 false。


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

在选项画面上此模组的页面中添加一个开关。会替换具有相同 key 的条目。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

添加一个选择框。值为 choices 中的索引。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

添加一个选择框，其选项以本地化键给出。


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

添加一个滑块。step 为 0 时，值是连续的。


#### UnregisterUI

```csharp
public void UnregisterUI()
```

移除此模组在选项画面上的所有条目。已保存的值保留。


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

选项画面上一个模组的页面。只持有 UI 定义；已保存的值另行保管。

### 属性

#### Options

```csharp
public ModOptions Options { get; }
```

此页面的模组选项


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

选项画面上显示的模组名称


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

按注册顺序排列的条目


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

选项画面条目的通用定义。由 `ModOptions.Add*` 创建。

### 属性

#### Options

```csharp
public ModOptions Options { get; }
```

此条目所属的模组选项


#### Key

```csharp
public string Key { get; }
```

存储键


#### Label

```csharp
public ModOptionText Label { get; }
```

显示名称。为空时显示 key。


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

行的类型

### 构造函数

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

验证 key。


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

持有值的选项条目。保留其句柄即可从代码中读取和修改值。

### 事件

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

值发生变化后，以新值调用。

### 属性

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

注册时给定的默认值


#### Value

```csharp
public T Value { get; set; }
```

当前值。设置它会把值存入内存，若值发生了变化则触发 `ValueChanged`。

### 构造函数

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged 成为 `ValueChanged` 的第一个订阅者。

### 方法

#### Read

```csharp
protected abstract T Read()
```

从存储中读取值。


#### Write

```csharp
protected abstract void Write(T value)
```

把值写入存储内存。


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

写入前把值限制在允许的范围内。


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

开/关选项。

### 属性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

始终为 Toggle

### 方法

#### Read

```csharp
protected override bool Read()
```

读取 bool 值。


#### Write

```csharp
protected override void Write(bool value)
```

写入 bool 值。


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

选择框选项。值为 `Choices` 中的索引。

### 属性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

始终为 SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

选项列表

### 方法

#### Read

```csharp
protected override int Read()
```

已保存的索引。超出范围时为默认值。


#### Normalize

```csharp
protected override int Normalize(int value)
```

限制在选项范围内。


#### Write

```csharp
protected override void Write(int value)
```

写入索引。


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

滑块选项。

### 属性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

始终为 Slider


#### Min

```csharp
public float Min { get; }
```

最小值


#### Max

```csharp
public float Max { get; }
```

最大值


#### Step

```csharp
public float Step { get; }
```

步长。为 0 时连续。

### 方法

#### Read

```csharp
protected override float Read()
```

已保存的值，经过规范化


#### Normalize

```csharp
protected override float Normalize(float value)
```

限制在范围内并对齐到步长。


#### Write

```csharp
protected override void Write(float value)
```

写入 float 值。


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*暂无说明。*

### 方法

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

原样显示字符串。


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

在显示时翻译 LocalizedString 的键。


## ModOptionKind

```csharp
public enum ModOptionKind
```

选项画面上的行类型。

| 值 | 说明 |
|---|---|
| `Toggle` | 开/关 |
| `SelectionBox` | 选择框 |
| `Slider` | 滑块 |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

选项画面读取的模组页面列表。

### 方法

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

返回要显示的页面，按 modId 排序。
