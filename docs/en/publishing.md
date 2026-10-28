# Publishing to the Workshop

This page explains how to upload your mod to the Steam Workshop, how the review system works, and how mods are synced between players in multiplayer.


## Before You Upload

- To upload a mod to the Workshop, a preview image (PNG or JPG, under 1 MB) is required. Set it with `info.preview`, or put a `thumbnail.png` in your mod folder. See the [metadata.json Reference](metadata.md#preview) for details.
- `info.title` can be up to 128 characters and `info.description` up to 7999 characters. If you wrote them in multiple languages, the `en-US` version is used on the Workshop page.
- Put Workshop categories in `tags`. See the [metadata.json Reference](metadata.md) for the available categories.
- Every file in the folder is subject to review, so make sure your mod folder doesn't contain copies of game DLLs, `.pdb` files, or intermediate build output.

## Publishing Steps

1. Launch Sephiria.
2. Open the **Steam Workshop** panel on the title screen and find the mod you made.
3. Select your mod.
4. Mods that include code (DLLs) go through code review.
5. Mods without code can be uploaded to the Workshop right away.
6. Go to the uploaded Workshop page, do a final test such as subscribing to it, then change the visibility to "Public".

## Code Review

- The review system checks every file in your mod folder. DLLs are decompiled and checked for network communication, file access, obfuscation, and malicious behavior; data files such as images, sounds, and JSON are checked for valid format.
- If no issues are found, the mod is approved automatically; if anything looks suspicious, an operator checks it manually. Clearly malicious mods are rejected automatically.
- Review combines automated analysis with operator checks, and we understand that no review system can catch every risk. Still, we aim to reduce the spread of malicious mods through this up-front review and approval process. Thank you for your cooperation.
- Whether a mod contains code is determined by file **content**, not file name. Even without a `dll` module, a mod is subject to review if it has `libraries` or contains a DLL or EXE with a changed extension.
- Mods that go through code review must be 20 MB or less when the folder is compressed.

## Approval Certificate (Mods with Code Only)

- Mods that contain code receive an approval certificate.
- It certifies that the mod's files are identical to the approved version.
- DLL mods must go through this certification before players who download the mod can run its code.
- If even one file changes after certification, the certificate becomes invalid and the mod must be reviewed again.

## Visibility and Going Public

- When you first upload, the visibility is set to `Unlisted`.
- Before going public, subscribe to it yourself, check that the installed copy works properly, then change it to "Public".
- You can edit the page description, screenshots, descriptions in other languages, and so on from the Steam Workshop item page.

## Updating Your Mod

You can update your mod the same way you published it.

- DLL mods need a new approval certificate.
- Don't change your `modId`. The mod will be treated as a different mod, which can break dependencies and more.
- **Always bump `version` in `metadata.json` with every update.** Mod versions are compared when joining a multiplayer lobby, so if the content changed but the version didn't, players with different mod content can end up in the same game and run into problems.

## Upload Ticket

After you request review and upload, the files below are added to your mod folder. They contain your review approval information, so **don't delete them.** If you manage your mod folder with git, it's safest to commit these three files as well. If you switch computers or reformat, you'll need these files to keep updating the same Workshop item.

| File | Contents |
|---|---|
| `uploadticket.json` | The review ticket and Workshop item ID. Without it, the game treats the folder as a new mod that has never been published. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | The review approval certificate: the list of approved files and a signature. Editing them invalidates the approval. |

See the [FAQ](faq.md) for how to recover these files if you lose them.

## Approval Revocation

If a rule violation is found after publishing, an operator can revoke the approval. A revoked mod isn't loaded even if it's installed, and it's shown in the panel as "Review approval revoked".


## Multiplayer Rules

### Multiplayer Lobby Setup

When the host creates a multiplayer lobby, **the list of mods the host has enabled** is registered with the lobby.

- Mods with `installation` set to `host_only`: participants aren't required to install them; they're only shown in the list.
- Mods with `multiplayer.installation` set to `everyone` (or not declared): participants must install them too.
- Costume mods and data mods: participants must install them too.

### Joining

If a player's current mod setup differs from the lobby's, they go through these steps: download → enable → restart the game and try joining again.

### Test Mods Installed in WorkshopLocal

Multiplayer isn't available in a game that has local mods from `WorkshopLocal/` loaded. To test multiplayer, upload your mod to the Workshop, clear out the local folder, and use the subscribed copy. You can subscribe via the link even while the item is `Unlisted`.
