# Costume Mods

Let's add a costume using nothing but PNG and JSON files, no code. This kind of mod can add the following two things:

- **New costumes**: Create a brand-new costume. You can set its appearance, stats, starting items, and more.
- **Appearances for existing costumes**: Add an appearance (skin) to an existing costume such as Pink Bunny.

This page is based on the [`examples/costume-sample`](../../examples/costume-sample) example. Download it to get a feel for the mod structure.

## Mod Structure

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← costume folder
│   └── WhiteCat/              ← one folder = one costume
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← other motion PNGs
└── Localization/              ← costume name and description text
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

When you build your own mod from the example, change `modId` and `author` in `metadata.json`. (See [`metadata.json`](metadata.md) for details.)

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


## Sprite Animations

- A costume needs quite a few animations if you include all of them.
- If this is your first costume, try making a simple one with just `idle, move, airborne, fall`.

| Motion | Description | Required |
|---|---|---|
| `idle` | Idle | Required |
| `move` | Moving | Required |
| `airborne` | In the air | Optional |
| `fall` | Collapsing | Optional |
| `attack` | Attack | Optional |
| `greatsword_lower` | Spinning swing | Optional |
| `greatsword_sweep` | Spin | Optional |
| `hitfeedback` | Getting hit | Optional |

### Back-Facing Animations

- For a more natural-looking costume, you can draw back-facing sprites. This is optional.
- If you decide to add back-facing sprites, you must include the `idle_back, move_back` motions.
- Back-facing animations are defined as follows.

| Motion | Description |
|---|---|
| `idle_back` | Idle (back) |
| `move_back` | Moving (back) |
| `attack_back` | Attack (back) |
| `greatsword_upper` | Spinning swing (back) |

## Preparing the Art

- For each motion, make a PNG sprite sheet with the frames of that motion laid out horizontally.
- The image height equals the frame height, and the width is frame width × frame count. Don't leave gaps between frames.
- We recommend making every individual image the same size.
- In the White Cat example, where each sprite is 20×16, the image sizes are as follows.

| File | Size | Frames |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Example image*


### Writing the New Costume's Info

Define your own costume by writing a `costume.json` file.

Example `Costumes/WhiteCat/costume.json`:

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

| Field | Required | Description |
|---|---|---|
| `schemaVersion` | Yes | Format identifier. Enter the integer `1`. |
| `id` | Yes | Use lowercase English letters, digits, underscores, and hyphens, 1–31 characters. To avoid collisions, it's registered in-game as `<modId>:<id>`. |
| `name` | Yes | The **localization key** for the costume name. See [Preparing Localization Files](#preparing-localization-files) below. |
| `flavorText` | No | The **localization key** for the costume description. See [Preparing Localization Files](#preparing-localization-files) below. |
| `icon` | Yes | Path to the costume preview icon PNG. |
| `face` | No | Path to the face PNG for the multiplayer HUD. |
| `pixelsPerUnit` | No | Change this to use more detailed art. Higher values render the art at finer detail. The default PPU is 16. |
| `frameSize` | No | The `[width, height]` of each sprite. Default: `[16, 16]`. |
| `pivot` | No | The pivot point of each sprite. The bottom-left is `[0, 0]`. Default: `[width/2, 0]`. |
| `animations` | Yes | Art for each motion. See [animations](#animations). |
| `stats` | No | List of stats to set. See the [Stat List](stats.md). |
| `startingItems` | No | Array of starting item IDs. See the [Item List](item-table.md). |
| `body` | No | Sets the hitbox. See [body](#body). |
| `skins` | No | Additional appearances for the same costume. See [skins](#skins). |

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

- Define the animations you prepared above with four properties: `{ "file", "fps", "loop", "footstepFrames" }`.
- You can also set `frameSize` and `pivot` separately for each motion.
- If you drew just one motion at a different size, as in the last line, set `frameSize` and `pivot` on that motion.

#### Properties

| Field | Required | Description |
|---|---|---|
| `file` | Yes | The file name to play. |
| `fps` | No | Animation playback speed. (Default: 12) |
| `loop` | No | Whether the animation loops. (Default: true; false for attack animations) |
| `footstepFrames` | No | Zero-based frame numbers. A footstep sound plays when those frames are shown. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Defines the character's movement and hit collision areas.
- Built-in costumes use these values as defaults.
- Unless you have a specific reason, we recommend leaving this out.

#### Properties

| Field | Required | Description |
|---|---|---|
| `hitboxRadius` | No | Radius of the hitbox circle, in pixels, 0.5–64. Default `4`. |
| `hitboxOffset` | No | Center of the hitbox circle, `[x, y]`, in pixels relative to the pivot; positive x is right and positive y is up, ±64. Default `[0, 4]`. |
| `movementRadius` | No | Radius of the movement collision circle that collides with walls and obstacles, in units (16 pixels = 1), 0.05–2. Default `0.25`. |
| `bodyScale` | No | Scale for the shoulder position where the weapon is held, 0.1–4. Default `1`. Raise it when the weapon sinks into the body of a large costume. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Adds extra appearances to the same costume.
- Animations follow the same format as above.
- If you omit `frameSize` or `pivot`, the costume's own values are used.




## Preparing Localization Files

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- In `name` and `flavorText`, write a **localization key** instead of text, and put the text in the same mod's `Localization/` folder, one file per language.
- Use English letters, digits, underscores, and hyphens for keys, with no spaces. Including your mod's name helps avoid collisions with other mods.
- If a key **isn't in any language file**, the costume won't load. You must provide at least one language.
- We recommend also including English (`en-US`) so more people can enjoy your costume.
- Don't forget to also declare a `localization` module in `modules` in `metadata.json`.



## Check It

1. Put your mod folder in `WorkshopLocal/` and launch the game.
2. Start a game, open the costume selection in the village (**Change costume**), and check that your new costume has been added.
3. If you change the costume's content, you need to restart the game.



## Extra: Adding Only a Skin to an Existing Costume

You can create an appearance for an existing costume by making `skin.json` instead of `costume.json`.

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

- `costume` is the id of the target costume. Use a built-in costume id (such as `PinkRabbit`) or, for another mod's costume, its runtime id including the mod name. (For the White Cat example, `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins`, and `flavorText` come from the existing costume as-is, so you can't set them.
- Overriding the default appearance of a built-in costume isn't supported.
