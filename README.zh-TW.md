[한국어](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Polski](README.pl.md) | [Português (Brasil)](README.pt-BR.md) | [Русский](README.ru.md) | [Svenska](README.sv.md) | [ไทย](README.th.md) | [Türkçe](README.tr.md)

# Sephiria 模組製作指南

感謝你加入 Sephiria 的模組製作社群。

這是 [Sephiria](https://store.steampowered.com/app/2436940) 的官方模組製作指南、範例與範本儲存庫。模組可以透過 [Steam 工作坊](https://steamcommunity.com/app/2436940/workshop/)分享。



## 規則

為了讓所有人都能安全地遊玩，請遵守以下規則。違反規則可能會被限制發布模組或遊玩遊戲。

1. **禁止惡意行為**：禁止惡意軟體、後門、鍵盤側錄程式、加密貨幣挖礦程式，以及任何會危害使用者裝置、帳號或資料的功能。
2. **非商業用途**：所有模組都必須以非商業目的製作與散布。
3. **禁止侵權與有害內容**：禁止侵害他人著作權、隱私或肖像權的內容，以及含有色情內容，或鼓吹仇恨、騷擾、現實暴力的內容。
4. **禁止妨礙遊玩**：請勿刻意讓遊戲故障，或干擾其他玩家正常遊玩。
5. **禁止蒐集資料**：請勿蒐集使用者資訊或將其傳送到外部。
6. **禁止繞過存取驗證**：請勿以不正當方式繞過遊戲或散布平台的購買、持有或存取驗證。
7. **模組審核**：含有程式碼（DLL）的模組在核准前會經過審核，包括分析與反編譯，以檢查是否違反規則。核准可能需要一些時間。請勿偽造審核結果或核准標記。檔案必須以可供審核的狀態提供；禁止混淆（obfuscation）。
8. **遊戲素材的使用**：在製作模組所需的範圍內，允許反編譯與分析遊戲的程式碼、美術、音效等素材。但請勿再散布擷取出來的遊戲素材本身。
9. **禁止冒充與竄改**：請勿冒充他人，也請勿在未經同意的情況下修改並再散布他人製作的模組。



## 開始之前

- 含有程式碼（DLL）的模組必須先通過**審核**才能上傳至工作坊。審核主要是檢查程式碼是否有惡意行為。翻譯、服裝等不含程式碼的模組可以直接上傳，不需審核。請注意，審核系統無法讀取的模組會被拒絕（混淆過的檔案、以 C/C++ 編譯的 DLL 等）。
- 在多人遊戲中，房間內的所有參與者都必須啟用相同的模組組合。（翻譯等個人 UI 模組除外。）
- 啟用模組之後，若要變更模組順序或再次停用，必須重新啟動遊戲。
- 已訂閱的模組預設為停用。你可以自行在清單中啟用。



## 文件

| 文件 | 內容 |
|---|---|
| [你的第一個模組](docs/zh-TW/getting-started.md) | 建立開發資料夾、在本機確認模組、讀取記錄檔 |
| [metadata.json 參考](docs/zh-TW/metadata.md) | 每個欄位的意義、相依性與版本規則、模組（module）宣告 |
| [在地化模組](docs/zh-TW/localization-mods.md) | 語言檔結構、尋找鍵值、保留標籤與佔位符的規則 |
| [服裝模組](docs/zh-TW/costume-mods.md) | 製作你自己的服裝 |
| [DLL 模組](docs/zh-TW/dll-mods.md) | 專案設定、進入點、`HorayModAPI` 事件、偵錯 |
| [發布至工作坊](docs/zh-TW/publishing.md) | 上傳至工作坊、程式碼模組審核、多人遊戲規則 |
| [API 參考](docs/zh-TW/api/README.md) | `HorayModAPI`、`WorkshopModBase`、模組選項、UI 等公開型別與成員。由遊戲原始碼自動產生 |
| [附錄與常見問題](docs/zh-TW/faq.md) |  |


## 授權

本儲存庫中的程式碼（範例與範本）以 [MIT 授權條款](LICENSE)授權。
文件可以自由引用、翻譯與再散布；請註明出處。
Sephiria 遊戲本身及其素材為 TEAM HORAY 的著作，不在本授權範圍內。
