# Deine erste Mod

Dieser Leitfaden erklärt den grundlegenden Aufbau einer Sephiria-Mod und führt dich Schritt für Schritt durch das Erstellen deiner ersten Mod und das Prüfen im Spiel.

## Was du brauchst

- Einen brauchbaren Texteditor (zum Beispiel [Visual Studio Code](https://code.visualstudio.com/))

## Den Installationsordner des Spiels finden

Öffne den Installationsordner von Sephiria.

- Über deine Steam-Bibliothek
  - `Steam-Bibliothek → Rechtsklick auf Sephiria → Verwalten → Lokale Dateien durchsuchen`
- Windows (Standard-Installationspfad)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (Standard-Installationspfad)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (der Ordner, der `Sephiria.app` enthält)

## Den WorkshopLocal-Ordner und deinen eigenen Mod-Ordner anlegen

1. Lege im Installationsordner des Spiels einen Ordner `WorkshopLocal/` an.
2. Erstelle darin einen Ordner für deine eigene Mod.
   - Sephiria behandelt jeden Ordner als eine Mod.
   - Mods, die hier erstellt werden, können im Spiel getestet und in den Steam-Workshop hochgeladen werden.

In diesem Leitfaden baust du `my-first-mod`, eine Mod, die den Text der Schaltfläche „Spiel starten“ auf dem Titelbildschirm in „Hello World!“ ändert. Die fertige Struktur sieht so aus:

```
<Installationsordner des Spiels>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← eine Mod
    │   ├── metadata.json      ← die zentrale Datei, die die Mod definiert
    │   ├── thumbnail.png      ← Vorschaubild der Mod (für den Workshop-Upload erforderlich)
    │   └── Localization/      ← Ordner, den das Lokalisierungsmodul liest
    │       └── en-US.json     ← der zu ersetzende Text
    └── another-mod/           ← eine weitere Mod
        └── ...
```

## Die Datei metadata.json

- Erstelle `metadata.json` in deinem Mod-Ordner und füge den folgenden Inhalt ein.
- `metadata.json`: die zentrale Datei, die deine Mod definiert.
- `yourname`: ersetze dies durch deinen eigenen Nicknamen.
- Speichere die Datei als **UTF-8**. Im Windows-Editor (Notepad) wählst du unter „Speichern unter“ die Codierung UTF-8.
- Was die einzelnen Felder bedeuten, steht in der [metadata.json-Referenz](metadata.md).

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

## Die Sprachdatei erstellen

- Lege in deinem Mod-Ordner einen Ordner `Localization/` an.
- Erstelle in `Localization/` eine Datei `en-US.json` mit dem folgenden Inhalt.
- Zum Testen kannst du die Datei auch nach deinem eigenen Sprachcode umbenennen.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Prüfen

Jetzt kannst du prüfen, ob die Mod funktioniert.

1. Starte Sephiria.
2. Stelle die Spielsprache auf Englisch und sieh dir den Titelbildschirm an: Die Schaltfläche „Start Game“ zeigt jetzt „Hello World!“. Mods in `WorkshopLocal/` sind standardmäßig aktiviert, du musst sie also nicht einschalten.
3. Drücke auf dem Titelbildschirm die Schaltfläche **Steam Workshop**, um das Workshop-Panel zu öffnen. „My First Mod“ sollte im Abschnitt „Meine Mods“ erscheinen.

### Meine Mod wird nicht angewendet!

- Prüfe, ob deine JSON-Syntax korrekt ist.
- Der `path` in `metadata.json` muss exakt mit dem tatsächlichen Ordnernamen übereinstimmen, einschließlich Groß- und Kleinschreibung.
- Prüfe, ob `metadata.json` als UTF-8 codiert ist.
- Prüfe, ob die Spielsprache zur Sprache im Dateinamen passt.

## Nächste Schritte

1. Lerne in der [metadata.json-Referenz](metadata.md), wie Mods definiert werden.
2. [Lokalisierungs-Mods](localization-mods.md) und [Kostüm-Mods](costume-mods.md) sind relativ einfach zu erstellen, fang also damit an.
3. [DLL-Mods](dll-mods.md) sind empfehlenswert, wenn du Grundkenntnisse im Programmieren hast.
4. Veröffentliche deine Mod im Workshop mit [Im Workshop veröffentlichen](publishing.md).
