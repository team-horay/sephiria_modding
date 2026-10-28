# Lokaliseringsmoddar

[Din första mod](getting-started.md) täckte grunderna i att skapa en mod, så den här sidan tar upp de gemensamma reglerna för lokaliseringsmoddar.
[`examples/translation-sample`](../../examples/translation-sample) innehåller ett komplett exempel som du kan kopiera rakt in i `WorkshopLocal/` och prova.



## Så fungerar det

- Spelet har en tabell **nyckel → sträng** per språk. När en mod läses in skriver värdena i modden över originaltabellen.
- Din fil behöver bara innehålla de nycklar du vill ändra.
- Filnamnet är språket. `ko-KR.json` skriver över den koreanska tabellen och `en-US.json` skriver över den engelska.
- Om flera lokaliseringsmoddar sätter samma nyckel vinner den mod som läses in sist.
- En mod som bara ändrar text påverkar inte andra deltagare, så vi rekommenderar att du sätter `multiplayer.installation` i `metadata.json` till `local_only`.
- Både nycklar och värden är strängar. Nästlade objekt och arrayer är inte tillåtna.
- Spara filerna som **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← lägg till { "type": "localization", "path": "Localization" } i modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Det här ska du inte översätta

Speltexten innehåller uttryck som tolkas vid körning.
Översätt inte texten nedan; behåll den på engelska exakt som den är (skiftlägeskänslig).

| Typ    | Exempel                                                                     | Beskrivning                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Nyckelord   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Ersätts med siffror eller namn vid körning. Ändra inte ett enda tecken inom klammerparenteserna.   |
| Speltaggar | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Ersätts med verktygstips och ikoner för nyckelord. Namnen inuti taggarna är fasta, inklusive skiftläge. |
| Formateringstaggar | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Textformatering. Öppnande och stängande taggar måste vara parade.        |
| Radbrytningar   | `\n`                                                                        | En radbrytning inuti en JSON-sträng.                        |




## Lägga till ett nytt språk

- Du kan också lägga till ett språk som spelet inte stöder. Om filnamnet är en ny språkkod registrerar spelet det som ett nytt språk och visar det i listan i språkinställningarna.
- Originalspråkfilerna finns i `<spelets installationsmapp>/Sephiria_Data/StreamingAssets/Localization/`, en per språk; använd dem som referens när du översätter.
- De språk spelet redan stöder är `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` och `tr-TR`.
- **Översätt alla nycklar.** Nycklar som saknas i din modfil visas inte som tomma; de faller tillbaka på **engelska** (eller koreanska om engelska också saknas). Enklaste sättet att börja är att kopiera originalets `en-US.json`.
- **Fyll i inställningsnycklarna som börjar med `#`.** `#LanguageName` är namnet som visas i listan i språkinställningarna, och `#FontName` är teckensnittet som ska användas. Anpassa skiljeteckensnycklar som `#Colon`, `#Bullet` och `#ParenthesisOpen` efter språkets konventioner.
- För `#FontName` kan du välja bland teckensnitten som är inbyggda i spelet. Spelet innehåller teckensnitt för följande skriftsystem:

| Skriftsystem | `#FontName` |
|---|---|
| Latinskt, kyrilliskt, hangul | `GalCyrillic` |
| Japanska | `JF-Dot-MPlus10` |
| Förenklad kinesiska | `Fusion_Pixel_10_Simple` |
| Traditionell kinesiska | `BoutiqueBitmap9x9` |
| Thai | `NotoSansThai` |



## Felsökning

| Symptom | Det här bör du kontrollera |
|---|---|
| Modden är aktiverad men texten ändras inte | Om nyckelnamnet matchar originalet exakt, tecken för tecken. Om filnamnet matchar spelets aktuella språk (`ko-KR.json` gäller bara på koreanska). |
| Bara vissa nycklar ändras inte | Om en annan lokaliseringsmod skriver över samma nyckel senare. Prova att ändra ordningen i panelen. |
| `{VALUE}` visas som det är i texten | Nyckelordet är stavat annorlunda än i originalet. Kopiera det direkt från originaltexten. |
| `<tag=…>` visas som det är i texten | Taggnamnets stavning eller skiftläge skiljer sig. |
| Koreansk (eller annan icke-engelsk) text visas förvrängd | Filen är inte UTF-8. Spara den igen som UTF-8. |
| Tecken visas som rutor | De tecknen ingår inte i spelets inbyggda teckensnitt. |
| En hel fil ignoreras | Filen har ett JSON-syntaxfel. `Player.log` loggar felet tillsammans med filnamnet. |

Loggfilen finns på `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Sök efter "모드 내 언어 파일 발견" (språkfil hittad i mod) för att se vilka filer som lästes och hur många nycklar var och en hade.
