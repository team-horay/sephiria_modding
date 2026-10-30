# FAQ


## Hur ser jag loggarna?

Spelets loggar skrivs till filen nedan. Den skrivs om varje gång du startar spelet, och den föregående körningen sparas i `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Modrelaterade rader börjar med en tagg inom hakparenteser, så sök efter den taggen.

| Tagg | Innehåll |
|---|---|
| `[WorkshopModManager]` | Hämtning av prenumerationslistan, nedladdningar, skanning av lokala mappar, sammanfattning av laddningsplanen |
| `[WorkshopModRuntime]` | Resultat av DLL-aktivering och sammanslagning av lokalisering |
| `[WorkshopUploader]` | Uppladdning till Workshop och inskickning till granskning |
| `[ModGuardClient]` | Kommunikation med granskningsservern |
| `[WorkshopLobbyMods]` | Jämförelse av modduppsättningar i flerspelarrum |
| `모드 내 언어 파일 발견` (koreanska: "språkfil hittad i modden") | Antal nycklar som lästs från varje lokaliseringsfil |


## Vad är "Lokalt mod (AddOns-mappen)" i Steam Workshop-panelen?

Du kan se posten "Lokalt mod (AddOns-mappen)" i panelen. Det är en äldre moddningsfunktion, så använd den inte för nya moddar.
Äldre moddar kan inte synkroniseras med andra spelare, så vi rekommenderar att du tar bort dem.


## Jag vill flytta mappen WorkshopLocal någon annanstans än till Sephirias installationsmapp.

Om du vill ha dina moddar på en annan enhet eller i en arbetsmapp kan du ange sökvägen som ett startargument. Gå till Sephiria → Egenskaper → Allmänt → Startalternativ i ditt Steam-bibliotek och skriv in:

```
-workshop_local=D:\mods\my-first-mod
```

Separera flera sökvägar med semikolon, och omge hela strängen med citattecken om en sökväg innehåller mellanslag.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Mappar som anges som argument behandlas precis som undermappar till `WorkshopLocal/`.


## Jag har tappat bort uploadticket.json.

`mod-guard-manifest.json` och `mod-guard.sig.json` används inte längre från och med 1.1.1, så det gör inget om du tappar bort dem. Den enda fil du behöver är `uploadticket.json`.

Om du har förlorat `uploadticket.json` (till exempel efter att du har formaterat om datorn) behandlar spelet mappen som en ny mod, så om du laddar upp den som den är skapas ett nytt Workshop-objekt i stället för att det befintliga uppdateras. För att kunna fortsätta uppdatera det befintliga objektet återställer du det i den här ordningen:

1. Prenumerera på din egen mod på Workshop.
2. Kopiera hela mappen som Steam laddade ner (`<Steam-bibliotek>/steamapps/workshop/content/2436940/<Workshop-objektets ID>/`) till `WorkshopLocal/`. Mappnamnet spelar ingen roll.
3. Skapa `uploadticket.json` med innehållet nedan i den kopierade mappen. Som nummer använder du siffrorna efter `id=` i Workshop-sidans URL.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Starta spelet, välj modden i panelen och tryck på **Ladda upp till Workshop**. **Det befintliga objektet uppdateras**, och om det är en mod med kod skickas den in till granskning igen under samma objekt-ID. (Obs: om du har duplicerat en befintlig modmapp för att skapa en annan mod **måste du ta bort** `uploadticket.json`. Om du låter den ligga kvar skriver den nya modden över originalmoddens Workshop-objekt.)
