# 在地化模組

[你的第一個模組](getting-started.md)已經介紹了製作模組的基礎，本頁說明在地化模組的通用規則。
[`examples/translation-sample`](../../examples/translation-sample) 含有完整的範例，可以直接複製到 `WorkshopLocal/` 試用。



## 運作方式

- 遊戲每種語言都有一張**鍵 → 字串**的對照表。載入模組時，模組中的值會覆寫原始對照表。
- 你的檔案只需要包含想要更改的鍵。
- 檔名就是語言。`ko-KR.json` 會覆寫韓文對照表，`en-US.json` 會覆寫英文對照表。
- 若多個在地化模組設定了同一個鍵，以較晚載入的模組為準。
- 只更改文字的模組不會影響其他參與者，因此建議將 `metadata.json` 中的 `multiplayer.installation` 設為 `local_only`。
- 鍵與值都必須是字串。不允許巢狀物件與陣列。
- 檔案請以 **UTF-8** 儲存。

```
my-translation-mod/
├── metadata.json              ← 在 modules 中加入 { "type": "localization", "path": "Localization" }
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## 不可翻譯的內容

遊戲文字中含有會在執行期解讀的運算式。
請勿翻譯以下文字；請原封不動保留英文（區分大小寫）。

| 類型    | 範例                                                                     | 說明                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| 關鍵字   | `{VALUE}`、`{EFFECT0}`、`{LV}`、`{COUNT}`、`{ITEMNAME}`、`{COUNTER}/{GOAL}` | 執行期會被替換成數值或名稱。大括號內的任何一個字元都不能更動。   |
| 遊戲標籤 | `<tag=FinalDamage>`、`<tag=WeaponAction_Fury>`、`<tag=TEXT:Qliphoth>`       | 會被替換成關鍵字提示與圖示。標籤內的名稱是固定的，包括大小寫。 |
| 格式標籤 | `<color=red>…</color>`、`<sprite="Keyword" name=Dice>`、`<indent=10>`       | 文字格式。開頭與結尾標籤必須成對。        |
| 換行   | `\n`                                                                        | JSON 字串內的換行。                        |




## 新增語言

- 你也可以新增遊戲尚未支援的語言。若檔名是新的語言代碼，遊戲會將它註冊為新語言，並顯示在語言設定清單中。
- 原始語言檔位於 `<遊戲安裝資料夾>/Sephiria_Data/StreamingAssets/Localization/`，每種語言一個檔案；翻譯時可作為參考。
- 遊戲目前已支援的語言有 `ko-KR`、`en-US`、`ja-JP`、`zh-CN`、`zh-TW`、`de-DE`、`fr-FR`、`es-ES`、`it-IT`、`pl-PL`、`pt-BR`、`ru-RU`、`sv-SE`、`th-TH` 與 `tr-TR`。
- **請翻譯所有的鍵。** 模組檔案中缺少的鍵不會顯示為空白，而是會回退到**英文**（若英文也沒有則回退到韓文）。最簡單的起手方式是複製原始的 `en-US.json`。
- **請填寫以 `#` 開頭的設定鍵。** `#LanguageName` 是顯示在語言設定清單中的名稱，`#FontName` 是要使用的字型。`#Colon`、`#Bullet`、`#ParenthesisOpen` 等標點鍵請依該語言的慣例調整。
- `#FontName` 可以從遊戲內建的字型中選擇。遊戲內建了以下文字系統的字型：

| 文字系統 | `#FontName` |
|---|---|
| 拉丁字母、西里爾字母、韓文 | `GalCyrillic` |
| 日文 | `JF-Dot-MPlus10` |
| 簡體中文 | `Fusion_Pixel_10_Simple` |
| 繁體中文 | `BoutiqueBitmap9x9` |
| 泰文 | `NotoSansThai` |



## 疑難排解

| 症狀 | 檢查項目 |
|---|---|
| 模組已啟用但文字沒有改變 | 鍵名是否與原始鍵完全一致，一個字元都不能差。檔名是否與目前的遊戲語言一致（`ko-KR.json` 只在韓文時套用）。 |
| 只有部分鍵沒有改變 | 是否有其他在地化模組在之後覆寫了同一個鍵。試著在面板中調整順序。 |
| 文字中直接顯示 `{VALUE}` | 關鍵字的拼法與原文不同。請直接從原文複製。 |
| 文字中直接顯示 `<tag=…>` | 標籤名稱的拼法或大小寫不同。 |
| 中文（或其他非英文）文字顯示為亂碼 | 檔案不是 UTF-8。請重新以 UTF-8 儲存。 |
| 字元顯示為方框 | 遊戲內建字型不包含這些字元。 |
| 整個檔案被忽略 | 該檔案有 JSON 語法錯誤。`Player.log` 會連同檔名一起記錄錯誤。 |

記錄檔位於 `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`。搜尋「모드 내 언어 파일 발견」（在模組中發現語言檔）即可查看讀取了哪些檔案，以及每個檔案有多少個鍵。
