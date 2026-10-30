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
3. Wähle deine Mod aus und drücke **In den Workshop hochladen**.
4. Bei Mods ohne Code ist der Upload in den Workshop der letzte Schritt.
5. Bei Mods mit Code (DLLs) wird die Mod nach Abschluss des Workshop-Uploads automatisch zusammen mit ihrer Workshop-Beitrags-ID beim **Prüfsystem** eingereicht.
6. Gehe zur hochgeladenen Workshop-Seite, führe einen letzten Test durch (zum Beispiel indem du sie abonnierst) und stelle dann die Sichtbarkeit auf „Öffentlich“.

Ist der Workshop-Upload abgeschlossen und nur die Einreichung zur Prüfung fehlgeschlagen (zum Beispiel wegen eines Netzwerkfehlers), drücke bei derselben Mod erneut **In den Workshop hochladen**. Hat sich der Inhalt nicht geändert, wird die Mod nicht erneut in den Workshop hochgeladen; nur die Einreichung wird erneut gesendet. Die Einreichung muss mit dem Steam-Konto erfolgen, das den Workshop-Beitrag erstellt hat.

## Code-Prüfung

- Das Prüfsystem dekompiliert DLLs und prüft sie auf Netzwerkkommunikation, Dateizugriffe, Obfuskation und schädliches Verhalten. Die Prüfung kombiniert automatische Analyse mit Kontrollen durch Operatoren, und uns ist bewusst, dass kein Prüfsystem jedes Risiko erkennen kann. Trotzdem wollen wir mit diesem Prüfverfahren die Verbreitung schädlicher Mods verringern. Danke für deine Mithilfe.
- Jede Mod, die Code (DLLs) enthält, **muss zusätzlich beim Mod-Prüfsystem eingereicht werden.** Lade sie dazu immer über die Schaltfläche **In den Workshop hochladen** im Spiel hoch.
- Mods, die in den Workshop hochgeladen, aber nicht beim Prüfsystem eingereicht wurden, können von Spielern grundsätzlich nicht ausgeführt werden.
- Spieler, die eine Mod herunterladen, fragen ihren Prüfstatus anhand der Workshop-Beitrags-ID der Mod und der installierten DLLs ab.
  - **Eingereicht / Freigegeben**: Die Mod wird ausgeführt. Sie wird auch schon ausgeführt, bevor die Prüfung abgeschlossen ist.
  - **Nicht eingereicht / Abgelehnt**: Die Mod wird nicht ausgeführt, und der Grund wird im Mod-Panel angezeigt.
- Wird bei der Prüfung ein Problem festgestellt, kann eine Mod auch nach der Veröffentlichung abgelehnt werden. Bei einer abgelehnten Mod kannst du den Grund im Panel über **Ablehnungsgrund anzeigen** einsehen. Die abgelehnten DLLs können nicht unverändert erneut veröffentlicht werden; behebe das Problem und lade sie erneut hoch.
- Auch ohne `dll`-Modul unterliegt eine Mod der Prüfung, wenn sie `libraries` hat oder eine DLL oder EXE mit geänderter Dateiendung enthält.
- Mods, die die Code-Prüfung durchlaufen, dürfen als komprimierter Ordner höchstens 20 MB groß sein.

### Gründe für eine fehlgeschlagene Prüfung

|Meldung|Ursache|
|---|---|
|Im Prüfsystem gibt es keine Einreichung.|Die Mod wurde in den Workshop hochgeladen, aber nicht beim Prüfsystem eingereicht. Reiche sie über die Schaltfläche **In den Workshop hochladen** erneut ein.|
|Das Prüfsystem hat die Genehmigung dieses Mods abgelehnt.|Das Betriebsteam hat die Mod aus einem bestimmten Grund abgelehnt. Sieh dir den Ablehnungsgrund an.|
|Vom Prüfserver konnten keine Informationen abgerufen werden.|Möglicherweise ist die Internetverbindung des Abonnenten unterbrochen, oder es gibt ein Problem mit dem Prüfserver.|
|Dieser Mod stammt aus dem vorherigen Prüfsystem.|Der Prüfserver wurde aktualisiert, und die bisherigen Verifizierungsdaten der Mod wurden zurückgesetzt. Reiche sie erneut ein.|
|Im Mod wurde eine nicht autorisierte DLL erkannt.|Ändert sich die Zusammensetzung der DLL-Dateien in der Mod, während sie läuft, wird das Laden dieser Mod eingeschränkt. Folgendes kann also gegen das Prüfsystem verstoßen: Die Mod lädt während der Ausführung eine DLL von außen herunter und lädt die Assembly; die Mod erzeugt während der Ausführung eine eigene DLL und führt sie aus; die Mod entpackt eine DLL, die mit einem eigenen Komprimierungsverfahren in ihr verpackt ist, und verwendet sie.|

## Sichtbarkeit und Veröffentlichung

- Beim ersten Upload ist die Sichtbarkeit auf `Nicht gelistet` gesetzt.
- Bevor du sie öffentlich machst, abonniere sie selbst, prüfe, ob die installierte Kopie richtig funktioniert, und stelle sie dann auf „Öffentlich“.
- Seitenbeschreibung, Screenshots, Beschreibungen in anderen Sprachen und so weiter kannst du auf der Seite des Steam-Workshop-Beitrags bearbeiten.

## Deine Mod aktualisieren

Du kannst deine Mod auf dieselbe Weise aktualisieren, wie du sie veröffentlicht hast.

- Ein Update, das DLLs ändert, wird nach dem Upload automatisch erneut eingereicht und durchläuft die Prüfung erneut. Hast du keine DLLs geändert, bleibt der Prüfstatus unverändert.
- Ändere deine `modId` nicht. Die Mod würde als andere Mod behandelt, was unter anderem Abhängigkeiten brechen kann.
- **Erhöhe bei jedem Update immer `version` in `metadata.json`.** Mod-Versionen werden beim Beitritt zu einer Mehrspieler-Lobby verglichen. Hat sich der Inhalt geändert, aber nicht die Version, können Spieler mit unterschiedlichem Mod-Inhalt im selben Spiel landen und auf Probleme stoßen.

## Upload-Ticket

Nach dem Upload wird `uploadticket.json` zu deinem Mod-Ordner hinzugefügt. Sie enthält deine Workshop-Beitrags-ID und die Informationen zur Einreichung bei der Prüfung, also **lösche sie nicht.** Wenn du deinen Mod-Ordner mit git verwaltest, ist es am sichersten, auch diese Datei zu committen. Wenn du den Computer wechselst oder neu installierst, brauchst du diese Datei, um denselben Workshop-Beitrag weiter aktualisieren zu können. Ohne sie behandelt das Spiel den Ordner als neue, noch nie veröffentlichte Mod.

`mod-guard-manifest.json` und `mod-guard.sig.json`, die das Prüfsystem vor 1.1.1 verwendet hat, werden nicht mehr verwendet. Auch wenn sie noch vorhanden sind, werden sie nicht in den Workshop hochgeladen, und du kannst sie löschen.

Wie du die Datei wiederherstellst, wenn du sie verloren hast, steht in den [FAQ](faq.md).


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
