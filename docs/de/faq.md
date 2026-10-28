# FAQ


## Wie sehe ich mir die Logs an?

Die Spiel-Logs werden in die folgende Datei geschrieben. Sie wird bei jedem Spielstart neu geschrieben, der vorherige Durchlauf bleibt in `Player-prev.log` erhalten.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Mod-bezogene Zeilen beginnen mit einem Tag in eckigen Klammern, suche also nach diesem Tag.

| Tag | Inhalt |
|---|---|
| `[WorkshopModManager]` | Abfrage der Abonnementliste, Downloads, Scannen des lokalen Ordners, Zusammenfassung des Ladeplans |
| `[WorkshopModRuntime]` | Ergebnisse der DLL-Aktivierung und des Zusammenführens der Lokalisierung |
| `[WorkshopUploader]` | Einreichung zur Prüfung und Workshop-Upload |
| `[ModGuardClient]` | Kommunikation mit dem Prüfserver |
| `[WorkshopLobbyMods]` | Vergleich der Mod-Sets in Mehrspieler-Räumen |
| `모드 내 언어 파일 발견` (koreanisch für „Sprachdatei in Mod gefunden“) | Anzahl der aus jeder Lokalisierungsdatei gelesenen Schlüssel |


## Was ist „Lokaler Mod (AddOns-Ordner)“ im Steam-Workshop-Panel?

Im Panel kann ein Eintrag „Lokaler Mod (AddOns-Ordner)“ auftauchen. Das ist eine veraltete Modding-Funktion, verwende sie also nicht für neue Mods.
Veraltete Mods können nicht mit anderen Spielern synchronisiert werden, daher empfehlen wir, sie zu entfernen.


## Ich möchte den WorkshopLocal-Ordner an einen anderen Ort als den Sephiria-Installationsordner verschieben.

Wenn du deine Mods auf einem anderen Laufwerk oder in einem Arbeitsordner aufbewahren willst, kannst du den Pfad als Startparameter übergeben. Gehe in deiner Steam-Bibliothek zu Sephiria → Eigenschaften → Allgemein → Startoptionen und trage ein:

```
-workshop_local=D:\mods\my-first-mod
```

Trenne mehrere Pfade mit Semikolons und setze das Ganze in Anführungszeichen, wenn ein Pfad Leerzeichen enthält.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Als Parameter übergebene Ordner werden genauso behandelt wie Unterordner von `WorkshopLocal/`.


## Ich habe uploadticket.json, mod-guard-manifest.json und mod-guard.sig.json verloren.

Wenn nur die Nachweisdateien (`mod-guard-manifest.json`, `mod-guard.sig.json`) fehlen und `uploadticket.json` noch vorhanden ist, wähle die Mod im Panel aus und drücke die Schaltfläche **Freigabenachweis abrufen**, um sie erneut vom Prüfserver herunterzuladen. Hast du aber auch `uploadticket.json` verloren (zum Beispiel nach einer Neuinstallation), behandelt das Spiel den Ordner als neue Mod. Ein Upload in diesem Zustand erstellt also einen neuen Workshop-Beitrag, statt den bestehenden zu aktualisieren. Um den bestehenden Beitrag weiter aktualisieren zu können, stelle ihn in dieser Reihenfolge wieder her:

1. Abonniere deine eigene Mod im Workshop.
2. Kopiere den gesamten von Steam heruntergeladenen Ordner (`<Steam-Bibliothek>/steamapps/workshop/content/2436940/<Workshop-Beitrags-ID>/`) nach `WorkshopLocal/`. Der Ordnername ist beliebig.
3. Erstelle im kopierten Ordner eine `uploadticket.json` mit dem folgenden Inhalt. Als Zahl verwendest du die Ziffern nach `id=` in der URL der Workshop-Seite.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Beim Spielstart wird die Mod im Panel als „Nicht eingereicht“ angezeigt. Durchlaufe erneut Prüfung anfordern → Freigabe → Freigabenachweis abrufen → In den Workshop hochladen, und **der bestehende Beitrag wird aktualisiert**. (Hinweis: Wenn du einen bestehenden Mod-Ordner dupliziert hast, um eine andere Mod zu erstellen, **musst** du `uploadticket.json` und die Nachweisdateien **löschen**. Lässt du sie stehen, überschreibt die neue Mod den Workshop-Beitrag der ursprünglichen Mod.)
