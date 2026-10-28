# 服装模组

不写代码，只用 PNG 和 JSON 文件就能添加服装。这类模组可以添加以下两种内容：

- **新服装**：创建一件全新的服装。可以设置其外观、属性、初始物品等。
- **现有服装的外观**：为粉红兔子等现有服装添加外观（皮肤）。

本页基于 [`examples/costume-sample`](../../examples/costume-sample) 示例。下载它可以熟悉模组的结构。

## 模组结构

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← 服装文件夹
│   └── WhiteCat/              ← 一个文件夹 = 一件服装
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← 其他动作的 PNG
└── Localization/              ← 服装名称和描述文本
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

基于示例制作你自己的模组时，请修改 `metadata.json` 中的 `modId` 和 `author`。（详情请参阅 [`metadata.json`](metadata.md)。）

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


## 精灵动画

- 如果把所有动画都做全，一件服装需要相当多的动画。
- 如果这是你的第一件服装，可以先只做 `idle, move, airborne, fall` 这几个简单的动作。

| 动作 | 说明 | 是否必需 |
|---|---|---|
| `idle` | 待机 | 必需 |
| `move` | 移动 | 必需 |
| `airborne` | 空中 | 可选 |
| `fall` | 倒下 | 可选 |
| `attack` | 攻击 | 可选 |
| `greatsword_lower` | 旋转挥砍 | 可选 |
| `greatsword_sweep` | 旋转 | 可选 |
| `hitfeedback` | 受击 | 可选 |

### 背面动画

- 为了让服装看起来更自然，可以绘制背面的精灵图。这是可选的。
- 如果决定添加背面精灵图，则必须包含 `idle_back, move_back` 这两个动作。
- 背面动画的定义如下。

| 动作 | 说明 |
|---|---|
| `idle_back` | 待机（背面） |
| `move_back` | 移动（背面） |
| `attack_back` | 攻击（背面） |
| `greatsword_upper` | 旋转挥砍（背面） |

## 准备美术素材

- 为每个动作制作一张 PNG 精灵表，把该动作的各帧横向排列。
- 图片高度等于帧高度，宽度为帧宽度 × 帧数。帧与帧之间不要留空隙。
- 建议把每张图片都做成相同的尺寸。
- 在白猫示例中，每个精灵为 20×16，各图片的尺寸如下。

| 文件 | 尺寸 | 帧数 |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*示例图片*


### 编写新服装的信息

通过编写 `costume.json` 文件来定义你的服装。

`Costumes/WhiteCat/costume.json` 示例：

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

| 字段 | 是否必需 | 说明 |
|---|---|---|
| `schemaVersion` | 是 | 格式标识。填入整数 `1`。 |
| `id` | 是 | 使用小写英文字母、数字、下划线和连字符，1–31 个字符。为避免冲突，在游戏内会以 `<modId>:<id>` 的形式注册。 |
| `name` | 是 | 服装名称的**本地化键**。参阅下方的[准备本地化文件](#准备本地化文件)。 |
| `flavorText` | 否 | 服装描述的**本地化键**。参阅下方的[准备本地化文件](#准备本地化文件)。 |
| `icon` | 是 | 服装预览图标 PNG 的路径。 |
| `face` | 否 | 用于多人游戏 HUD 的面部 PNG 的路径。 |
| `pixelsPerUnit` | 否 | 想使用更精细的美术素材时修改此值。数值越高，素材渲染得越精细。默认 PPU 为 16。 |
| `frameSize` | 否 | 每个精灵的 `[宽, 高]`。默认：`[16, 16]`。 |
| `pivot` | 否 | 每个精灵的轴心点。左下角为 `[0, 0]`。默认：`[宽/2, 0]`。 |
| `animations` | 是 | 各动作的素材。参阅 [animations](#animations)。 |
| `stats` | 否 | 要设置的属性列表。参阅[属性列表](stats.md)。 |
| `startingItems` | 否 | 初始物品 ID 的数组。参阅[物品列表](item-table.md)。 |
| `body` | 否 | 设置碰撞区域。参阅 [body](#body)。 |
| `skins` | 否 | 同一服装的额外外观。参阅 [skins](#skins)。 |

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

- 用 `{ "file", "fps", "loop", "footstepFrames" }` 这四个属性来定义上面准备好的动画。
- 也可以为每个动作单独设置 `frameSize` 和 `pivot`。
- 如果只有某一个动作绘制成了不同的尺寸（如最后一行），就在该动作上设置 `frameSize` 和 `pivot`。

#### 属性

| 字段 | 是否必需 | 说明 |
|---|---|---|
| `file` | 是 | 要播放的文件名。 |
| `fps` | 否 | 动画播放速度。（默认：12） |
| `loop` | 否 | 动画是否循环。（默认：true；攻击动画为 false） |
| `footstepFrames` | 否 | 从 0 开始计数的帧编号。显示到这些帧时会播放脚步声。 |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- 定义角色的移动碰撞区域和受击碰撞区域。
- 内置服装使用这些值作为默认值。
- 除非有特殊理由，建议省略此项。

#### 属性

| 字段 | 是否必需 | 说明 |
|---|---|---|
| `hitboxRadius` | 否 | 受击碰撞圆的半径，单位为像素，0.5–64。默认 `4`。 |
| `hitboxOffset` | 否 | 受击碰撞圆的中心 `[x, y]`，以像素为单位、相对于轴心点；x 为正表示向右，y 为正表示向上，±64。默认 `[0, 4]`。 |
| `movementRadius` | 否 | 与墙壁和障碍物碰撞的移动碰撞圆的半径，单位为 unit（16 像素 = 1），0.05–2。默认 `0.25`。 |
| `bodyScale` | 否 | 持握武器的肩部位置的缩放比例，0.1–4。默认 `1`。当武器陷进大型服装的身体里时，可以调高此值。 |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- 为同一服装添加额外的外观。
- 动画的格式与上面相同。
- 若省略 `frameSize` 或 `pivot`，则使用服装本身的值。




## 准备本地化文件

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

- 在 `name` 和 `flavorText` 中填写**本地化键**而不是文本，并把文本放在同一模组的 `Localization/` 文件夹中，每种语言一个文件。
- 键请使用英文字母、数字、下划线和连字符，不要包含空格。把模组名包含在键中有助于避免与其他模组冲突。
- 如果某个键**在任何语言文件中都不存在**，服装将无法加载。至少必须提供一种语言。
- 建议同时包含英语（`en-US`），让更多人能够使用你的服装。
- 别忘了在 `metadata.json` 的 `modules` 中同时声明 `localization` 模块。



## 确认效果

1. 把模组文件夹放进 `WorkshopLocal/`，然后启动游戏。
2. 开始游戏，在村庄中打开服装选择（**更换服装**），确认你的新服装已被添加。
3. 修改服装内容后，需要重启游戏。



## 补充：只为现有服装添加皮肤

通过编写 `skin.json` 而不是 `costume.json`，可以为现有服装创建外观。

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

- `costume` 是目标服装的 id。可以使用内置服装的 id（例如 `PinkRabbit`），若是其他模组的服装，则使用包含模组名的运行时 id。（以白猫示例为例，即 `yourname.costume-whitecat:whitecat`。）
- `stats`、`startingItems`、`body`、`skins` 和 `flavorText` 直接沿用现有服装的设置，无法自行设定。
- 不支持覆盖内置服装的默认外观。
