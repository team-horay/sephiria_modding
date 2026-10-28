# metadata.json

位於模組資料夾根目錄的 `metadata.json` 是核心檔案，同時描述你的模組**是什麼**以及**如何執行**。


## 整體結構

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

最上層定義了三個項目。

| 鍵 | 用途 | 必要 |
|---|---|---|
| `info` | 顯示在 Steam 工作坊與遊戲內面板上的標題、說明與預覽圖 | 必要 |
| `tags` | 工作坊標籤 | 選用 |
| `mod` | 模組的執行方式 | 必要 |

## info

### title

- 模組的名稱。若留空，會改用 `modId` 作為名稱。
- 你可以使用語言對應表，為每種語言分別定義模組名稱。
	- 除了你自己的語言之外，建議也加上英文。
- 支援的語言代碼：`ko-KR`、`en-US`、`ja-JP`、`zh-CN`、`zh-TW`、`de-DE`、`fr-FR`、`es-ES`、`it-IT`、`pl-PL`、`pt-BR`、`ru-RU`、`sv-SE`、`th-TH`、`tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- 模組的說明。
- 與 `title` 一樣，可以依語言分別定義說明。

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- 上傳至工作坊時必須有預覽圖。（PNG 或 JPG，小於 1 MB）
- 填入預覽圖的相對路徑。
- 若省略，遊戲會依序在根資料夾中尋找 `thumbnail.png`、`preview.png`、`thumbnail.jpg`、`preview.jpg`。


## mod

### schemaVersion

- 目前的版本是 1。
- 請注意，未來若模組執行方式有重大變更，這個數字可能會增加。

### modId

- 用來識別模組的唯一字串。可使用小寫英文字母、數字、句點、連字號與底線，最多 64 個字元。
- **請取一個獨一無二的名稱。** 如果你的 `modId` 與其他模組衝突，模組將無法正常執行。在前面加上你的創作者名稱可以大幅降低衝突的機會。
（例如 `horay.artifact-pack`）
- **發布後請勿更改 `modId`。** 其他人可以透過 `dependencies` 在你的模組之上建立子模組，而他們是靠 `modId` 來識別上層模組的。


### version

- 定義模組版本的字串。
- 當其他模組透過 `dependencies.version` 要求特定版本時，比較的就是這個值。
- **每次更新模組時都要提高版本號。** 加入多人遊戲大廳時，遊戲會檢查房主與參與者的模組版本是否一致。如果你更改了內容卻沿用同一個版本號，模組內容不同的玩家可能會進到同一個大廳，導致內容同步失敗或遊戲無法進行。
- 建議使用與遊戲相同的 1.0.0 格式。

### author

讓大家知道你是誰！

### multiplayer / installation

定義多人遊戲中的玩家是否需要共用這個模組。允許的值如下：

| 值 | 說明 |
|---|---|
| `local_only` | 只影響你自己的畫面與 UI（在地化模組與簡單的 UI 模組屬於此類） |
| `everyone` | 房間內的所有人都必須啟用相同的模組（任何新增或變更內容的模組都屬於此類） |
| `host_only` | 只有房主需要啟用；其他參與者不需任何變更即可運作（只影響伺服器端運算的多人遊戲模組屬於此類） |

如果你將 `installation` 宣告為 `local_only` 或 `host_only`，但模組卻動到了參與者也需要的內容（註冊新物品或單位、網路物件、也會在參與者用戶端執行的程式碼），同一個大廳中的玩家可能會玩到不同的遊戲內容，或是內容同步失敗而導致遊戲無法正常進行。請務必確認你的模組實際上做了什麼。


### modules

- 模組（module）定義了你的模組（mod）要做什麼，因此這是**最重要的區段**。
- 目前的 module 有 `localization, dll, costume, data`。
- 如下方範例所示，一個套件中可以混用多個 module。
- 只要有一個 module 無法辨識，整個套件就會被標記為「不支援」，載入時會跳過該模組。

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization module

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- `localization` module 用於包含語言檔的模組。
- 將 `path` 設為存放語言檔的資料夾。
- 在 `path` 資料夾中放入**以語言代碼命名的 JSON 檔**，例如 `ko-KR.json`、`en-US.json`。遊戲使用的語言代碼與檔案格式請參閱[在地化模組](localization-mods.md)。

#### type: dll module

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- `dll` module 讓你能以 C# 程式碼掛接到遊戲中。
- 將 `path` 設為 DLL 檔案的路徑。
- 將 `entryClass` 設為 DLL 中進入點類別的完整名稱（含命名空間）。遊戲不會自行在 DLL 中搜尋進入點，所以**必須指定**。（巢狀類別請用 `Ns.Outer+Inner` 的寫法。）
- 不要在兩個不同的 module 中使用相同的 `entryClass`。
- 只能在 Mono 建置版本上運作。
- 詳情請參閱 [DLL 模組](dll-mods.md)。

#### type: costume module

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- 不需撰寫任何 C# 程式碼，就能建立你自己的服裝。
- 將 `path` 設為存放服裝檔案的資料夾。
- 在多人遊戲中，所有人都必須安裝。
- 詳情請參閱[服裝模組](costume-mods.md)。


#### type: data module

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- 模組用的通用資料 module。一般情況下不會用到，主要用於模組之間的相依關係。
- `consumer` 指定哪個模組會使用這個資料 module，但通常可以省略。若省略，`dependencies` 中列出的每個模組都可以讀取這份資料。
- 如果你有多個上層模組，而資料只給其中一個使用，就把該模組的 `modId` 填入 `consumer`。這個值也必須出現在 `dependencies` 中。

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- `dll` module 所參考的輔助組件清單。
- 遊戲**只會以此處宣告的檔案**解析參考。單純把 DLL 放進資料夾是找不到的。
- 每個項目可以是字串，或是 `{ "path": "..." }` 物件。
- 每個項目都必須以 `.dll` 結尾，且若與 `modules` 中的 DLL 重複，將無法正常執行。
- 程式庫不會被視為進入點。即使其中包含 `WorkshopModBase` 類別，也不會被呼叫。

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- 如果上層模組是「內容創作」類的模組，子模組可以在其基礎上新增內容。
- 這種情況下，你可以透過 `dependencies` 宣告對其他模組的相依性。
- 指定上層模組後，你的模組不會早於該上層模組載入。
- 若上層模組不存在、已停用或載入失敗，其所有子模組的載入都會被取消。
- 一個相依項目可以有以下三個欄位。

| 欄位 | 必要 | 規則 |
|---|---|---|
| `modId` | 是 | 你所依賴的模組的 `modId` |
| `version` | 否 | **只在需要版本字串完全一致時使用。** 版本不同時會跳過載入，所以通常省略比較安全。 |
| `workshopId` | 否 | 讓使用者能找到並訂閱所需的模組。 |


## tags

- 上傳至工作坊時使用的分類。
- 一個模組可以有多個標籤。（同時新增物品與服裝的整合模組可使用 `"tags": ["Item", "Costume"]`。）

| 分類 | 內容 |
|---|---|
| `Item` | 物品（神器、石板、藥水等）、組合 |
| `Costume` | 服裝 |
| `Weapon` | 武器 |
| `NPC` | 旅人、商人等 |
| `Enemy` | 新敵人、Boss 等 |
| `Gameplay` | 遊戲玩法變更 |
| `QoL` | UI、便利性與無障礙改善 |
| `Library` | 供其他模組依賴的執行環境、框架、資料包等 |
| `Localization` | 翻譯 |

## 路徑規則
 
- `modules[].path`、`libraries` 與 `info.preview` 中的路徑都遵循相同的規則。
- 只允許相對於模組資料夾的路徑。（絕對路徑、磁碟機代號與 `..` 都會被拒絕。）
- 請使用 `/` 作為分隔符號。
- 經過符號連結（symbolic link）或接合點（junction）的路徑會被拒絕。
- **大小寫必須與實際名稱完全一致。** 在 Windows 上，寫成 `localization` 雖然也能開啟資料夾，但遊戲會判定它與 `Localization` 不同而拒絕。（這條規則是為了確保在 Steam Deck（Linux）上也有相同的行為。）
