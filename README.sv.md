[한국어](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Polski](README.pl.md) | [Português (Brasil)](README.pt-BR.md) | [Русский](README.ru.md) | **Svenska** | [ไทย](README.th.md) | [Türkçe](README.tr.md)

# Moddningsguide för Sephiria

Tack för att du är med i Sephirias moddningsgemenskap.

Det här är det officiella arkivet med moddningsguider, exempel och mallar för [Sephiria](https://store.steampowered.com/app/2436940). Moddar kan delas via [Steam Workshop](https://steamcommunity.com/app/2436940/workshop/).



## Regler

För att spelet ska vara tryggt för alla ber vi dig följa reglerna nedan. Överträdelser kan leda till begränsningar i att publicera moddar eller spela spelet.

1. **Inget skadligt beteende**: Skadlig kod, bakdörrar, tangentloggare, kryptogrävare och alla andra funktioner som äventyrar användarens enhet, konto eller data är förbjudna.
2. **Icke-kommersiell användning**: Alla moddar måste skapas och distribueras i icke-kommersiellt syfte.
3. **Inga rättighetsintrång eller skadligt innehåll**: Innehåll som gör intrång i andras upphovsrätt, integritet eller rätt till sin egen bild, eller som innehåller sexuellt innehåll eller främjar hat, trakasserier eller verkligt våld, är förbjudet.
4. **Stör inte spelet**: Få inte spelet att fungera felaktigt med avsikt och stör inte andra spelares normala spelande.
5. **Ingen datainsamling**: Samla inte in användarinformation och skicka den inte till externa mottagare.
6. **Kringgå inte åtkomstkontroller**: Kringgå inte på otillåtet sätt kontroller av köp, ägande eller åtkomst i spelet eller på distributionsplattformen.
7. **Granskning av moddar**: Moddar som innehåller kod (DLL-filer) granskas innan de godkänns, inklusive analys och dekompilering, för att kontrollera att reglerna följs. Godkännandet kan ta en stund. Förfalska inte granskningsresultat eller godkännandemarkeringar. Filerna måste lämnas in i ett granskningsbart skick; obfuskering är förbjuden.
8. **Användning av spelets resurser**: Det är tillåtet att dekompilera och analysera spelets kod, grafik, ljud och andra resurser i den utsträckning som behövs för att skapa moddar. Du får dock inte vidaredistribuera extraherade spelresurser i sig.
9. **Ingen imitation eller manipulation**: Utge dig inte för att vara någon annan, och ändra eller vidaredistribuera inte andras moddar utan deras samtycke.



## Innan du börjar

- Moddar som innehåller kod (DLL-filer) **granskas** innan de kan laddas upp till Workshop. Granskningen kontrollerar i första hand att koden inte innehåller skadligt beteende. Moddar utan kod, till exempel översättningar och dräkter, kan laddas upp direkt utan granskning. Observera att moddar som granskningssystemet inte kan läsa avvisas (obfuskerade filer, DLL-filer kompilerade från C/C++ och så vidare).
- I flerspelarläge måste alla deltagare i rummet ha samma uppsättning moddar aktiverade. (Personliga UI-moddar som översättningar är undantagna.)
- När du har aktiverat moddar måste du starta om spelet för att ändra deras ordning eller inaktivera dem igen.
- Prenumererade moddar är inaktiverade som standard. Du kan själv aktivera dem i listan.



## Dokumentation

| Dokument | Innehåll |
|---|---|
| [Din första mod](docs/sv/getting-started.md) | Sätta upp en utvecklingsmapp, kontrollera en mod lokalt, läsa loggar |
| [Referens för metadata.json](docs/sv/metadata.md) | Vad varje fält betyder, regler för beroenden och versioner, moduldeklarationer |
| [Lokaliseringsmoddar](docs/sv/localization-mods.md) | Språkfilernas struktur, hitta nycklar, regler för att bevara taggar och platshållare |
| [Dräktmoddar](docs/sv/costume-mods.md) | Skapa din egen dräkt |
| [DLL-moddar](docs/sv/dll-mods.md) | Projektuppsättning, startpunkt, händelser i `HorayModAPI`, felsökning |
| [Publicera på Workshop](docs/sv/publishing.md) | Ladda upp till Workshop, granskning av kodmoddar, regler för flerspelarläge |
| [API-referens](docs/sv/api/README.md) | Publika typer och medlemmar som `HorayModAPI`, `WorkshopModBase`, modalternativ och UI. Genereras automatiskt från spelets källkod |
| [Bilaga och FAQ](docs/sv/faq.md) |  |


## Licens

Koden i det här arkivet (exempel och mallar) är licensierad under [MIT-licensen](LICENSE).
Dokumentationen får fritt citeras, översättas och vidaredistribueras; ange gärna källan.
Själva spelet Sephiria och dess resurser är verk av TEAM HORAY och omfattas inte av den här licensen.
