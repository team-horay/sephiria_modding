# Lokalisierungs-Mods

[Deine erste Mod](getting-started.md) hat die Grundlagen des Mod-Erstellens behandelt. Diese Seite beschreibt die allgemeinen Regeln für Lokalisierungs-Mods.
[`examples/translation-sample`](../../examples/translation-sample) enthält ein vollständiges Beispiel, das du direkt nach `WorkshopLocal/` kopieren und ausprobieren kannst.



## So funktioniert es

- Das Spiel hat pro Sprache eine **Schlüssel → Text**-Tabelle. Wenn eine Mod geladen wird, überschreiben die Werte der Mod die Originaltabelle.
- Deine Datei muss nur die Schlüssel enthalten, die du ändern willst.
- Der Dateiname ist die Sprache. `ko-KR.json` überschreibt die koreanische Tabelle, `en-US.json` die englische.
- Wenn mehrere Lokalisierungs-Mods denselben Schlüssel setzen, gewinnt die später geladene Mod.
- Eine Mod, die nur Text ändert, wirkt sich nicht auf andere Teilnehmer aus. Wir empfehlen daher, `multiplayer.installation` in `metadata.json` auf `local_only` zu setzen.
- Schlüssel und Werte sind beide Strings. Verschachtelte Objekte und Arrays sind nicht erlaubt.
- Speichere die Dateien als **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← { "type": "localization", "path": "Localization" } zu modules hinzufügen
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Was nicht übersetzt werden darf

Spieltexte enthalten Ausdrücke, die zur Laufzeit interpretiert werden.
Übersetze die folgenden Texte nicht, sondern lass sie exakt so auf Englisch stehen (Groß- und Kleinschreibung beachten).

| Typ    | Beispiel                                                                     | Beschreibung                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Schlüsselwörter   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Werden zur Laufzeit durch Zahlen oder Namen ersetzt. Ändere kein einziges Zeichen innerhalb der geschweiften Klammern.   |
| Spiel-Tags | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Werden durch Schlüsselwort-Tooltips und Symbole ersetzt. Die Namen in den Tags sind festgelegt, einschließlich Groß- und Kleinschreibung. |
| Formatierungs-Tags | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Textformatierung. Öffnende und schließende Tags müssen paarweise vorkommen.        |
| Zeilenumbrüche   | `\n`                                                                        | Ein Zeilenumbruch innerhalb eines JSON-Strings.                        |




## Eine neue Sprache hinzufügen

- Du kannst auch eine Sprache hinzufügen, die das Spiel nicht unterstützt. Wenn der Dateiname ein neuer Sprachcode ist, registriert das Spiel ihn als neue Sprache und zeigt ihn in der Liste der Spracheinstellungen an.
- Die originalen Sprachdateien liegen in `<Installationsordner des Spiels>/Sephiria_Data/StreamingAssets/Localization/`, eine pro Sprache. Nutze sie als Referenz beim Übersetzen.
- Die vom Spiel bereits unterstützten Sprachen sind `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` und `tr-TR`.
- **Übersetze jeden Schlüssel.** Schlüssel, die in deiner Mod-Datei fehlen, werden nicht leer angezeigt, sondern fallen auf **Englisch** zurück (oder auf Koreanisch, wenn auch Englisch fehlt). Am einfachsten startest du mit einer Kopie der originalen `en-US.json`.
- **Fülle die Einstellungsschlüssel aus, die mit `#` beginnen.** `#LanguageName` ist der Name, der in der Liste der Spracheinstellungen angezeigt wird, und `#FontName` die zu verwendende Schriftart. Passe Satzzeichenschlüssel wie `#Colon`, `#Bullet` und `#ParenthesisOpen` an die Konventionen der jeweiligen Sprache an.
- Für `#FontName` kannst du aus den im Spiel eingebauten Schriftarten wählen. Das Spiel enthält Schriftarten für folgende Schriftsysteme:

| Schriftsystem | `#FontName` |
|---|---|
| Lateinisch, Kyrillisch, Hangul | `GalCyrillic` |
| Japanisch | `JF-Dot-MPlus10` |
| Vereinfachtes Chinesisch | `Fusion_Pixel_10_Simple` |
| Traditionelles Chinesisch | `BoutiqueBitmap9x9` |
| Thai | `NotoSansThai` |



## Fehlerbehebung

| Symptom | Was zu prüfen ist |
|---|---|
| Die Mod ist aktiviert, aber der Text ändert sich nicht | Ob der Schlüsselname bis auf jedes Zeichen exakt mit dem Original übereinstimmt. Ob der Dateiname zur aktuellen Spielsprache passt (`ko-KR.json` gilt nur auf Koreanisch). |
| Nur einige Schlüssel ändern sich nicht | Ob eine andere Lokalisierungs-Mod denselben Schlüssel später überschreibt. Versuche, die Reihenfolge im Panel zu ändern. |
| `{VALUE}` erscheint unverändert im Text | Das Schlüsselwort ist anders geschrieben als im Original. Kopiere es direkt aus dem Originaltext. |
| `<tag=…>` erscheint unverändert im Text | Schreibweise oder Groß-/Kleinschreibung des Tag-Namens weicht ab. |
| Koreanischer (oder anderer nicht englischer) Text wird unleserlich angezeigt | Die Datei ist nicht UTF-8. Speichere sie erneut als UTF-8. |
| Zeichen werden als Kästchen angezeigt | Diese Zeichen sind in den eingebauten Schriftarten des Spiels nicht enthalten. |
| Eine ganze Datei wird ignoriert | Die Datei hat einen JSON-Syntaxfehler. `Player.log` protokolliert den Fehler zusammen mit dem Dateinamen. |

Die Logdatei liegt unter `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Suche nach „모드 내 언어 파일 발견“ (Sprachdatei in Mod gefunden), um zu sehen, welche Dateien gelesen wurden und wie viele Schlüssel jede enthielt.
