# 你的第一个模组

本指南介绍 Sephiria 模组的基本结构，并带你制作第一个模组并在游戏中确认效果。

## 准备工作

- 一个趁手的文本编辑器（例如 [Visual Studio Code](https://code.visualstudio.com/)）

## 找到游戏安装文件夹

打开 Sephiria 的安装文件夹。

- 从 Steam 库中打开
  - `Steam 库 → 右键点击 Sephiria → 管理 → 浏览本地文件`
- Windows（默认安装路径）
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS（默认安装路径）
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria`（包含 `Sephiria.app` 的文件夹）

## 创建 WorkshopLocal 文件夹和你自己的模组文件夹

1. 在游戏安装文件夹中创建 `WorkshopLocal/` 文件夹。
2. 在其中为你自己的模组创建一个文件夹。
   - Sephiria 将每个文件夹视为一个模组。
   - 在这里创建的模组可以在游戏中测试，也可以上传到 Steam 创意工坊。

本指南将制作 `my-first-mod`，一个把标题画面上“开始游戏”按钮的文字改为“Hello World!”的模组。完成后的结构如下：

```
<游戏安装文件夹>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← 一个模组
    │   ├── metadata.json      ← 定义模组的核心文件
    │   ├── thumbnail.png      ← 模组预览缩略图（上传创意工坊时必需）
    │   └── Localization/      ← 由本地化模块读取的文件夹
    │       └── en-US.json     ← 要替换的文本
    └── another-mod/           ← 另一个模组
        └── ...
```

## metadata.json 文件

- 在模组文件夹中创建 `metadata.json`，并粘贴以下内容。
- `metadata.json`：定义模组的关键文件。
- `yourname`：替换为你自己的昵称。
- 以 **UTF-8** 编码保存文件。在记事本中，可在“另存为”的编码选项中选择 UTF-8。
- 各字段的含义请参阅 [metadata.json 参考](metadata.md)。

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

## 创建语言文件

- 在模组文件夹中创建 `Localization/` 文件夹。
- 在 `Localization/` 中创建 `en-US.json` 文件，内容如下。
- 测试时也可以把文件名改成你自己的语言代码。

```json
{
  "Title_GameStart": "Hello World!"
}
```

## 确认效果

现在可以确认模组是否生效了。

1. 启动 Sephiria。
2. 将游戏语言设置为英语后查看标题画面：“Start Game”按钮应显示为“Hello World!”。`WorkshopLocal/` 中的模组默认启用，无需手动开启。
3. 点击标题画面上的 **Steam 创意工坊** 按钮打开创意工坊面板。“My First Mod”应出现在“我的模组”栏目中。

### 我的模组没有生效！

- 检查 JSON 语法是否正确。
- `metadata.json` 中的 `path` 必须与实际文件夹名完全一致，包括大小写。
- 检查 `metadata.json` 是否以 UTF-8 编码保存。
- 检查游戏语言是否与文件名中的语言一致。

## 下一步

1. 通过 [metadata.json 参考](metadata.md)了解模组是如何定义的。
2. [本地化模组](localization-mods.md)和[服装模组](costume-mods.md)相对容易制作，建议从这两种开始。
3. 如果你有基础的编程知识，推荐尝试 [DLL 模组](dll-mods.md)。
4. 按照[发布到创意工坊](publishing.md)把你的模组发布到创意工坊。
