# metadata.json

Die Datei `metadata.json` im Stammverzeichnis deines Mod-Ordners ist die zentrale Datei, die beschreibt, **was** deine Mod ist und **wie** sie läuft.


## Gesamtstruktur

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

Die oberste Ebene definiert drei Dinge.

| Schlüssel | Rolle | Erforderlich |
|---|---|---|
| `info` | Titel, Beschreibung und Vorschaubild, die im Steam-Workshop und im Panel im Spiel angezeigt werden | Erforderlich |
| `tags` | Workshop-Tags | Optional |
| `mod` | Wie die Mod läuft | Erforderlich |

## info

### title

- Der Name der Mod. Ist er leer, wird stattdessen die `modId` als Name verwendet.
- Du kannst eine Sprachzuordnung verwenden, um den Namen der Mod für jede Sprache festzulegen.
	- Zusätzlich zu deiner eigenen Sprache empfehlen wir, Englisch hinzuzufügen.
- Unterstützte Sprachcodes: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- Die Beschreibung der Mod.
- Wie bei `title` kannst du die Beschreibung pro Sprache festlegen.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Für den Upload in den Workshop ist ein Vorschaubild erforderlich. (PNG oder JPG, unter 1 MB)
- Gib den relativen Pfad zum Vorschaubild an.
- Fehlt die Angabe, sucht das Spiel im Stammordner in dieser Reihenfolge nach `thumbnail.png`, `preview.png`, `thumbnail.jpg` und `preview.jpg`.


## mod

### schemaVersion

- Die aktuelle Version ist 1.
- Beachte, dass diese Zahl in Zukunft steigen kann, wenn es größere Änderungen daran gibt, wie Mods laufen.

### modId

- Ein eindeutiger String, der deine Mod identifiziert. Verwende englische Kleinbuchstaben, Ziffern, Punkte, Bindestriche und Unterstriche. Bis zu 64 Zeichen.
- **Wähle einen eindeutigen Namen.** Kollidiert deine `modId` mit der einer anderen Mod, läuft sie nicht korrekt. Ein Präfix mit deinem Ersteller-Namen hilft sehr, Kollisionen zu vermeiden.
(z. B. `horay.artifact-pack`)
- **Ändere deine `modId` nach der Veröffentlichung nicht mehr.** Andere können mit `dependencies` Kind-Mods auf deiner Mod aufbauen, und sie identifizieren die Eltern-Mod über ihre `modId`.


### version

- Ein String, der die Version deiner Mod festlegt.
- Er wird verglichen, wenn eine andere Mod über `dependencies.version` eine exakte Version verlangt.
- **Erhöhe die Version bei jedem Update deiner Mod.** Beim Beitritt zu einer Mehrspieler-Lobby prüft das Spiel, ob Host und Teilnehmer dieselben Mod-Versionen haben. Änderst du den Inhalt, behältst aber die Version bei, können Spieler mit unterschiedlichem Mod-Inhalt in derselben Lobby landen, was die Inhaltssynchronisierung scheitern lassen oder den Spielfortschritt blockieren kann.
- Wir empfehlen dasselbe Format 1.0.0, das auch das Spiel verwendet.

### author

Lass alle wissen, wer du bist!

### multiplayer / installation

Legt fest, ob Spieler in einem Mehrspieler-Spiel diese Mod gemeinsam haben müssen. Erlaubte Werte:

| Wert | Beschreibung |
|---|---|
| `local_only` | Betrifft nur deinen eigenen Bildschirm und deine UI (Lokalisierungs-Mods und einfache UI-Mods fallen hierunter) |
| `everyone` | Alle im Raum müssen dieselbe Mod aktiviert haben (jede Mod, die Inhalte hinzufügt oder ändert, fällt hierunter) |
| `host_only` | Nur der Host muss sie aktiviert haben; bei den anderen Teilnehmern funktioniert es ohne Änderungen (Mehrspieler-Mods, die nur serverseitige Berechnungen betreffen, fallen hierunter) |

Wenn du `installation` als `local_only` oder `host_only` deklarierst, deine Mod aber Inhalte berührt, die auch die Teilnehmer brauchen (Registrieren neuer Gegenstände oder Einheiten, Netzwerkobjekte, Code, der auch auf den Clients der Teilnehmer läuft), spielen Spieler in derselben Lobby womöglich unterschiedliche Spiele, oder die Inhaltssynchronisierung scheitert und blockiert den normalen Spielfortschritt. Prüfe unbedingt, was deine Mod tatsächlich tut.


### modules

- Module legen fest, was deine Mod tut, und sind damit **der wichtigste Abschnitt**.
- Die aktuellen Module sind `localization, dll, costume, data`.
- Wie im folgenden Beispiel kannst du mehrere Module in einem Paket mischen.
- Ist auch nur ein Modul unbekannt, wird das gesamte Paket als „nicht unterstützt“ markiert und die Mod beim Laden übersprungen.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization-Modul

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- Das Modul `localization` ist für Mods, die Sprachdateien enthalten.
- Setze `path` auf den Ordner, der die Sprachdateien enthält.
- Lege **nach Sprachcodes benannte JSON-Dateien** wie `ko-KR.json` und `en-US.json` im `path`-Ordner ab. Die Sprachcodes und das Dateiformat des Spiels findest du unter [Lokalisierungs-Mods](localization-mods.md).

#### type: dll-Modul

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- Mit dem Modul `dll` kannst du dich mit C#-Code in das Spiel einklinken.
- Setze `path` auf den Pfad der DLL-Datei.
- Setze `entryClass` auf den vollständigen Namen der Einstiegsklasse in der DLL, einschließlich Namespace. Das Spiel durchsucht die DLL nicht nach einem Einstiegspunkt, du **musst ihn also angeben**. (Für verschachtelte Klassen verwende `Ns.Outer+Inner`.)
- Verwende nicht dieselbe `entryClass` in zwei verschiedenen Modulen.
- Funktioniert nur in Mono-Builds.
- Details findest du unter [DLL-Mods](dll-mods.md).

#### type: costume-Modul

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Ermöglicht es dir, eigene Kostüme zu erstellen, ohne C#-Code zu schreiben.
- Setze `path` auf den Ordner, der deine Kostümdateien enthält.
- Im Mehrspielermodus müssen alle sie installiert haben.
- Details findest du unter [Kostüm-Mods](costume-mods.md).


#### type: data-Modul

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Ein allgemeines Datenmodul für Mods. In typischen Fällen wird es nicht gebraucht; es dient hauptsächlich für Abhängigkeiten zwischen Mods.
- `consumer` gibt an, welche Mod dieses Datenmodul verwenden wird, wird aber normalerweise weggelassen. Fehlt die Angabe, kann jede in `dependencies` aufgeführte Mod diese Daten lesen.
- Hast du mehrere Eltern-Mods und sind die Daten nur für eine davon bestimmt, trage deren `modId` in `consumer` ein. Dieser Wert muss ebenfalls in `dependencies` stehen.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- Eine Liste von Hilfs-Assemblys, auf die dein `dll`-Modul verweist.
- Das Spiel löst Verweise **nur über die hier deklarierten Dateien** auf. Eine DLL einfach nur in den Ordner zu legen, reicht nicht, damit sie gefunden wird.
- Einträge können entweder ein String oder ein Objekt `{ "path": "..." }` sein.
- Jeder Eintrag muss auf `.dll` enden, und er läuft nicht korrekt, wenn er sich mit einer DLL in `modules` überschneidet.
- Bibliotheken werden nicht als Einstiegspunkte behandelt. Selbst wenn eine eine `WorkshopModBase`-Klasse enthält, wird diese nicht aufgerufen.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Wenn eine Eltern-Mod eine Mod zum „Erstellen von Inhalten“ ist, könnte eine Kind-Mod darauf aufbauend neue Inhalte hinzufügen.
- In diesem Fall kannst du mit `dependencies` eine Abhängigkeit von einer anderen Mod deklarieren.
- Ist eine Eltern-Mod angegeben, kann deine Mod nicht vor dieser Eltern-Mod geladen werden.
- Fehlt eine Eltern-Mod, ist sie deaktiviert oder schlägt ihr Laden fehl, wird das Laden aller ihrer Kind-Mods abgebrochen.
- Eine Abhängigkeit kann die folgenden drei Felder haben.

| Feld | Erforderlich | Regel |
|---|---|---|
| `modId` | Ja | Die `modId` der Mod, auf der du aufbaust |
| `version` | Nein | **Nur verwenden, wenn du einen exakt übereinstimmenden Versionsstring brauchst.** Weicht die Version ab, wird das Laden übersprungen, daher ist es meist sicherer, das Feld wegzulassen. |
| `workshopId` | Nein | Ermöglicht es Nutzern, die benötigte Mod zu finden und zu abonnieren. |


## tags

- Kategorien, die beim Upload in den Workshop verwendet werden.
- Eine Mod kann mehrere Tags haben. (Für eine Komplett-Mod, die Gegenstände und Kostüme hinzufügt, verwende `"tags": ["Item", "Costume"]`.)

| Kategorie | Inhalt |
|---|---|
| `Item` | Gegenstände (Artefakte, Steintafeln, Tränke usw.), Kombos |
| `Costume` | Kostüme |
| `Weapon` | Waffen |
| `NPC` | Reisende, Händler usw. |
| `Enemy` | Neue Gegner, Bosse usw. |
| `Gameplay` | Gameplay-Änderungen |
| `QoL` | Verbesserungen an UI, Komfort und Barrierefreiheit |
| `Library` | Laufzeitumgebungen, Frameworks, Datenpakete usw., von denen andere Mods abhängen |
| `Localization` | Übersetzungen |

## Pfadregeln
 
- Pfade in `modules[].path`, `libraries` und `info.preview` folgen alle denselben Regeln.
- Nur Pfade relativ zum Mod-Ordner sind erlaubt. (Absolute Pfade, Laufwerksbuchstaben und `..` werden abgelehnt.)
- Verwende `/` als Trennzeichen.
- Pfade, die über symbolische Links oder Junctions führen, werden abgelehnt.
- **Die Groß- und Kleinschreibung muss exakt mit dem tatsächlichen Namen übereinstimmen.** Unter Windows öffnet `localization` zwar trotzdem den Ordner, das Spiel lehnt es aber als abweichend von `Localization` ab. (Diese Regel sorgt für dasselbe Verhalten auf dem Steam Deck (Linux).)
