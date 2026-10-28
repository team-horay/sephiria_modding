# FAQ


## How do I view the logs?

Game logs are written to the file below. It's rewritten every time you launch the game, and the previous run is kept in `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Mod-related lines start with a tag in square brackets, so search for that tag.

| Tag | Contents |
|---|---|
| `[WorkshopModManager]` | Subscription list lookup, downloads, local folder scanning, load plan summary |
| `[WorkshopModRuntime]` | DLL activation and localization merge results |
| `[WorkshopUploader]` | Review submission and Workshop upload |
| `[ModGuardClient]` | Communication with the review server |
| `[WorkshopLobbyMods]` | Comparison of mod sets in multiplayer rooms |
| `모드 내 언어 파일 발견` | Number of keys read from each localization file |


## What is "Local mod (AddOns folder)" in the Steam Workshop panel?

You may see a "Local mod (AddOns folder)" entry in the panel. This is a legacy modding feature, so don't use it for new mods.
Legacy mods can't be synced with other players, so we recommend removing them.


## I want to move the WorkshopLocal folder somewhere other than the Sephiria install folder.

If you want to keep your mods on another drive or in a working folder, you can pass the path as a launch argument. In your Steam library, go to Sephiria → Properties → General → Launch Options and enter:

```
-workshop_local=D:\mods\my-first-mod
```

Separate multiple paths with semicolons, and wrap the whole thing in quotes if a path contains spaces.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Folders passed as arguments are treated exactly like subfolders of `WorkshopLocal/`.


## I lost uploadticket.json, mod-guard-manifest.json, and mod-guard.sig.json.

If only the certificate files (`mod-guard-manifest.json`, `mod-guard.sig.json`) are missing and `uploadticket.json` is still there, select the mod in the panel and press the **Get Approval Certificate** button to download them again from the review server. However, if you also lost `uploadticket.json` (for example, after reformatting), the game treats the folder as a new mod, so uploading it as is creates a new Workshop item instead of updating the existing one. To keep updating the existing item, recover it in this order:

1. Subscribe to your own mod on the Workshop.
2. Copy the entire folder Steam downloaded (`<Steam library>/steamapps/workshop/content/2436940/<Workshop item ID>/`) into `WorkshopLocal/`. Any folder name is fine.
3. In the copied folder, create `uploadticket.json` with the content below. For the number, use the digits after `id=` in the Workshop page URL.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. When you launch the game, the mod shows as "Not yet submitted for review" in the panel. Go through request review → approval → get approval certificate → upload to the Workshop again, and **the existing item is updated**. (Note: if you duplicated an existing mod folder to make a different mod, you **must delete** `uploadticket.json` and the certificate files. If you leave them, the new mod will overwrite the original mod's Workshop item.)
