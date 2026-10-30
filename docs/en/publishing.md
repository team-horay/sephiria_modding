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
3. Select your mod and press **Upload to Workshop**.
4. For mods without code, the Workshop upload is the last step.
5. For mods that include code (DLLs), once the Workshop upload finishes, the mod is automatically submitted to the **review system** together with its Workshop item ID.
6. Go to the uploaded Workshop page, do a final test such as subscribing to it, then change the visibility to "Public".

If the Workshop upload finished but only the review submission failed (for example, because of a network error), press **Upload to Workshop** again on the same mod. If the content hasn't changed, the mod isn't uploaded to the Workshop again; only the submission is resent. The submission must be made with the Steam account that created the Workshop item.

## Code Review

- The review system decompiles DLLs and checks them for network communication, file access, obfuscation, and malicious behavior. Review combines automated analysis with operator checks, and we understand that no review system can catch every risk. Still, we aim to reduce the spread of malicious mods through this review process. Thank you for your cooperation.
- Every mod that contains code (DLLs) **must also be submitted to the mod review system.** To do this, always upload through the game's **Upload to Workshop** button.
- Mods that were uploaded to the Workshop but not submitted to the review system generally can't be run by players.
- Players who download a mod look up its review status using the mod's Workshop item ID and the installed DLLs.
  - **Submitted / Approved**: The mod runs. It runs even before the review is finished.
  - **Not submitted / Rejected**: The mod doesn't run, and the reason is shown in the mod panel.
- If a problem is found during review, a mod can be rejected even after it has been published. For a rejected mod, you can see the reason with **View Rejection Reason** in the panel. The rejected DLLs can't be published again as they are; fix the issue and upload again.
- Even without a `dll` module, a mod is subject to review if it has `libraries` or contains a DLL or EXE with a changed extension.
- Mods that go through code review must be 20 MB or less when the folder is compressed.

### Review Failure Reasons

|Message|Cause|
|---|---|
|There is no submission record in the review system.|The mod was uploaded to the Workshop but not submitted to the review system. Submit it again with the **Upload to Workshop** button.|
|The review system rejected this mod's approval.|The operations team rejected the mod for some reason. Check the rejection reason.|
|Could not get information from the review server.|The subscriber's internet connection may be down, or there may be a problem with the review server.|
|This mod was made for the previous review system.|The review server was upgraded and the mod's existing verification data was reset. Submit it again.|
|An unauthorized DLL was detected in the mod.|If the set of DLL files in the mod changes while the mod is running, loading that mod is restricted. In other words, the following can violate the review system: the mod downloads a DLL from outside while running and loads the assembly; the mod creates and runs its own DLL while running; the mod extracts a DLL packed inside it with its own compression scheme and uses it.|

## Visibility and Going Public

- When you first upload, the visibility is set to `Unlisted`.
- Before going public, subscribe to it yourself, check that the installed copy works properly, then change it to "Public".
- You can edit the page description, screenshots, descriptions in other languages, and so on from the Steam Workshop item page.

## Updating Your Mod

You can update your mod the same way you published it.

- An update that changes DLLs is automatically resubmitted after upload and goes through review again. If you didn't change any DLLs, the review status stays the same.
- Don't change your `modId`. The mod will be treated as a different mod, which can break dependencies and more.
- **Always bump `version` in `metadata.json` with every update.** Mod versions are compared when joining a multiplayer lobby, so if the content changed but the version didn't, players with different mod content can end up in the same game and run into problems.

## Upload Ticket

After you upload, `uploadticket.json` is added to your mod folder. It contains your Workshop item ID and review submission information, so **don't delete it.** If you manage your mod folder with git, it's safest to commit this file as well. If you switch computers or reformat, you'll need this file to keep updating the same Workshop item. Without it, the game treats the folder as a new mod that has never been published.

`mod-guard-manifest.json` and `mod-guard.sig.json`, used by the review system before 1.1.1, are no longer used. Even if they remain, they aren't uploaded to the Workshop, and you can delete them.

See the [FAQ](faq.md) for how to recover the file if you lose it.


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
