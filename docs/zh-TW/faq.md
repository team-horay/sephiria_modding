# 常見問題


## 如何查看記錄檔？

遊戲記錄會寫入下方的檔案。每次啟動遊戲時都會重寫，上一次的記錄則保留在 `Player-prev.log`。

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

與模組相關的行都以方括號中的標籤開頭，搜尋該標籤即可。

| 標籤 | 內容 |
|---|---|
| `[WorkshopModManager]` | 訂閱清單查詢、下載、本機資料夾掃描、載入計畫摘要 |
| `[WorkshopModRuntime]` | DLL 啟用與在地化合併結果 |
| `[WorkshopUploader]` | 審核提交與工作坊上傳 |
| `[ModGuardClient]` | 與審核伺服器的通訊 |
| `[WorkshopLobbyMods]` | 多人遊戲房間的模組組合比對 |
| `모드 내 언어 파일 발견`（在模組中發現語言檔） | 每個在地化檔案讀取到的鍵數量 |


## Steam 工作坊面板中的「本機模組（AddOns資料夾）」是什麼？

你可能會在面板中看到「本機模組（AddOns資料夾）」項目。這是舊版的模組功能，請勿用於新模組。
舊版模組無法與其他玩家同步，建議移除。


## 我想把 WorkshopLocal 資料夾放在 Sephiria 安裝資料夾以外的地方。

如果想把模組放在其他磁碟機或工作資料夾中，可以用啟動參數傳入路徑。在 Steam 收藏庫中，前往 Sephiria → 內容 → 一般 → 啟動選項，輸入：

```
-workshop_local=D:\mods\my-first-mod
```

多個路徑以分號分隔；若路徑含有空格，請用引號把整個字串括起來。

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

以參數傳入的資料夾會被視為與 `WorkshopLocal/` 的子資料夾完全相同。


## 我弄丟了 uploadticket.json、mod-guard-manifest.json 與 mod-guard.sig.json。

如果只缺少憑證檔案（`mod-guard-manifest.json`、`mod-guard.sig.json`），而 `uploadticket.json` 還在，請在面板中選取模組並按下**取得審核憑證**按鈕，從審核伺服器重新下載。但如果連 `uploadticket.json` 也遺失了（例如重灌系統之後），遊戲會把資料夾視為新模組，直接上傳會建立新的工作坊物品，而不是更新現有物品。若要繼續更新現有物品，請依以下順序復原：

1. 在工作坊訂閱你自己的模組。
2. 將 Steam 下載的整個資料夾（`<Steam 收藏庫>/steamapps/workshop/content/2436940/<工作坊物品 ID>/`）複製到 `WorkshopLocal/`。資料夾名稱可以任意取。
3. 在複製過來的資料夾中建立 `uploadticket.json`，內容如下。數字請使用工作坊頁面網址中 `id=` 後面的數字。
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. 啟動遊戲後，模組會在面板中顯示為「未申請審核」。依序進行申請審核 → 核准 → 取得審核憑證 → 再次上傳至工作坊，**現有物品就會被更新**。（注意：如果你複製了現有模組的資料夾來製作另一個不同的模組，**務必刪除** `uploadticket.json` 與憑證檔案。若保留下來，新模組會覆寫原本模組的工作坊物品。）
