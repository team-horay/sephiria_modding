# metadata.json

The `metadata.json` file at the root of your mod folder is the core file that describes both **what** your mod is and **how** it runs.


## Overall Structure

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

The top level defines three things.

| Key | Role | Required |
|---|---|---|
| `info` | Title, description, and preview shown on the Steam Workshop and in the in-game panel | Required |
| `tags` | Workshop tags | Optional |
| `mod` | How the mod runs | Required |

## info

### title

- The mod's name. If empty, `modId` is used as the name instead.
- You can use a language map to define the mod's name for each language.
	- In addition to your own language, we recommend adding English.
- Supported language codes: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- The mod's description.
- Like `title`, you can define the description per language.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- A preview image is required to upload to the Workshop. (PNG or JPG, under 1 MB)
- Enter the relative path to the preview image.
- If omitted, the game looks for `thumbnail.png`, `preview.png`, `thumbnail.jpg`, and `preview.jpg` in the root folder, in that order.


## mod

### schemaVersion

- The current version is 1.
- Note that this number may increase in the future if there are major changes to how mods run.

### modId

- A unique string that identifies your mod. Use lowercase English letters, digits, periods, hyphens, and underscores. Up to 64 characters.
- **Pick a unique name.** If your `modId` collides with another mod's, it won't run correctly. Prefixing it with your creator name goes a long way toward avoiding collisions.
(e.g. `horay.artifact-pack`)
- **Don't change your `modId` once published.** Others can build child mods on top of yours using `dependencies`, and they identify the parent mod by its `modId`.


### version

- A string that defines your mod's version.
- It's what gets compared when another mod requires an exact version via `dependencies.version`.
- **Bump the version every time you update your mod.** When joining a multiplayer lobby, the game checks that the host and participants have the same mod versions. If you change the content but keep the same version, players with different mod content can end up in the same lobby, which can cause content sync to fail or block game progress.
- We recommend the same 1.0.0 format the game uses.

### author

Let everyone know who you are!

### multiplayer / installation

Defines whether players in a multiplayer game need to share this mod. Allowed values:

| Value | Description |
|---|---|
| `local_only` | Affects only your own screen and UI (localization mods and simple UI mods fall here) |
| `everyone` | Everyone in the room must have the same mod enabled (any mod that adds or changes content falls here) |
| `host_only` | Only the host needs it enabled; works without changes on other participants' side (multiplayer mods that only affect server-side computation fall here) |

If you declare `installation` as `local_only` or `host_only` but your mod touches content that participants also need (registering new items or units, network objects, code that also runs on participants' clients), players in the same lobby may end up playing different games, or content sync may fail and block normal game progress. Be sure to check what your mod actually does.


### modules

- Modules define what your mod does, which makes this **the most important section**.
- The current modules are `localization, dll, costume, data`.
- As in the example below, you can mix multiple modules in one package.
- If even one module is unknown, the whole package is marked "unsupported" and the mod is skipped during loading.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization module

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- The `localization` module is for mods that include language files.
- Set `path` to the folder that contains the language files.
- Put **JSON files named after language codes**, such as `ko-KR.json` and `en-US.json`, inside the `path` folder. See [Localization Mods](localization-mods.md) for the language codes and file format the game uses.

#### type: dll module

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- The `dll` module lets you hook into the game with C# code.
- Set `path` to the path of the DLL file.
- Set `entryClass` to the full name of the entry point class in the DLL, including its namespace. The game doesn't search the DLL for an entry point, so you **must specify it**. (For nested classes, use `Ns.Outer+Inner`.)
- Don't use the same `entryClass` in two different modules.
- Works only on Mono builds.
- See [DLL Mods](dll-mods.md) for details.

#### type: costume module

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Lets you create your own costumes without writing any C# code.
- Set `path` to the folder that contains your costume files.
- In multiplayer, everyone must have it installed.
- See [Costume Mods](costume-mods.md) for details.


#### type: data module

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- A general-purpose data module for mods. It isn't used in typical cases; it's mainly used for dependencies between mods.
- `consumer` specifies which mod will use this data module, but you usually omit it. If omitted, every mod listed in `dependencies` can read this data.
- If you have multiple parent mods and the data is meant for only one of them, put that mod's `modId` in `consumer`. That value must also be in `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- A list of helper assemblies referenced by your `dll` module.
- The game resolves references using **only the files declared here**. Just placing a DLL in the folder isn't enough for it to be found.
- Entries can be either a string or a `{ "path": "..." }` object.
- Each entry must end in `.dll`, and it won't run correctly if it overlaps with a DLL in `modules`.
- Libraries aren't treated as entry points. Even if one contains a `WorkshopModBase` class, it won't be called.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- If a parent mod is a "content creation" mod, a child mod might add new content built on top of it.
- In that case, you can declare a dependency on another mod using `dependencies`.
- When a parent mod is specified, your mod can't be loaded before that parent mod.
- If a parent mod is missing, disabled, or fails to load, loading of all its child mods is canceled.
- A dependency can have the following three fields.

| Field | Required | Rule |
|---|---|---|
| `modId` | Yes | The `modId` of the mod you build on |
| `version` | No | **Use only when you need an exact matching version string.** If the version differs, loading is skipped, so it's usually safer to leave it out. |
| `workshopId` | No | Lets users find and subscribe to the required mod. |


## tags

- Categories used when uploading to the Workshop.
- A mod can have multiple tags. (For an all-in-one mod that adds items and costumes, use `"tags": ["Item", "Costume"]`.)

| Category | Content |
|---|---|
| `Item` | Items (artifacts, tablets, potions, etc.), combos |
| `Costume` | Costumes |
| `Weapon` | Weapons |
| `NPC` | Travelers, merchants, etc. |
| `Enemy` | New enemies, bosses, etc. |
| `Gameplay` | Gameplay changes |
| `QoL` | UI, convenience, and accessibility improvements |
| `Library` | Runtimes, frameworks, data packs, etc. that other mods depend on |
| `Localization` | Translations |

## Path Rules
 
- Paths in `modules[].path`, `libraries`, and `info.preview` all follow the same rules.
- Only paths relative to the mod folder are allowed. (Absolute paths, drive letters, and `..` are rejected.)
- Use `/` as the separator.
- Paths that go through symbolic links or junctions are rejected.
- **Case must match the actual name exactly.** On Windows, writing `localization` still opens the folder, but the game rejects it as different from `Localization`. (This rule ensures the same behavior on Steam Deck (Linux).)
