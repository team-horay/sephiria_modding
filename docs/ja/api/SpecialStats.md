<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# 専門ステータス

`HorayModAPI.OnLoadSpecialStatDatabase` で `RegisterSpecialStatCategory` と `RegisterSpecialStat` を使って登録するステータス表示の型です。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | 値を読み取る関数に渡される対象アバターの情報。 |
| [`SpecialStatValue`](#specialstatvalue) | 専門ステータス1行分の値。 |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | ステータスUIの専門ステータスカテゴリのデータ。 |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | カテゴリをいつ表示するかを示します。 |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | `StatusEntity.specialStat` の詳細データ。 |
| [`SpecialStatValueSource`](#specialstatvaluesource) | 専門ステータスの表示値をどこから読み取るかを示します。 |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | 専門ステータス行の名前の色。 |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | StatusInstance なしで表示値だけを読み取るアセット。 |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

値を読み取る関数に渡される対象アバターの情報です。

### プロパティ

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

対象ユニット


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

対象の AvatarStatsHooker。ない場合は null。


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

対象の武器コントローラー。ない場合は null。

### コンストラクター

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

hooker または weapons を省略すると、avatar から検索されます。


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

専門ステータス1行分の値です。数値の値は `Number` で作成します。

### 定数とフィールド

#### text

```csharp
public string text
```

表示文字列


#### hasStat

```csharp
public bool hasStat
```

値があるかどうか


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

増加/減少の色


#### hasNumericValue

```csharp
public bool hasNumericValue
```

`numericValue` が有効かどうか


#### numericValue

```csharp
public double numericValue
```

表示用の数値


#### numberFormat

```csharp
public string numberFormat
```

数値の表示形式


#### symbolKey

```csharp
public string symbolKey
```

単位記号のローカライズキー


#### symbol

```csharp
public string symbol
```

単位記号の文字列


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

ツールチップのキーワード置換値

### メソッド

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

数値の値を作成します。


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

ステータスUIの専門ステータスカテゴリのデータです。

### 定数とフィールド

#### id

```csharp
public string id
```

識別子


#### enabled

```csharp
public bool enabled
```

カテゴリを表示するかどうか


#### displayName

```csharp
public LocalizedString displayName
```

タイトルのローカライズキー


#### fallbackName

```csharp
public string fallbackName
```

翻訳がない場合に使われるタイトル


#### headerColor

```csharp
public Color headerColor
```

タイトルの色


#### order

```csharp
public int order
```

並び順


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

表示条件


#### requireWeapon

```csharp
public bool requireWeapon
```

特定の武器を装備しているときだけ表示するかどうか


#### weaponType

```csharp
public EWeaponType weaponType
```

`requireWeapon` が設定されているときに必要な武器の種類

### プロパティ

#### DisplayName

```csharp
public string DisplayName { get; }
```

翻訳されたタイトル


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

カテゴリをいつ表示するかを示します。

| 値 | 説明 |
|---|---|
| `Always` | 常に表示 |
| `WhenAnyStatHasValue` | 少なくとも1行に値があるときだけ表示 |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

`StatusEntity.specialStat` の詳細データです。このステータスをステータスUIと探検記録UIでどのように表示するかを決めます。

### 定数とフィールド

#### enabled

```csharp
public bool enabled
```

パネルに表示するかどうか


#### category

```csharp
public SpecialStatCategoryEntity category
```

所属するカテゴリ(必須)


#### order

```csharp
public int order
```

カテゴリ内での並び順


#### showWhenZero

```csharp
public bool showWhenZero
```

値がないときにも行を表示するかどうか


#### labelOverride

```csharp
public LocalizedString labelOverride
```

専門ステータスパネルでのみ使う名前のローカライズキー


#### fallbackLabel

```csharp
public string fallbackLabel
```

翻訳がない場合に使われる名前


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

名前の色の選択


#### labelColor

```csharp
public Color labelColor
```

`SpecialStatLabelColor.Override` のときの名前の色


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

値の読み取り方法


#### valueId

```csharp
public string valueId
```

StatHook の検索IDまたはカスタムステータスのキー。空なら StatusEntity.id が使われます。


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

`SpecialStatValueSource.Provider` のときに値を読み取るアセット


#### divideForDisplay

```csharp
public float divideForDisplay
```

表示用の除数


#### displayOffset

```csharp
public float displayOffset
```

表示用に加算するオフセット


#### numberFormat

```csharp
public string numberFormat
```

数値の表示形式


#### includeSign

```csharp
public bool includeSign
```

符号を表示するかどうか


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

専門ステータスの表示値をどこから読み取るかを示します。

| 値 | 説明 |
|---|---|
| `StatHook` | `AvatarStatsHooker.HookStat(valueId)` の結果 |
| `CustomStat` | `UnitAvatar.GetCustomStat(valueId)` の値 |
| `Provider` | `valueProvider` アセットの `Read` の結果 |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

専門ステータス行の名前の色です。

| 値 | 説明 |
|---|---|
| `Default` | 白 |
| `Keyword` | 関連するキーワードの色 |
| `Override` | `labelColor` の値 |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

StatusInstance なしで表示値だけを読み取るアセットです。

### メソッド

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

status の現在の表示値を読み取ります。
