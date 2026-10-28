# 你的第一個模組

本指南說明 Sephiria 模組的基本結構，並帶你一步步製作第一個模組，然後在遊戲中確認。

## 你需要準備的東西

- 一款好用的文字編輯器（例如 [Visual Studio Code](https://code.visualstudio.com/)）

## 找到遊戲安裝資料夾

開啟 Sephiria 的安裝資料夾。

- 從 Steam 收藏庫
  - `Steam 收藏庫 → 在 Sephiria 上按右鍵 → 管理 → 瀏覽本機檔案`
- Windows（預設安裝路徑）
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS（預設安裝路徑）
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria`（包含 `Sephiria.app` 的資料夾）

## 建立 WorkshopLocal 資料夾與你自己的模組資料夾

1. 在遊戲安裝資料夾中建立 `WorkshopLocal/` 資料夾。
2. 在裡面建立一個屬於你自己模組的資料夾。
   - Sephiria 會把每一個資料夾視為一個模組。
   - 在這裡建立的模組可以在遊戲中測試，也可以上傳至 Steam 工作坊。

本指南會製作 `my-first-mod`，這個模組會把標題畫面上「開始遊戲」按鈕的文字改成「Hello World!」。完成後的結構如下：

```
<遊戲安裝資料夾>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← 一個模組
    │   ├── metadata.json      ← 定義模組的核心檔案
    │   ├── thumbnail.png      ← 模組預覽縮圖（上傳至工作坊時必要）
    │   └── Localization/      ← 在地化模組讀取的資料夾
    │       └── en-US.json     ← 要取代的文字
    └── another-mod/           ← 另一個模組
        └── ...
```

## metadata.json 檔案

- 在模組資料夾中建立 `metadata.json`，並貼上下方內容。
- `metadata.json`：定義模組的關鍵檔案。
- `yourname`：請改成你自己的暱稱。
- 請以 **UTF-8** 儲存檔案。在記事本中，可於「另存新檔」的編碼選項選擇 UTF-8。
- 每個欄位的意義請參閱 [metadata.json 參考](metadata.md)。

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## 建立語言檔

- 在模組資料夾中建立 `Localization/` 資料夾。
- 在 `Localization/` 裡建立 `en-US.json` 檔案，內容如下。
- 你也可以把檔名改成自己的語言代碼來測試。

```json
{
  "Title_GameStart": "Hello World!"
}
```

## 確認結果

到這裡就可以確認模組是否正常運作了。

1. 啟動 Sephiria。
2. 將遊戲語言設定為英文後查看標題畫面：「Start Game」按鈕的文字已變成「Hello World!」。`WorkshopLocal/` 中的模組預設為啟用，所以不需要另外開啟。
3. 按下標題畫面的 **Steam 工作坊** 按鈕開啟工作坊面板。「My First Mod」應該會出現在「我的模組」區塊中。

### 我的模組沒有套用！

- 檢查 JSON 語法是否正確。
- `metadata.json` 中的 `path` 必須與實際資料夾名稱完全一致，包括大小寫。
- 檢查 `metadata.json` 是否以 UTF-8 編碼。
- 檢查遊戲語言是否與檔名中的語言一致。

## 下一步

1. 透過 [metadata.json 參考](metadata.md)瞭解模組是如何定義的。
2. [在地化模組](localization-mods.md)與[服裝模組](costume-mods.md)相對容易製作，建議從這些開始。
3. 如果你有基本的程式設計知識，推薦嘗試 [DLL 模組](dll-mods.md)。
4. 透過[發布至工作坊](publishing.md)將模組發布到工作坊。
