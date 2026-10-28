# Im Workshop veröffentlichen

Diese Seite erklärt, wie du deine Mod in den Steam-Workshop hochlädst, wie das Prüfsystem funktioniert und wie Mods im Mehrspielermodus zwischen Spielern synchronisiert werden.


## Vor dem Upload

- Um eine Mod in den Workshop hochzuladen, ist ein Vorschaubild (PNG oder JPG, unter 1 MB) erforderlich. Gib es mit `info.preview` an oder lege eine `thumbnail.png` in deinen Mod-Ordner. Details findest du in der [metadata.json-Referenz](metadata.md#preview).
- `info.title` darf bis zu 128 Zeichen und `info.description` bis zu 7999 Zeichen lang sein. Wenn du sie in mehreren Sprachen geschrieben hast, wird auf der Workshop-Seite die `en-US`-Version verwendet.
- Trage Workshop-Kategorien in `tags` ein. Die verfügbaren Kategorien stehen in der [metadata.json-Referenz](metadata.md).
- Jede Datei im Ordner unterliegt der Prüfung. Stelle daher sicher, dass dein Mod-Ordner keine Kopien von Spiel-DLLs, `.pdb`-Dateien oder Zwischenergebnisse des Builds enthält.

## Schritte zur Veröffentlichung

1. Starte Sephiria.
2. Öffne auf dem Titelbildschirm das Panel **Steam Workshop** und suche die von dir erstellte Mod.
3. Wähle deine Mod aus.
4. Mods mit Code (DLLs) durchlaufen die Code-Prüfung.
5. Mods ohne Code können sofort in den Workshop hochgeladen werden.
6. Gehe zur hochgeladenen Workshop-Seite, führe einen letzten Test durch (zum Beispiel indem du sie abonnierst) und stelle dann die Sichtbarkeit auf „Öffentlich“.

## Code-Prüfung

- Das Prüfsystem untersucht jede Datei in deinem Mod-Ordner. DLLs werden dekompiliert und auf Netzwerkkommunikation, Dateizugriffe, Obfuskation und schädliches Verhalten geprüft; Datendateien wie Bilder, Sounds und JSON werden auf ein gültiges Format geprüft.
- Werden keine Probleme gefunden, wird die Mod automatisch freigegeben; wirkt etwas verdächtig, prüft ein Operator sie manuell. Eindeutig schädliche Mods werden automatisch abgelehnt.
- Die Prüfung kombiniert automatische Analyse mit Kontrollen durch Operatoren, und uns ist bewusst, dass kein Prüfsystem jedes Risiko erkennen kann. Trotzdem wollen wir mit dieser vorgelagerten Prüfung und Freigabe die Verbreitung schädlicher Mods verringern. Danke für deine Mithilfe.
- Ob eine Mod Code enthält, wird anhand des **Dateiinhalts** bestimmt, nicht anhand des Dateinamens. Auch ohne `dll`-Modul unterliegt eine Mod der Prüfung, wenn sie `libraries` hat oder eine DLL oder EXE mit geänderter Dateiendung enthält.
- Mods, die die Code-Prüfung durchlaufen, dürfen als komprimierter Ordner höchstens 20 MB groß sein.

## Freigabenachweis (nur Mods mit Code)

- Mods, die Code enthalten, erhalten einen Freigabenachweis.
- Er bescheinigt, dass die Dateien der Mod mit der freigegebenen Version identisch sind.
- DLL-Mods müssen diesen Nachweis erhalten, bevor Spieler, die die Mod herunterladen, ihren Code ausführen können.
- Ändert sich nach der Freigabe auch nur eine Datei, wird der Nachweis ungültig und die Mod muss erneut geprüft werden.

## Sichtbarkeit und Veröffentlichung

- Beim ersten Upload ist die Sichtbarkeit auf `Nicht gelistet` gesetzt.
- Bevor du sie öffentlich machst, abonniere sie selbst, prüfe, ob die installierte Kopie richtig funktioniert, und stelle sie dann auf „Öffentlich“.
- Seitenbeschreibung, Screenshots, Beschreibungen in anderen Sprachen und so weiter kannst du auf der Seite des Steam-Workshop-Beitrags bearbeiten.

## Deine Mod aktualisieren

Du kannst deine Mod auf dieselbe Weise aktualisieren, wie du sie veröffentlicht hast.

- DLL-Mods benötigen einen neuen Freigabenachweis.
- Ändere deine `modId` nicht. Die Mod würde als andere Mod behandelt, was unter anderem Abhängigkeiten brechen kann.
- **Erhöhe bei jedem Update immer `version` in `metadata.json`.** Mod-Versionen werden beim Beitritt zu einer Mehrspieler-Lobby verglichen. Hat sich der Inhalt geändert, aber nicht die Version, können Spieler mit unterschiedlichem Mod-Inhalt im selben Spiel landen und auf Probleme stoßen.

## Upload-Ticket

Nachdem du die Prüfung angefordert und hochgeladen hast, werden die folgenden Dateien zu deinem Mod-Ordner hinzugefügt. Sie enthalten deine Freigabeinformationen, also **lösche sie nicht.** Wenn du deinen Mod-Ordner mit git verwaltest, ist es am sichersten, auch diese drei Dateien zu committen. Wenn du den Computer wechselst oder neu installierst, brauchst du diese Dateien, um denselben Workshop-Beitrag weiter aktualisieren zu können.

| Datei | Inhalt |
|---|---|
| `uploadticket.json` | Das Prüf-Ticket und die Workshop-Beitrags-ID. Ohne sie behandelt das Spiel den Ordner als neue, noch nie veröffentlichte Mod. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | Der Freigabenachweis der Prüfung: die Liste der freigegebenen Dateien und eine Signatur. Wer sie bearbeitet, macht die Freigabe ungültig. |

Wie du diese Dateien wiederherstellst, wenn du sie verloren hast, steht in den [FAQ](faq.md).

## Widerruf der Freigabe

Wird nach der Veröffentlichung ein Regelverstoß festgestellt, kann ein Operator die Freigabe widerrufen. Eine widerrufene Mod wird auch dann nicht geladen, wenn sie installiert ist, und im Panel als „Prüffreigabe widerrufen“ angezeigt.


## Mehrspieler-Regeln

### Einrichten einer Mehrspieler-Lobby

Wenn der Host eine Mehrspieler-Lobby erstellt, wird **die Liste der vom Host aktivierten Mods** bei der Lobby registriert.

- Mods mit `installation` auf `host_only`: Teilnehmer müssen sie nicht installieren; sie werden nur in der Liste angezeigt.
- Mods mit `multiplayer.installation` auf `everyone` (oder ohne Angabe): Teilnehmer müssen sie ebenfalls installieren.
- Kostüm-Mods und Daten-Mods: Teilnehmer müssen sie ebenfalls installieren.

### Beitreten

Weicht die aktuelle Mod-Konfiguration eines Spielers von der der Lobby ab, durchläuft er diese Schritte: herunterladen → aktivieren → Spiel neu starten und erneut beitreten.

### In WorkshopLocal installierte Test-Mods

In einem Spiel, in dem lokale Mods aus `WorkshopLocal/` geladen sind, ist kein Mehrspielermodus verfügbar. Um den Mehrspielermodus zu testen, lade deine Mod in den Workshop hoch, leere den lokalen Ordner und verwende die abonnierte Kopie. Du kannst sie auch über den Link abonnieren, solange der Beitrag `Nicht gelistet` ist.
