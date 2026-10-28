# 本地化模组

[你的第一个模组](getting-started.md)已经介绍了制作模组的基础，本页介绍本地化模组的通用规则。
[`examples/translation-sample`](../../examples/translation-sample) 中有一个完整的示例，可以直接复制到 `WorkshopLocal/` 中试用。



## 工作原理

- 游戏为每种语言维护一张**键 → 字符串**表。加载模组时，模组中的值会覆盖原始表。
- 你的文件只需包含想要修改的键。
- 文件名即语言。`ko-KR.json` 覆盖韩语表，`en-US.json` 覆盖英语表。
- 如果多个本地化模组设置了同一个键，后加载的模组生效。
- 只修改文本的模组不会影响其他参与者，因此建议把 `metadata.json` 中的 `multiplayer.installation` 设为 `local_only`。
- 键和值都必须是字符串。不允许嵌套对象和数组。
- 以 **UTF-8** 编码保存文件。

```
my-translation-mod/
├── metadata.json              ← 在 modules 中添加 { "type": "localization", "path": "Localization" }
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## 不要翻译的内容

游戏文本中包含会在运行时解析的表达式。
下面这些内容不要翻译，请原样保留英文（区分大小写）。

| 类型    | 示例                                                                     | 说明                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| 关键字   | `{VALUE}`、`{EFFECT0}`、`{LV}`、`{COUNT}`、`{ITEMNAME}`、`{COUNTER}/{GOAL}` | 运行时会被替换为数字或名称。花括号内的任何一个字符都不要改动。   |
| 游戏标签 | `<tag=FinalDamage>`、`<tag=WeaponAction_Fury>`、`<tag=TEXT:Qliphoth>`       | 会被替换为关键字提示和图标。标签内的名称是固定的，包括大小写。 |
| 格式标签 | `<color=red>…</color>`、`<sprite="Keyword" name=Dice>`、`<indent=10>`       | 文本格式。开始标签和结束标签必须成对出现。        |
| 换行   | `\n`                                                                        | JSON 字符串内的换行。                        |




## 添加新语言

- 也可以添加游戏尚不支持的语言。如果文件名是一个新的语言代码，游戏会将其注册为新语言，并显示在语言设置列表中。
- 原始语言文件位于 `<游戏安装文件夹>/Sephiria_Data/StreamingAssets/Localization/`，每种语言一个文件；翻译时可以作为参考。
- 游戏已支持的语言有 `ko-KR`、`en-US`、`ja-JP`、`zh-CN`、`zh-TW`、`de-DE`、`fr-FR`、`es-ES`、`it-IT`、`pl-PL`、`pt-BR`、`ru-RU`、`sv-SE`、`th-TH` 和 `tr-TR`。
- **翻译所有的键。** 模组文件中缺失的键不会显示为空白，而是回退到**英语**（英语也缺失时回退到韩语）。最简单的起步方式是复制原始的 `en-US.json`。
- **填写以 `#` 开头的设置键。** `#LanguageName` 是语言设置列表中显示的名称，`#FontName` 是要使用的字体。`#Colon`、`#Bullet`、`#ParenthesisOpen` 等标点键请按照该语言的习惯调整。
- `#FontName` 可以从游戏内置的字体中选择。游戏包含以下文字体系的字体：

| 文字体系 | `#FontName` |
|---|---|
| 拉丁文、西里尔文、韩文 | `GalCyrillic` |
| 日文 | `JF-Dot-MPlus10` |
| 简体中文 | `Fusion_Pixel_10_Simple` |
| 繁体中文 | `BoutiqueBitmap9x9` |
| 泰文 | `NotoSansThai` |



## 故障排查

| 症状 | 检查项 |
|---|---|
| 模组已启用，但文本没有变化 | 键名是否与原文逐字完全一致。文件名是否与当前游戏语言一致（`ko-KR.json` 只在韩语下生效）。 |
| 只有部分键没有变化 | 是否有另一个本地化模组在之后覆盖了同一个键。试着在面板中调整顺序。 |
| 文本中原样显示 `{VALUE}` | 关键字的写法与原文不同。请直接从原文复制。 |
| 文本中原样显示 `<tag=…>` | 标签名的拼写或大小写不同。 |
| 韩语（或其他非英语）文本显示为乱码 | 文件不是 UTF-8 编码。请重新以 UTF-8 保存。 |
| 字符显示为方块 | 游戏内置字体中不包含这些字符。 |
| 整个文件被忽略 | 该文件存在 JSON 语法错误。`Player.log` 会记录错误以及文件名。 |

日志文件位于 `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`。搜索“모드 내 언어 파일 발견”（在模组中发现语言文件）即可查看读取了哪些文件以及每个文件有多少个键。
