# Localization Mods

[Your First Mod](getting-started.md) covered the basics of making a mod, so this page covers the common rules for localization mods.
[`examples/translation-sample`](../../examples/translation-sample) contains a complete example you can copy straight into `WorkshopLocal/` to try out.



## How It Works

- The game has one **key → string** table per language. When a mod is loaded, the values in the mod overwrite the original table.
- Your file only needs the keys you want to change.
- The file name is the language. `ko-KR.json` overwrites the Korean table, and `en-US.json` overwrites the English table.
- If multiple localization mods set the same key, the mod loaded later wins.
- A mod that only changes text doesn't affect other participants, so we recommend setting `multiplayer.installation` in `metadata.json` to `local_only`.
- Both keys and values are strings. Nested objects and arrays aren't allowed.
- Save files as **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← add { "type": "localization", "path": "Localization" } to modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## What Not to Translate

Game text contains expressions that are interpreted at runtime.
Don't translate the text below; keep it in English exactly as is (case-sensitive).

| Type    | Example                                                                     | Description                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Keywords   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Replaced with numbers or names at runtime. Don't change a single character inside the braces.   |
| Game tags | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Replaced with keyword tooltips and icons. The names inside tags are fixed, including case. |
| Formatting tags | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Text formatting. Opening and closing tags must be paired.        |
| Line breaks   | `\n`                                                                        | A line break inside a JSON string.                        |




## Adding a New Language

- You can also add a language the game doesn't support. If the file name is a new language code, the game registers it as a new language and shows it in the language settings list.
- The original language files are in `<game install folder>/Sephiria_Data/StreamingAssets/Localization/`, one per language; use them as a reference when translating.
- The languages the game already supports are `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, and `tr-TR`.
- **Translate every key.** Keys missing from your mod file aren't shown as blank; they fall back to **English** (or Korean if English is also missing). The easiest way to start is to copy the original `en-US.json`.
- **Fill in the settings keys that start with `#`.** `#LanguageName` is the name shown in the language settings list, and `#FontName` is the font to use. Adjust punctuation keys such as `#Colon`, `#Bullet`, and `#ParenthesisOpen` to suit the conventions of that language.
- For `#FontName`, you can choose from the fonts built into the game. The game includes fonts for the following scripts:

| Script | `#FontName` |
|---|---|
| Latin, Cyrillic, Hangul | `GalCyrillic` |
| Japanese | `JF-Dot-MPlus10` |
| Simplified Chinese | `Fusion_Pixel_10_Simple` |
| Traditional Chinese | `BoutiqueBitmap9x9` |
| Thai | `NotoSansThai` |



## Troubleshooting

| Symptom | What to check |
|---|---|
| The mod is enabled but the text doesn't change | Whether the key name matches the original exactly, down to every character. Whether the file name matches the current game language (`ko-KR.json` only applies in Korean). |
| Only some keys don't change | Whether another localization mod is overwriting the same key later. Try changing the order in the panel. |
| `{VALUE}` shows up as-is in the text | The keyword is spelled differently from the original. Copy it directly from the original text. |
| `<tag=…>` shows up as-is in the text | The tag name's spelling or case is different. |
| Korean (or other non-English) text appears garbled | The file isn't UTF-8. Save it again as UTF-8. |
| Characters show up as boxes | Those characters aren't included in the game's built-in fonts. |
| An entire file is ignored | That file has a JSON syntax error. `Player.log` records the error along with the file name. |

The log file is at `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Search for "모드 내 언어 파일 발견" (language file found in mod) to see which files were read and how many keys each had.
