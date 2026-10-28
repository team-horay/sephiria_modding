# 服裝模組

讓我們只用 PNG 與 JSON 檔案、不寫任何程式碼來新增服裝。這類模組可以新增以下兩種東西：

- **新服裝**：建立一套全新的服裝。可以設定外觀、能力值、初始物品等。
- **現有服裝的外觀**：為「粉紅兔子」等現有服裝新增外觀（skin）。

本頁以 [`examples/costume-sample`](../../examples/costume-sample) 範例為基礎。下載它來熟悉模組的結構。

## 模組結構

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← 服裝資料夾
│   └── WhiteCat/              ← 一個資料夾 = 一套服裝
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← 其他動作的 PNG
└── Localization/              ← 服裝名稱與說明文字
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

以範例為基礎製作自己的模組時，請更改 `metadata.json` 中的 `modId` 與 `author`。（詳情請參閱 [`metadata.json`](metadata.md)。）

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


## 精靈圖動畫

- 如果把所有動畫都做齊，一套服裝需要相當多的動畫。
- 如果這是你的第一套服裝，建議先只用 `idle, move, airborne, fall` 做一套簡單的。

| 動作 | 說明 | 必要 |
|---|---|---|
| `idle` | 待機 | 必要 |
| `move` | 移動 | 必要 |
| `airborne` | 空中 | 選用 |
| `fall` | 倒下 | 選用 |
| `attack` | 攻擊 | 選用 |
| `greatsword_lower` | 迴旋揮砍 | 選用 |
| `greatsword_sweep` | 旋轉 | 選用 |
| `hitfeedback` | 受擊 | 選用 |

### 背面動畫

- 想讓服裝看起來更自然，可以繪製背面的精靈圖。這是選用的。
- 如果決定加入背面精靈圖，就必須包含 `idle_back, move_back` 這兩個動作。
- 背面動畫定義如下。

| 動作 | 說明 |
|---|---|
| `idle_back` | 待機（背面） |
| `move_back` | 移動（背面） |
| `attack_back` | 攻擊（背面） |
| `greatsword_upper` | 迴旋揮砍（背面） |

## 準備美術素材

- 每個動作都做成一張 PNG 精靈圖表，將該動作的影格水平排列。
- 圖片高度等於影格高度，寬度為影格寬度 × 影格數。影格之間不要留空隙。
- 建議每張個別圖片都使用相同尺寸。
- 在白貓範例中，每個精靈圖為 20×16，圖片尺寸如下。

| 檔案 | 尺寸 | 影格數 |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*範例圖片*


### 撰寫新服裝的資訊

撰寫 `costume.json` 檔案來定義你自己的服裝。

`Costumes/WhiteCat/costume.json` 範例：

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

| 欄位 | 必要 | 說明 |
|---|---|---|
| `schemaVersion` | 是 | 格式識別碼。填入整數 `1`。 |
| `id` | 是 | 使用小寫英文字母、數字、底線與連字號，1 至 31 個字元。為避免衝突，遊戲內會以 `<modId>:<id>` 的形式註冊。 |
| `name` | 是 | 服裝名稱的**在地化鍵**。請參閱下方的[準備在地化檔案](#準備在地化檔案)。 |
| `flavorText` | 否 | 服裝說明的**在地化鍵**。請參閱下方的[準備在地化檔案](#準備在地化檔案)。 |
| `icon` | 是 | 服裝預覽圖示 PNG 的路徑。 |
| `face` | 否 | 多人遊戲 HUD 用的臉部 PNG 路徑。 |
| `pixelsPerUnit` | 否 | 想使用更精細的美術時可更改此值。值越高，美術的細節越細緻。預設 PPU 為 16。 |
| `frameSize` | 否 | 每個精靈圖的 `[寬, 高]`。預設：`[16, 16]`。 |
| `pivot` | 否 | 每個精靈圖的樞軸點。左下角為 `[0, 0]`。預設：`[寬/2, 0]`。 |
| `animations` | 是 | 每個動作的美術。請參閱 [animations](#animations)。 |
| `stats` | 否 | 要設定的能力值清單。請參閱[能力值清單](stats.md)。 |
| `startingItems` | 否 | 初始物品 ID 的陣列。請參閱[物品清單](item-table.md)。 |
| `body` | 否 | 設定碰撞範圍。請參閱 [body](#body)。 |
| `skins` | 否 | 同一套服裝的額外外觀。請參閱 [skins](#skins)。 |

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

- 用 `{ "file", "fps", "loop", "footstepFrames" }` 這四個屬性來定義上面準備好的動畫。
- 也可以為每個動作分別設定 `frameSize` 與 `pivot`。
- 如果只有某個動作以不同尺寸繪製（如最後一行），就在該動作上設定 `frameSize` 與 `pivot`。

#### 屬性

| 欄位 | 必要 | 說明 |
|---|---|---|
| `file` | 是 | 要播放的檔名。 |
| `fps` | 否 | 動畫播放速度。（預設：12） |
| `loop` | 否 | 動畫是否循環。（預設：true；攻擊動畫為 false） |
| `footstepFrames` | 否 | 從零開始計算的影格編號。顯示這些影格時會播放腳步聲。 |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- 定義角色的移動與受擊碰撞範圍。
- 內建服裝使用這些值作為預設值。
- 除非有特別的理由，否則建議省略。

#### 屬性

| 欄位 | 必要 | 說明 |
|---|---|---|
| `hitboxRadius` | 否 | 受擊範圍圓形的半徑，單位為像素，0.5 至 64。預設 `4`。 |
| `hitboxOffset` | 否 | 受擊範圍圓形的中心 `[x, y]`，以相對於樞軸點的像素表示；x 為正時向右，y 為正時向上，±64。預設 `[0, 4]`。 |
| `movementRadius` | 否 | 與牆壁和障礙物碰撞的移動碰撞圓形半徑，以單位計（16 像素 = 1），0.05 至 2。預設 `0.25`。 |
| `bodyScale` | 否 | 持握武器的肩膀位置比例，0.1 至 4。預設 `1`。當大型服裝的武器陷進身體裡時可調高。 |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- 為同一套服裝新增額外的外觀。
- 動畫的格式與上面相同。
- 若省略 `frameSize` 或 `pivot`，會使用服裝本身的值。




## 準備在地化檔案

`Localization/ko-KR.json`：

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`：

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- 在 `name` 與 `flavorText` 中填寫**在地化鍵**而不是文字，並將文字放在同一個模組的 `Localization/` 資料夾中，每種語言一個檔案。
- 鍵請使用英文字母、數字、底線與連字號，不含空格。加入你的模組名稱有助於避免與其他模組衝突。
- 如果鍵**不在任何語言檔中**，服裝就不會載入。至少必須提供一種語言。
- 建議也加入英文（`en-US`），讓更多人能享受你的服裝。
- 別忘了也要在 `metadata.json` 的 `modules` 中宣告 `localization` module。



## 確認結果

1. 把模組資料夾放進 `WorkshopLocal/`，然後啟動遊戲。
2. 開始遊戲，在村莊開啟服裝選擇（**更換服裝**），確認你的新服裝已經加入。
3. 如果更改了服裝的內容，需要重新啟動遊戲。



## 補充：只為現有服裝新增外觀

製作 `skin.json` 而不是 `costume.json`，就能為現有服裝建立外觀。

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

- `costume` 是目標服裝的 id。請使用內建服裝的 id（例如 `PinkRabbit`），若是其他模組的服裝，則使用含模組名稱的執行期 id。（以白貓範例來說是 `yourname.costume-whitecat:whitecat`。）
- `stats`、`startingItems`、`body`、`skins` 與 `flavorText` 會直接沿用現有服裝的設定，因此無法設定。
- 不支援覆寫內建服裝的預設外觀。
