[한국어](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **Deutsch** | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Polski](README.pl.md) | [Português (Brasil)](README.pt-BR.md) | [Русский](README.ru.md) | [Svenska](README.sv.md) | [ไทย](README.th.md) | [Türkçe](README.tr.md)

# Sephiria Modding-Leitfaden

Danke, dass du Teil der Sephiria-Modding-Community wirst.

Dies ist das offizielle Repository mit Modding-Leitfäden, Beispielen und Vorlagen für [Sephiria](https://store.steampowered.com/app/2436940). Mods können über den [Steam-Workshop](https://steamcommunity.com/app/2436940/workshop/) geteilt werden.



## Regeln

Damit das Spiel für alle sicher bleibt, halte dich bitte an die folgenden Regeln. Verstöße können dazu führen, dass das Veröffentlichen von Mods oder das Spielen selbst eingeschränkt wird.

1. **Kein schädliches Verhalten**: Malware, Backdoors, Keylogger, Krypto-Miner und alle anderen Funktionen, die das Gerät, das Konto oder die Daten eines Nutzers gefährden, sind verboten.
2. **Keine kommerzielle Nutzung**: Alle Mods müssen zu nicht kommerziellen Zwecken erstellt und verbreitet werden.
3. **Keine Rechtsverletzungen oder schädlichen Inhalte**: Inhalte, die Urheberrechte, die Privatsphäre oder Persönlichkeitsrechte anderer verletzen, sexuelle Inhalte enthalten oder Hass, Belästigung oder reale Gewalt fördern, sind verboten.
4. **Keine Störung des Spiels**: Bringe das Spiel nicht absichtlich zum Fehlverhalten und störe nicht das normale Spiel anderer Spieler.
5. **Keine Datensammlung**: Sammle keine Nutzerdaten und sende sie nicht an externe Ziele.
6. **Kein Umgehen von Zugangsprüfungen**: Umgehe nicht unrechtmäßig die Kauf-, Besitz- oder Zugangsprüfungen des Spiels oder der Vertriebsplattform.
7. **Mod-Prüfung**: Mods, die Code (DLLs) enthalten, durchlaufen vor der Freigabe eine Prüfung, einschließlich Analyse und Dekompilierung, um Regelverstöße zu erkennen. Die Freigabe kann einige Zeit dauern. Fälsche keine Prüfergebnisse oder Freigabekennzeichen. Dateien müssen in einem prüfbaren Zustand bereitgestellt werden; Obfuskation ist verboten.
8. **Verwendung von Spiel-Assets**: Das Dekompilieren und Analysieren von Code, Grafiken, Sounds und anderen Assets des Spiels ist in dem Umfang erlaubt, der zum Erstellen von Mods nötig ist. Extrahierte Spiel-Assets selbst dürfen jedoch nicht weiterverbreitet werden.
9. **Kein Identitätsmissbrauch oder Manipulation**: Gib dich nicht als andere aus und verändere oder verbreite keine Mods anderer ohne deren Zustimmung.



## Bevor du anfängst

- Mods, die Code (DLLs) enthalten, werden **geprüft**, bevor sie in den Workshop hochgeladen werden können. Die Prüfung untersucht den Code hauptsächlich auf schädliches Verhalten. Mods ohne Code, etwa Übersetzungen und Kostüme, können sofort ohne Prüfung hochgeladen werden. Beachte, dass Mods, die das Prüfsystem nicht lesen kann, abgelehnt werden (obfuskierte Dateien, aus C/C++ kompilierte DLLs usw.).
- Im Mehrspielermodus müssen alle Teilnehmer eines Raums dieselben Mods aktiviert haben. (Persönliche UI-Mods wie Übersetzungen sind davon ausgenommen.)
- Nachdem du Mods aktiviert hast, musst du das Spiel neu starten, um ihre Reihenfolge zu ändern oder sie wieder zu deaktivieren.
- Abonnierte Mods sind standardmäßig deaktiviert. Du kannst sie selbst in der Liste aktivieren.



## Dokumentation

| Dokument | Inhalt |
|---|---|
| [Deine erste Mod](docs/de/getting-started.md) | Entwicklungsordner einrichten, Mod lokal prüfen, Logs lesen |
| [metadata.json-Referenz](docs/de/metadata.md) | Bedeutung aller Felder, Regeln für Abhängigkeiten und Versionen, Modul-Deklarationen |
| [Lokalisierungs-Mods](docs/de/localization-mods.md) | Aufbau der Sprachdateien, Schlüssel finden, Regeln zum Erhalt von Tags und Platzhaltern |
| [Kostüm-Mods](docs/de/costume-mods.md) | Ein eigenes Kostüm erstellen |
| [DLL-Mods](docs/de/dll-mods.md) | Projekt einrichten, Einstiegspunkt, `HorayModAPI`-Events, Debugging |
| [Im Workshop veröffentlichen](docs/de/publishing.md) | In den Workshop hochladen, Prüfung von Code-Mods, Mehrspieler-Regeln |
| [API-Referenz](docs/de/api/README.md) | Öffentliche Typen und Member wie `HorayModAPI`, `WorkshopModBase`, Mod-Optionen und UI. Automatisch aus dem Spielquellcode generiert |
| [Anhang & FAQ](docs/de/faq.md) |  |


## Lizenz

Der Code in diesem Repository (Beispiele und Vorlagen) steht unter der [MIT-Lizenz](LICENSE).
Die Dokumentation darf frei zitiert, übersetzt und weiterverbreitet werden; bitte gib die Quelle an.
Das Spiel Sephiria selbst und seine Assets sind Werke von TEAM HORAY und fallen nicht unter diese Lizenz.
