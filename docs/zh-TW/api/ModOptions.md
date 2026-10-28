<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# 模組選項

依裝置儲存的每個模組設定，以及在遊戲選項畫面新增開關、選擇方塊與滑桿的 API。透過 `Context.Options` 或 `HorayModAPI.GetModOptions(modId)` 取得。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`ModOptions`](#modoptions) | 每個模組可設定的選項值。 |
| [`ModOptionsPage`](#modoptionspage) | 選項畫面上一個模組的頁面。 |
| [`ModOptionDefinition`](#modoptiondefinition) | 選項畫面項目的共通定義。 |
| [`ModOption<T>`](#modoptiont) | 持有值的選項項目。 |
| [`ModToggleOption`](#modtoggleoption) | 開／關選項。 |
| [`ModSelectionOption`](#modselectionoption) | 選擇方塊選項。 |
| [`ModSliderOption`](#modslideroption) | 滑桿選項。 |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | 選項畫面上的列類型。 |
| [`ModOptionsRegistry`](#modoptionsregistry) | 選項畫面讀取的模組頁面清單。 |


## ModOptions

```csharp
public sealed class ModOptions
```

每個模組可設定的選項值。

### 屬性

#### ModId

```csharp
public string ModId { get; }
```

此選項集的 modId


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

儲存空間是否已初始化


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

選項畫面上顯示的模組名稱。Steam 工作坊模組預設為資訊清單的顯示名稱。

### 方法

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

已儲存的 bool 值。若不存在或型別不同則回傳 fallback。


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

已儲存的 int 值。若不存在或型別不同則回傳 fallback。


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

已儲存的 long 值。若不存在或型別不同則回傳 fallback。


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

已儲存的 float 值。若不存在或型別不同則回傳 fallback。


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

已儲存的 string 值。若不存在或型別不同則回傳 fallback。


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

將 bool 值寫入記憶體。


#### SetInt

```csharp
public void SetInt(string key, int value)
```

將 int 值寫入記憶體。


#### SetLong

```csharp
public void SetLong(string key, long value)
```

將 long 值寫入記憶體。


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

將 float 值寫入記憶體。


#### SetString

```csharp
public void SetString(string key, string value)
```

將 string 值寫入記憶體。不接受 null。


#### HasKey

```csharp
public bool HasKey(string key)
```

key 底下是否有已儲存的值


#### Remove

```csharp
public bool Remove(string key)
```

移除已儲存的值，之後的讀取會回傳 fallback。


#### Reset

```csharp
public int Reset()
```

清除此模組的所有選項。回傳移除的數量。


#### Save

```csharp
public bool Save()
```

儲存選項檔（DeviceOptions）。Set*/Remove/Reset 只會更改記憶體，所以請呼叫此方法將它們寫入檔案。若沒有儲存空間或寫入失敗則回傳 false。


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

在選項畫面上此模組的頁面新增一個開關。會取代任何相同 key 的項目。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

新增一個選擇方塊。值為 choices 的索引。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

新增一個選擇方塊，其選項以在地化鍵提供。


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

新增一個滑桿。若 step 為 0，值為連續值。


#### UnregisterUI

```csharp
public void UnregisterUI()
```

移除此模組在選項畫面上的所有項目。已儲存的值會保留。


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

選項畫面上一個模組的頁面。只持有 UI 定義；已儲存的值另行保存。

### 屬性

#### Options

```csharp
public ModOptions Options { get; }
```

此頁面的模組選項


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

選項畫面上顯示的模組名稱


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

依註冊順序排列的項目


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

選項畫面項目的共通定義。由 `ModOptions.Add*` 建立。

### 屬性

#### Options

```csharp
public ModOptions Options { get; }
```

此項目所屬的模組選項


#### Key

```csharp
public string Key { get; }
```

儲存鍵


#### Label

```csharp
public ModOptionText Label { get; }
```

顯示名稱。若為空，則顯示 key。


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

列類型

### 建構函式

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

驗證 key。


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

持有值的選項項目。保留這個控制代碼，即可從程式碼讀取與更改值。

### 事件

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

值改變後以新值呼叫。

### 屬性

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

註冊時給定的預設值


#### Value

```csharp
public T Value { get; set; }
```

目前的值。設定它會將值存入記憶體，若值有改變則觸發 `ValueChanged`。

### 建構函式

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged 會成為 `ValueChanged` 的第一個訂閱者。

### 方法

#### Read

```csharp
protected abstract T Read()
```

從儲存空間讀取值。


#### Write

```csharp
protected abstract void Write(T value)
```

將值寫入儲存記憶體。


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

寫入前將值限制在允許的範圍內。


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

開／關選項。

### 屬性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

一律為 Toggle

### 方法

#### Read

```csharp
protected override bool Read()
```

讀取 bool 值。


#### Write

```csharp
protected override void Write(bool value)
```

寫入 bool 值。


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

選擇方塊選項。值為 `Choices` 的索引。

### 屬性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

一律為 SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

選項清單

### 方法

#### Read

```csharp
protected override int Read()
```

已儲存的索引。若超出範圍則為預設值。


#### Normalize

```csharp
protected override int Normalize(int value)
```

限制在選項範圍內。


#### Write

```csharp
protected override void Write(int value)
```

寫入索引。


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

滑桿選項。

### 屬性

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

一律為 Slider


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

步進大小。為 0 時為連續值。

### 方法

#### Read

```csharp
protected override float Read()
```

已儲存的值，經正規化


#### Normalize

```csharp
protected override float Normalize(float value)
```

限制在範圍內並對齊到步進。


#### Write

```csharp
protected override void Write(float value)
```

寫入 float 值。


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*尚無說明。*

### 方法

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

原樣顯示該字串。


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

在顯示時翻譯 LocalizedString 的鍵。


## ModOptionKind

```csharp
public enum ModOptionKind
```

選項畫面上的列類型。

| 值 | 說明 |
|---|---|
| `Toggle` | 開／關 |
| `SelectionBox` | 選擇方塊 |
| `Slider` | 滑桿 |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

選項畫面讀取的模組頁面清單。

### 方法

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

回傳要顯示的頁面，依 modId 排序。
