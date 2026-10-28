<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# MODオプション

デバイスごとに保存されるMOD別の設定と、ゲームの設定画面にトグル、選択ボックス、スライダーを追加するためのAPIです。`Context.Options` または `HorayModAPI.GetModOptions(modId)` から取得します。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`ModOptions`](#modoptions) | 各MODが設定できるオプション値。 |
| [`ModOptionsPage`](#modoptionspage) | 設定画面における1つのMODのページ。 |
| [`ModOptionDefinition`](#modoptiondefinition) | 設定画面の項目の共通定義。 |
| [`ModOption<T>`](#modoptiont) | 値を保持するオプション項目。 |
| [`ModToggleOption`](#modtoggleoption) | オン/オフのオプション。 |
| [`ModSelectionOption`](#modselectionoption) | 選択ボックスのオプション。 |
| [`ModSliderOption`](#modslideroption) | スライダーのオプション。 |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | 設定画面の行の種類。 |
| [`ModOptionsRegistry`](#modoptionsregistry) | 設定画面が読み取るMODページのリスト。 |


## ModOptions

```csharp
public sealed class ModOptions
```

各MODが設定できるオプション値です。

### プロパティ

#### ModId

```csharp
public string ModId { get; }
```

このオプションセットの modId


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

ストレージが初期化されているかどうか


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

設定画面に表示されるMOD名。SteamワークショップのMODでは、既定でマニフェストの表示名になります。

### メソッド

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

保存されている bool 値。存在しないか型が異なる場合は fallback を返します。


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

保存されている int 値。存在しないか型が異なる場合は fallback を返します。


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

保存されている long 値。存在しないか型が異なる場合は fallback を返します。


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

保存されている float 値。存在しないか型が異なる場合は fallback を返します。


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

保存されている string 値。存在しないか型が異なる場合は fallback を返します。


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

bool 値をメモリに書き込みます。


#### SetInt

```csharp
public void SetInt(string key, int value)
```

int 値をメモリに書き込みます。


#### SetLong

```csharp
public void SetLong(string key, long value)
```

long 値をメモリに書き込みます。


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

float 値をメモリに書き込みます。


#### SetString

```csharp
public void SetString(string key, string value)
```

string 値をメモリに書き込みます。null は受け付けません。


#### HasKey

```csharp
public bool HasKey(string key)
```

key に値が保存されているかどうか


#### Remove

```csharp
public bool Remove(string key)
```

保存されている値を削除し、以降の読み取りでは fallback が返るようにします。


#### Reset

```csharp
public int Reset()
```

このMODのすべてのオプションを消去します。削除した数を返します。


#### Save

```csharp
public bool Save()
```

オプションファイル(DeviceOptions)を保存します。Set*/Remove/Reset はメモリを変更するだけなので、ファイルに書き込むにはこれを呼び出してください。ストレージがない場合や書き込みに失敗した場合は false を返します。


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

設定画面のこのMODのページにトグルを追加します。同じ key の項目があれば置き換えます。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

選択ボックスを追加します。値は choices のインデックスです。


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

選択肢をローカライズキーで指定する選択ボックスを追加します。


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

スライダーを追加します。step が 0 の場合、値は連続値になります。


#### UnregisterUI

```csharp
public void UnregisterUI()
```

このMODの設定画面の項目をすべて削除します。保存された値は残ります。


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

設定画面における1つのMODのページです。UIの定義だけを保持し、保存された値は別に管理されます。

### プロパティ

#### Options

```csharp
public ModOptions Options { get; }
```

このページのMODオプション


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

設定画面に表示されるMOD名


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

登録順の項目


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

設定画面の項目の共通定義です。`ModOptions.Add*` によって生成されます。

### プロパティ

#### Options

```csharp
public ModOptions Options { get; }
```

この項目が属するMODオプション


#### Key

```csharp
public string Key { get; }
```

ストレージのキー


#### Label

```csharp
public ModOptionText Label { get; }
```

表示名。空の場合はキーが表示されます。


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

行の種類

### コンストラクター

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

key を検証します。


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

値を保持するオプション項目です。コードから値を読み書きするには、このハンドルを保持しておいてください。

### イベント

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

値が変わった後、新しい値を引数に呼ばれます。

### プロパティ

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

登録時に指定した既定値


#### Value

```csharp
public T Value { get; set; }
```

現在の値。設定するとメモリに値が保存され、変化があれば `ValueChanged` が発生します。

### コンストラクター

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged は `ValueChanged` の最初の購読者になります。

### メソッド

#### Read

```csharp
protected abstract T Read()
```

ストレージから値を読み取ります。


#### Write

```csharp
protected abstract void Write(T value)
```

ストレージのメモリに値を書き込みます。


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

書き込む前に、値を許容範囲に収めます。


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

オン/オフのオプションです。

### プロパティ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

常に Toggle

### メソッド

#### Read

```csharp
protected override bool Read()
```

bool 値を読み取ります。


#### Write

```csharp
protected override void Write(bool value)
```

bool 値を書き込みます。


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

選択ボックスのオプションです。値は `Choices` のインデックスです。

### プロパティ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

常に SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

選択肢のリスト

### メソッド

#### Read

```csharp
protected override int Read()
```

保存されているインデックス。範囲外の場合は既定値。


#### Normalize

```csharp
protected override int Normalize(int value)
```

選択肢の範囲に収めます。


#### Write

```csharp
protected override void Write(int value)
```

インデックスを書き込みます。


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

スライダーのオプションです。

### プロパティ

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

常に Slider


#### Min

```csharp
public float Min { get; }
```

最小値


#### Max

```csharp
public float Max { get; }
```

最大値


#### Step

```csharp
public float Step { get; }
```

刻み幅。0 なら連続値。

### メソッド

#### Read

```csharp
protected override float Read()
```

保存されている値(正規化済み)


#### Normalize

```csharp
protected override float Normalize(float value)
```

範囲に収め、刻み幅に合わせます。


#### Write

```csharp
protected override void Write(float value)
```

float 値を書き込みます。


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*説明はまだありません。*

### メソッド

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

文字列をそのまま表示します。


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

表示時に LocalizedString のキーを翻訳します。


## ModOptionKind

```csharp
public enum ModOptionKind
```

設定画面の行の種類です。

| 値 | 説明 |
|---|---|
| `Toggle` | オン/オフ |
| `SelectionBox` | 選択ボックス |
| `Slider` | スライダー |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

設定画面が読み取るMODページのリストです。

### メソッド

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

表示するページを modId 順で返します。
