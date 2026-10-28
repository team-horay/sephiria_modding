# metadata.json

位于模组文件夹根目录的 `metadata.json` 是核心文件，它同时描述了你的模组**是什么**以及**如何运行**。


## 整体结构

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

顶层定义了三项内容。

| 键 | 作用 | 是否必需 |
|---|---|---|
| `info` | 在 Steam 创意工坊和游戏内面板中显示的标题、描述和预览图 | 必需 |
| `tags` | 创意工坊标签 | 可选 |
| `mod` | 模组的运行方式 | 必需 |

## info

### title

- 模组的名称。若为空，则使用 `modId` 作为名称。
- 可以使用语言映射为每种语言分别定义模组名称。
	- 除了你自己的语言之外，建议同时添加英语。
- 支持的语言代码：`ko-KR`、`en-US`、`ja-JP`、`zh-CN`、`zh-TW`、`de-DE`、`fr-FR`、`es-ES`、`it-IT`、`pl-PL`、`pt-BR`、`ru-RU`、`sv-SE`、`th-TH`、`tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- 模组的描述。
- 与 `title` 一样，可以按语言分别定义描述。

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- 上传到创意工坊时必须提供预览图。（PNG 或 JPG，小于 1 MB）
- 填写预览图的相对路径。
- 若省略，游戏会按 `thumbnail.png`、`preview.png`、`thumbnail.jpg`、`preview.jpg` 的顺序在根文件夹中查找。


## mod

### schemaVersion

- 当前版本为 1。
- 请注意，今后如果模组的运行方式发生重大变化，这个数字可能会增加。

### modId

- 用于识别模组的唯一字符串。可使用小写英文字母、数字、句点、连字符和下划线，最多 64 个字符。
- **请选择一个独一无二的名称。** 如果你的 `modId` 与其他模组冲突，模组将无法正常运行。以你的创作者名作为前缀能有效避免冲突。
（例如 `horay.artifact-pack`）
- **发布后不要更改 `modId`。** 其他人可以通过 `dependencies` 在你的模组之上制作子模组，而他们是通过 `modId` 来识别父模组的。


### version

- 定义模组版本的字符串。
- 当其他模组通过 `dependencies.version` 要求精确版本时，比较的就是这个值。
- **每次更新模组时都要提升版本号。** 加入多人游戏大厅时，游戏会检查主机和参与者的模组版本是否一致。如果你修改了内容却沿用相同的版本号，模组内容不同的玩家可能会进入同一个大厅，导致内容同步失败或游戏无法继续进行。
- 建议使用与游戏相同的 1.0.0 格式。

### author

让大家知道你是谁！

### multiplayer / installation

定义多人游戏中的玩家是否需要共享此模组。允许的值如下：

| 值 | 说明 |
|---|---|
| `local_only` | 只影响你自己的画面和 UI（本地化模组和简单的 UI 模组属于此类） |
| `everyone` | 房间内所有人都必须启用相同的模组（任何添加或修改内容的模组都属于此类） |
| `host_only` | 只需主机启用；其他参与者无需任何改动即可正常运行（只影响服务器端计算的多人游戏模组属于此类） |

如果你把 `installation` 声明为 `local_only` 或 `host_only`，但模组又涉及参与者同样需要的内容（注册新物品或单位、网络对象、同样会在参与者客户端上运行的代码），那么同一大厅中的玩家可能会玩到不同的游戏，或者内容同步失败、游戏无法正常进行。请务必确认你的模组实际做了什么。


### modules

- 模块定义了模组的功能，因此这是**最重要的部分**。
- 当前的模块有 `localization, dll, costume, data`。
- 如下例所示，可以在一个包中混合使用多个模块。
- 只要有一个模块无法识别，整个包就会被标记为“不支持”，并在加载时被跳过。

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization 模块

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- `localization` 模块用于包含语言文件的模组。
- 将 `path` 设为存放语言文件的文件夹。
- 在 `path` 文件夹中放入**以语言代码命名的 JSON 文件**，例如 `ko-KR.json` 和 `en-US.json`。游戏使用的语言代码和文件格式请参阅[本地化模组](localization-mods.md)。

#### type: dll 模块

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- `dll` 模块让你可以用 C# 代码挂接到游戏中。
- 将 `path` 设为 DLL 文件的路径。
- 将 `entryClass` 设为 DLL 中入口点类的完整名称，包括命名空间。游戏不会在 DLL 中搜索入口点，所以**必须明确指定**。（嵌套类请使用 `Ns.Outer+Inner`。）
- 不要在两个不同的模块中使用相同的 `entryClass`。
- 仅在 Mono 构建上可用。
- 详情请参阅 [DLL 模组](dll-mods.md)。

#### type: costume 模块

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- 无需编写任何 C# 代码即可制作你自己的服装。
- 将 `path` 设为存放服装文件的文件夹。
- 多人游戏中，所有人都必须安装。
- 详情请参阅[服装模组](costume-mods.md)。


#### type: data 模块

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- 供模组使用的通用数据模块。一般情况下用不到，主要用于模组之间的依赖关系。
- `consumer` 指定由哪个模组使用这个数据模块，但通常可以省略。省略时，`dependencies` 中列出的所有模组都可以读取这些数据。
- 如果你有多个父模组，而数据只提供给其中一个，就在 `consumer` 中填入该模组的 `modId`。这个值也必须出现在 `dependencies` 中。

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- `dll` 模块所引用的辅助程序集列表。
- 游戏**只使用这里声明的文件**来解析引用。仅把 DLL 放进文件夹是不会被找到的。
- 每一项可以是字符串，也可以是 `{ "path": "..." }` 对象。
- 每一项都必须以 `.dll` 结尾，并且若与 `modules` 中的 DLL 重复，将无法正常运行。
- 库不会被当作入口点。即使其中包含 `WorkshopModBase` 类，也不会被调用。

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- 如果父模组是“创建内容”类的模组，子模组可以在其基础上添加新内容。
- 这时可以通过 `dependencies` 声明对另一个模组的依赖。
- 指定父模组后，你的模组不会先于该父模组加载。
- 如果父模组缺失、被停用或加载失败，其所有子模组的加载都会被取消。
- 一个依赖项可以包含以下三个字段。

| 字段 | 是否必需 | 规则 |
|---|---|---|
| `modId` | 是 | 你所依赖的模组的 `modId` |
| `version` | 否 | **仅在需要版本字符串精确匹配时使用。** 版本不同时会跳过加载，所以通常省略更安全。 |
| `workshopId` | 否 | 让用户能够找到并订阅所需的模组。 |


## tags

- 上传到创意工坊时使用的分类。
- 一个模组可以有多个标签。（同时添加物品和服装的综合模组可使用 `"tags": ["Item", "Costume"]`。）

| 分类 | 内容 |
|---|---|
| `Item` | 物品（神器、石板、药水等）、连招 |
| `Costume` | 服装 |
| `Weapon` | 武器 |
| `NPC` | 旅行者、商人等 |
| `Enemy` | 新敌人、Boss 等 |
| `Gameplay` | 游戏玩法改动 |
| `QoL` | UI、便利性和无障碍改进 |
| `Library` | 供其他模组依赖的运行时、框架、数据包等 |
| `Localization` | 翻译 |

## 路径规则
 
- `modules[].path`、`libraries` 和 `info.preview` 中的路径都遵循相同的规则。
- 只允许相对于模组文件夹的路径。（绝对路径、盘符和 `..` 会被拒绝。）
- 使用 `/` 作为分隔符。
- 经过符号链接或联接点的路径会被拒绝。
- **大小写必须与实际名称完全一致。** 在 Windows 上，写成 `localization` 仍能打开文件夹，但游戏会把它视为与 `Localization` 不同而拒绝。（这条规则是为了在 Steam Deck（Linux）上保证相同的行为。）
