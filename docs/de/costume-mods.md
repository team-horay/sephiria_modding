# Kostüm-Mods

Fügen wir ein Kostüm hinzu, nur mit PNG- und JSON-Dateien und ganz ohne Code. Mit dieser Art von Mod kannst du die folgenden zwei Dinge hinzufügen:

- **Neue Kostüme**: Erstelle ein komplett neues Kostüm. Du kannst Aussehen, Werte, Startgegenstände und mehr festlegen.
- **Aussehen für bestehende Kostüme**: Füge einem bestehenden Kostüm wie dem Rosa Häschen ein Aussehen (Skin) hinzu.

Diese Seite basiert auf dem Beispiel [`examples/costume-sample`](../../examples/costume-sample). Lade es herunter, um ein Gefühl für den Aufbau der Mod zu bekommen.

## Aufbau der Mod

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← Kostümordner
│   └── WhiteCat/              ← ein Ordner = ein Kostüm
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← weitere Bewegungs-PNGs
└── Localization/              ← Name und Beschreibung des Kostüms
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Wenn du aus dem Beispiel deine eigene Mod baust, ändere `modId` und `author` in `metadata.json`. (Details siehe [`metadata.json`](metadata.md).)

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


## Sprite-Animationen

- Ein Kostüm braucht ziemlich viele Animationen, wenn du alle einbaust.
- Ist dies dein erstes Kostüm, versuche es mit einem einfachen, das nur `idle, move, airborne, fall` enthält.

| Bewegung | Beschreibung | Erforderlich |
|---|---|---|
| `idle` | Stehen | Erforderlich |
| `move` | Laufen | Erforderlich |
| `airborne` | In der Luft | Optional |
| `fall` | Zusammenbrechen | Optional |
| `attack` | Angriff | Optional |
| `greatsword_lower` | Drehschlag | Optional |
| `greatsword_sweep` | Drehung | Optional |
| `hitfeedback` | Getroffen werden | Optional |

### Rückansicht-Animationen

- Für ein natürlicher wirkendes Kostüm kannst du Sprites in Rückansicht zeichnen. Das ist optional.
- Wenn du Rückansicht-Sprites hinzufügst, musst du die Bewegungen `idle_back, move_back` einbauen.
- Rückansicht-Animationen sind wie folgt definiert.

| Bewegung | Beschreibung |
|---|---|
| `idle_back` | Stehen (Rückansicht) |
| `move_back` | Laufen (Rückansicht) |
| `attack_back` | Angriff (Rückansicht) |
| `greatsword_upper` | Drehschlag (Rückansicht) |

## Die Grafiken vorbereiten

- Erstelle für jede Bewegung ein PNG-Sprite-Sheet, in dem die Frames dieser Bewegung horizontal nebeneinander liegen.
- Die Bildhöhe entspricht der Frame-Höhe, die Breite ist Frame-Breite × Frame-Anzahl. Lass keine Lücken zwischen den Frames.
- Wir empfehlen, alle Einzelbilder gleich groß zu machen.
- Im Beispiel „Weiße Katze“, in dem jedes Sprite 20×16 groß ist, haben die Bilder folgende Größen.

| Datei | Größe | Frames |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Beispielbild*


### Die Infos des neuen Kostüms schreiben

Definiere dein eigenes Kostüm, indem du eine Datei `costume.json` schreibst.

Beispiel `Costumes/WhiteCat/costume.json`:

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

| Feld | Erforderlich | Beschreibung |
|---|---|---|
| `schemaVersion` | Ja | Formatkennung. Trage die Ganzzahl `1` ein. |
| `id` | Ja | Verwende englische Kleinbuchstaben, Ziffern, Unterstriche und Bindestriche, 1–31 Zeichen. Um Kollisionen zu vermeiden, wird es im Spiel als `<modId>:<id>` registriert. |
| `name` | Ja | Der **Lokalisierungsschlüssel** für den Kostümnamen. Siehe [Lokalisierungsdateien vorbereiten](#lokalisierungsdateien-vorbereiten) weiter unten. |
| `flavorText` | Nein | Der **Lokalisierungsschlüssel** für die Kostümbeschreibung. Siehe [Lokalisierungsdateien vorbereiten](#lokalisierungsdateien-vorbereiten) weiter unten. |
| `icon` | Ja | Pfad zum PNG des Vorschausymbols des Kostüms. |
| `face` | Nein | Pfad zum Gesichts-PNG für das Mehrspieler-HUD. |
| `pixelsPerUnit` | Nein | Ändere diesen Wert, um detailliertere Grafiken zu verwenden. Höhere Werte rendern die Grafik feiner. Der Standard-PPU ist 16. |
| `frameSize` | Nein | `[Breite, Höhe]` jedes Sprites. Standard: `[16, 16]`. |
| `pivot` | Nein | Der Drehpunkt jedes Sprites. Unten links ist `[0, 0]`. Standard: `[Breite/2, 0]`. |
| `animations` | Ja | Grafiken für jede Bewegung. Siehe [animations](#animations). |
| `stats` | Nein | Liste der zu setzenden Werte. Siehe die [Werteliste](stats.md). |
| `startingItems` | Nein | Array von Startgegenstand-IDs. Siehe die [Gegenstandsliste](item-table.md). |
| `body` | Nein | Legt die Hitbox fest. Siehe [body](#body). |
| `skins` | Nein | Zusätzliche Aussehen für dasselbe Kostüm. Siehe [skins](#skins). |

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

- Definiere die oben vorbereiteten Animationen mit vier Eigenschaften: `{ "file", "fps", "loop", "footstepFrames" }`.
- Du kannst `frameSize` und `pivot` auch für jede Bewegung einzeln festlegen.
- Hast du nur eine Bewegung in einer anderen Größe gezeichnet, wie in der letzten Zeile, setze `frameSize` und `pivot` für diese Bewegung.

#### Eigenschaften

| Feld | Erforderlich | Beschreibung |
|---|---|---|
| `file` | Ja | Der Name der abzuspielenden Datei. |
| `fps` | Nein | Abspielgeschwindigkeit der Animation. (Standard: 12) |
| `loop` | Nein | Ob die Animation in Schleife läuft. (Standard: true; false bei Angriffsanimationen) |
| `footstepFrames` | Nein | Frame-Nummern, beginnend bei null. Wenn diese Frames angezeigt werden, wird ein Schrittgeräusch abgespielt. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Legt die Bewegungs- und Trefferkollisionsbereiche des Charakters fest.
- Die eingebauten Kostüme verwenden diese Werte als Standard.
- Ohne besonderen Grund empfehlen wir, dies wegzulassen.

#### Eigenschaften

| Feld | Erforderlich | Beschreibung |
|---|---|---|
| `hitboxRadius` | Nein | Radius des Hitbox-Kreises in Pixeln, 0,5–64. Standard `4`. |
| `hitboxOffset` | Nein | Mittelpunkt des Hitbox-Kreises, `[x, y]`, in Pixeln relativ zum Drehpunkt; positives x ist rechts, positives y ist oben, ±64. Standard `[0, 4]`. |
| `movementRadius` | Nein | Radius des Bewegungskollisionskreises, der mit Wänden und Hindernissen kollidiert, in Einheiten (16 Pixel = 1), 0,05–2. Standard `0.25`. |
| `bodyScale` | Nein | Skalierung der Schulterposition, an der die Waffe gehalten wird, 0,1–4. Standard `1`. Erhöhe den Wert, wenn die Waffe bei einem großen Kostüm im Körper versinkt. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Fügt demselben Kostüm zusätzliche Aussehen hinzu.
- Animationen folgen demselben Format wie oben.
- Lässt du `frameSize` oder `pivot` weg, werden die Werte des Kostüms selbst verwendet.




## Lokalisierungsdateien vorbereiten

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

- Schreibe in `name` und `flavorText` statt Text einen **Lokalisierungsschlüssel** und lege den Text im Ordner `Localization/` derselben Mod ab, eine Datei pro Sprache.
- Verwende für Schlüssel englische Buchstaben, Ziffern, Unterstriche und Bindestriche, ohne Leerzeichen. Den Namen deiner Mod einzubauen hilft, Kollisionen mit anderen Mods zu vermeiden.
- Ist ein Schlüssel **in keiner Sprachdatei** enthalten, wird das Kostüm nicht geladen. Du musst mindestens eine Sprache bereitstellen.
- Wir empfehlen, auch Englisch (`en-US`) einzubauen, damit mehr Leute dein Kostüm nutzen können.
- Vergiss nicht, in `metadata.json` unter `modules` auch ein `localization`-Modul zu deklarieren.



## Prüfen

1. Lege deinen Mod-Ordner in `WorkshopLocal/` und starte das Spiel.
2. Starte ein Spiel, öffne im Dorf die Kostümauswahl (**Kostüm wechseln**) und prüfe, ob dein neues Kostüm hinzugefügt wurde.
3. Wenn du den Inhalt des Kostüms änderst, musst du das Spiel neu starten.



## Extra: Nur einen Skin zu einem bestehenden Kostüm hinzufügen

Du kannst ein Aussehen für ein bestehendes Kostüm erstellen, indem du statt `costume.json` eine `skin.json` anlegst.

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

- `costume` ist die ID des Zielkostüms. Verwende die ID eines eingebauten Kostüms (etwa `PinkRabbit`) oder, für das Kostüm einer anderen Mod, dessen Laufzeit-ID einschließlich Mod-Name. (Für das Beispiel „Weiße Katze“: `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` und `flavorText` werden unverändert vom bestehenden Kostüm übernommen, du kannst sie also nicht setzen.
- Das Überschreiben des Standard-Aussehens eines eingebauten Kostüms wird nicht unterstützt.
