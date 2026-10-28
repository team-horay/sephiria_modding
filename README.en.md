[한국어](README.md) | **English** | [日本語](docs/ja/README.md) | [简体中文](docs/zh-CN/README.md) | [繁體中文](docs/zh-TW/README.md) | [Deutsch](docs/de/README.md) | [Français](docs/fr/README.md) | [Español](docs/es/README.md) | [Italiano](docs/it/README.md) | [Polski](docs/pl/README.md) | [Português (Brasil)](docs/pt-BR/README.md) | [Русский](docs/ru/README.md) | [Svenska](docs/sv/README.md) | [ไทย](docs/th/README.md) | [Türkçe](docs/tr/README.md)

# Sephiria Modding Guide

Thank you for joining the Sephiria modding community.

This is the official repository of modding guides, examples, and templates for [Sephiria](https://store.steampowered.com/app/2436940). Mods can be shared through the [Steam Workshop](https://steamcommunity.com/app/2436940/workshop/).



## Rules

To keep the game safe for everyone, please follow the rules below. Violations may result in restrictions on publishing mods or playing the game.

1. **No malicious behavior**: Malware, backdoors, keyloggers, crypto miners, and any other feature that compromises a user's device, account, or data are prohibited.
2. **Non-commercial use**: All mods must be made and distributed for non-commercial purposes.
3. **No rights infringement or harmful content**: Content that infringes on others' copyright, privacy, or likeness rights, or that contains sexual content or promotes hate, harassment, or real-world violence, is prohibited.
4. **No disrupting play**: Do not intentionally make the game malfunction or interfere with other players' normal play.
5. **No data collection**: Do not collect user information or send it to external destinations.
6. **No bypassing access checks**: Do not illegitimately bypass purchase, ownership, or access checks of the game or the distribution platform.
7. **Mod review**: Mods that contain code (DLLs) go through a review before approval, including analysis and decompilation, to check for rule violations. Approval may take some time. Do not forge review results or approval marks. Files must be provided in a reviewable state; obfuscation is prohibited.
8. **Use of game assets**: Decompiling and analyzing the game's code, art, sound, and other assets is allowed to the extent needed for making mods. However, do not redistribute extracted game assets themselves.
9. **No impersonation or tampering**: Do not impersonate others, or modify and redistribute mods made by others without their consent.



## Before You Start

- Mods that contain code (DLLs) are **reviewed** before they can be uploaded to the Workshop. The review mainly checks the code for malicious behavior. Mods without code, such as translations and costumes, can be uploaded right away without review. Note that mods the review system cannot read will be rejected (obfuscated files, DLLs compiled from C/C++, etc.).
- In multiplayer, every participant in the room must have the same set of mods enabled. (Personal UI mods such as translations are exempt.)
- After enabling mods, you need to restart the game to change their order or disable them again.
- Subscribed mods are disabled by default. You can enable them yourself from the list.



## Documentation

| Document | Contents |
|---|---|
| [Your First Mod](docs/en/getting-started.md) | Setting up a development folder, checking a mod locally, reading logs |
| [metadata.json Reference](docs/en/metadata.md) | What every field means, dependency and version rules, module declarations |
| [Localization Mods](docs/en/localization-mods.md) | Language file structure, finding keys, rules for preserving tags and placeholders |
| [Costume Mods](docs/en/costume-mods.md) | Making your own costume |
| [DLL Mods](docs/en/dll-mods.md) | Project setup, entry point, `HorayModAPI` events, debugging |
| [Publishing to the Workshop](docs/en/publishing.md) | Uploading to the Workshop, code mod review, multiplayer rules |
| [API Reference](docs/en/api/README.md) | Public types and members such as `HorayModAPI`, `WorkshopModBase`, mod options, and UI. Auto-generated from the game source |
| [Appendix & FAQ](docs/en/faq.md) |  |


## License

The code in this repository (examples and templates) is licensed under the [MIT License](LICENSE).
The documentation may be freely quoted, translated, and redistributed; please credit the source.
The Sephiria game itself and its assets are works of TEAM HORAY and are not covered by this license.
