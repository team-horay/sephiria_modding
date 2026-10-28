# コスチュームMOD

コードを書かず、PNGとJSONファイルだけでコスチュームを追加してみましょう。この種のMODでは、次の2つを追加できます。

- **新しいコスチューム**: まったく新しいコスチュームを作成します。見た目、ステータス、初期アイテムなどを設定できます。
- **既存コスチュームの外見**: ピンク色のウサギなど、既存のコスチュームに外見(スキン)を追加します。

このページは [`examples/costume-sample`](../../examples/costume-sample) のサンプルをもとにしています。ダウンロードして、MODの構造をつかんでください。

## MODの構造

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← コスチュームフォルダ
│   └── WhiteCat/              ← フォルダ1つ = コスチューム1つ
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← その他のモーションPNG
└── Localization/              ← コスチュームの名前と説明のテキスト
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

サンプルをもとに自分のMODを作るときは、`metadata.json` の `modId` と `author` を変更してください。(詳細は [`metadata.json`](metadata.md) を参照。)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## スプライトアニメーション

- コスチュームは、すべて含めるとかなりの数のアニメーションが必要です。
- はじめてのコスチュームなら、まずは `idle, move, airborne, fall` だけのシンプルなものを作ってみてください。

| モーション | 説明 | 必須 |
|---|---|---|
| `idle` | 待機 | 必須 |
| `move` | 移動 | 必須 |
| `airborne` | 空中 | 任意 |
| `fall` | 倒れる | 任意 |
| `attack` | 攻撃 | 任意 |
| `greatsword_lower` | 回転振り | 任意 |
| `greatsword_sweep` | 回転 | 任意 |
| `hitfeedback` | 被弾 | 任意 |

### 後ろ向きのアニメーション

- より自然なコスチュームにするために、後ろ向きのスプライトを描くこともできます。これは任意です。
- 後ろ向きのスプライトを追加する場合は、`idle_back, move_back` のモーションを必ず含めてください。
- 後ろ向きのアニメーションは次のように定義します。

| モーション | 説明 |
|---|---|
| `idle_back` | 待機(後ろ) |
| `move_back` | 移動(後ろ) |
| `attack_back` | 攻撃(後ろ) |
| `greatsword_upper` | 回転振り(後ろ) |

## 絵の準備

- モーションごとに、そのモーションのフレームを横に並べたPNGスプライトシートを作成します。
- 画像の高さはフレームの高さと同じで、幅は フレームの幅 × フレーム数 です。フレームの間に隙間を空けないでください。
- 個々の画像はすべて同じサイズにすることをおすすめします。
- 各スプライトが 20×16 の白猫のサンプルでは、画像サイズは次のとおりです。

| ファイル | サイズ | フレーム数 |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*画像の例*


### 新しいコスチュームの情報を書く

`costume.json` ファイルを書いて、自分のコスチュームを定義します。

`Costumes/WhiteCat/costume.json` の例:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| フィールド | 必須 | 説明 |
|---|---|---|
| `schemaVersion` | はい | 形式の識別子です。整数 `1` を入力します。 |
| `id` | はい | 小文字の英字、数字、アンダースコア、ハイフンで1〜31文字。衝突を避けるため、ゲーム内では `<modId>:<id>` として登録されます。 |
| `name` | はい | コスチューム名の**ローカライズキー**です。下の [ローカライズファイルの準備](#ローカライズファイルの準備) を参照してください。 |
| `flavorText` | いいえ | コスチュームの説明の**ローカライズキー**です。下の [ローカライズファイルの準備](#ローカライズファイルの準備) を参照してください。 |
| `icon` | はい | コスチュームのプレビューアイコンPNGへのパスです。 |
| `face` | いいえ | マルチプレイHUD用の顔PNGへのパスです。 |
| `pixelsPerUnit` | いいえ | より細かい絵を使いたい場合に変更します。値が大きいほど絵が細かく描画されます。既定のPPUは16です。 |
| `frameSize` | いいえ | 各スプライトの `[幅, 高さ]` です。既定値: `[16, 16]`。 |
| `pivot` | いいえ | 各スプライトの基準点です。左下が `[0, 0]` です。既定値: `[幅/2, 0]`。 |
| `animations` | はい | モーションごとの絵です。[animations](#animations) を参照してください。 |
| `stats` | いいえ | 設定するステータスのリストです。[ステータス一覧](stats.md) を参照してください。 |
| `startingItems` | いいえ | 初期アイテムIDの配列です。[アイテム一覧](item-table.md) を参照してください。 |
| `body` | いいえ | 当たり判定を設定します。[body](#body) を参照してください。 |
| `skins` | いいえ | 同じコスチュームの追加の外見です。[skins](#skins) を参照してください。 |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- 上で準備したアニメーションを、`{ "file", "fps", "loop", "footstepFrames" }` の4つのプロパティで定義します。
- モーションごとに `frameSize` と `pivot` を個別に設定することもできます。
- 最後の行のように、1つのモーションだけ別のサイズで描いた場合は、そのモーションに `frameSize` と `pivot` を設定します。

#### プロパティ

| フィールド | 必須 | 説明 |
|---|---|---|
| `file` | はい | 再生するファイル名です。 |
| `fps` | いいえ | アニメーションの再生速度です。(既定値: 12) |
| `loop` | いいえ | アニメーションをループするかどうかです。(既定値: true。攻撃アニメーションはfalse) |
| `footstepFrames` | いいえ | 0から始まるフレーム番号です。そのフレームが表示されたときに足音が鳴ります。 |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- キャラクターの移動判定と被弾判定の範囲を定義します。
- 内蔵コスチュームはこの値を既定値として使っています。
- 特別な理由がない限り、省略することをおすすめします。

#### プロパティ

| フィールド | 必須 | 説明 |
|---|---|---|
| `hitboxRadius` | いいえ | 当たり判定の円の半径(ピクセル)、0.5〜64。既定値 `4`。 |
| `hitboxOffset` | いいえ | 当たり判定の円の中心 `[x, y]`。基準点からの相対位置(ピクセル)で、xの正は右、yの正は上、±64。既定値 `[0, 4]`。 |
| `movementRadius` | いいえ | 壁や障害物と衝突する移動判定の円の半径。単位(16ピクセル = 1)、0.05〜2。既定値 `0.25`。 |
| `bodyScale` | いいえ | 武器を持つ肩の位置のスケール、0.1〜4。既定値 `1`。大きなコスチュームで武器が体に埋もれるときに上げてください。 |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- 同じコスチュームに追加の外見を加えます。
- アニメーションは上と同じ形式で記述します。
- `frameSize` や `pivot` を省略すると、コスチューム自身の値が使われます。




## ローカライズファイルの準備

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- `name` と `flavorText` にはテキストではなく**ローカライズキー**を書き、テキストは同じMODの `Localization/` フォルダに言語ごとに1ファイルずつ置きます。
- キーには英字、数字、アンダースコア、ハイフンを使い、スペースは入れないでください。MODの名前を含めると、他のMODとの衝突を避けやすくなります。
- キーが**どの言語ファイルにもない**場合、コスチュームはロードされません。少なくとも1つの言語を用意する必要があります。
- より多くの人にコスチュームを楽しんでもらえるように、英語(`en-US`)も含めることをおすすめします。
- `metadata.json` の `modules` に `localization` モジュールを宣言するのも忘れないでください。



## 動作確認

1. MODフォルダを `WorkshopLocal/` に置いてゲームを起動します。
2. ゲームを開始し、村でコスチューム選択(**衣装変更**)を開いて、新しいコスチュームが追加されていることを確認します。
3. コスチュームの内容を変更した場合は、ゲームの再起動が必要です。



## 補足: 既存のコスチュームにスキンだけを追加する

`costume.json` の代わりに `skin.json` を作ると、既存のコスチュームの外見を作成できます。

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume` は対象コスチュームのidです。内蔵コスチュームのid(`PinkRabbit` など)か、他のMODのコスチュームならMOD名を含む実行時のidを使います。(白猫のサンプルなら `yourname.costume-whitecat:whitecat`。)
- `stats`、`startingItems`、`body`、`skins`、`flavorText` は既存のコスチュームのものがそのまま使われるため、設定できません。
- 内蔵コスチュームの既定の外見を上書きすることはできません。
