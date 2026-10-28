<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# HorayModAPI

Den statiska startpunkt som spelet exponerar för moddar. Prenumerera på databasinläsnings-, sessions- och spawnhändelser, och registrera nätverksprefaber och specialstats. Prenumerera på händelser bara en gång, i `OnModLoaded`. Livscykeln och reglerna för flerspelarläge finns i [DLL-moddar](../dll-mods.md).

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Startpunkten för moddar. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | En hjälpklass för att registrera lokaliseringstext för moddar. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Startpunkten för moddar.

Exempel:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Konstanter och fält

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Ingen beskrivning ännu.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Ingen beskrivning ännu.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Ingen beskrivning ännu.*

### Händelser

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Avfyras direkt efter att mirakeldatabasen har initierats. Registrera eller ändra mirakel med `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Avfyras direkt efter att vapendatabasen har initierats. Registrera eller ändra vapen med `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Avfyras direkt efter att status-databasen (stateffekter) har initierats. Registrera eller ändra statusar med `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Avfyras direkt efter att specialstat-kategorier och statusar har lästs in. Registrera visningsposter med `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Avfyras direkt efter att föremålsdatabasen har initierats. Registrera eller ändra föremål med `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Avfyras direkt efter att nyckelordsdatabasen har initierats. Registrera eller ändra nyckelord med `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Avfyras direkt efter att databasen för passiva förmågor har initierats. Registrera eller ändra passiva förmågor med `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Avfyras direkt efter att dräktdatabasen har initierats. Registrera eller ändra dräkter med `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Avfyras direkt efter att rekvisitadatabasen har initierats. Registrera rekvisita med `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Avfyras direkt efter att enhetsdatabasen (spawn-entiteter för fiender och NPC:er) har initierats.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Avfyras direkt efter att NPC-rolldatabasen har initierats.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Avfyras direkt efter att databasen för anpassade spel har initierats.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Avfyras direkt efter att alla databaser har initierats klart.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Anropas efter att titelmenyn har lästs in.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Anropas när spelet startar. (serversidan) Med start menas att man klickar på en sparplats på titelskärmen för att gå in i spelet. Sessionsdata som spelaren och den aktuella platsen har ännu inte lästs in.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Anropas när spelet startar. (klientsidan) Med start menas att man klickar på en sparplats på titelskärmen för att gå in i spelet. Sessionsdata som spelaren och den aktuella platsen har ännu inte lästs in.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Anropas när en spelsession startar. (serversidan) En session är en spelomgång. När spelaren dör avslutas den sessionen och en ny börjar.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Anropas när en spelsession startar. (klientsidan) En session är en spelomgång. När spelaren dör avslutas den sessionen och en ny börjar.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Anropas efter att lokaliseringsfilerna har lästs in.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Anropas när en våning allokeras. (serversidan) Varje plats i Sephiria definieras som en våning. När en spelare förflyttar sig mellan platser körs följande steg i ordning. 1. Våningsallokering: skapar och läser in den nya våningen. 2. Förflyttning: flyttar spelaren till den begärda punkten på den fullständigt inlästa våningen. 3. Borttagning av våning: tar bort platsen spelaren befann sig på tidigare, om den är tom (= inga spelare finns kvar där).
param1: Våningens GUID  
param2: Våningens namn  
param3: Instans av FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Anropas när en våning allokeras. (klientsidan) Varje plats i Sephiria definieras som en våning. När en spelare förflyttar sig mellan platser körs följande steg i ordning. 1. Våningsallokering: skapar och läser in den nya våningen. 2. Förflyttning: flyttar spelaren till den begärda punkten på den fullständigt inlästa våningen. 3. Borttagning av våning: tar bort platsen spelaren befann sig på tidigare, om den är tom (= inga spelare finns kvar där).
param1: Våningens GUID  
param2: Våningens namn  
param3: Instans av FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Anropas efter att den slumpmässiga fiendesammansättningen för en bana har bestämts. (serversidan) Avfyras direkt efter att våningsgeneratorn har valt monsterfaserna för ett stridsrum (eller en stridszon), innan spawnern tar emot dem. Ersätt eller redigera ctx.Phases för att ändra vad som spawnar där. Spelet tvingar inte fram någon balans här; se RoomSpawnAssignContext för reglerna (deterministisk slumpgenerator via ctx.Seed, rör aldrig ctx.Sources, entiteter måste vara registrerade i UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Anropas när en spelare spawnar. (serversidan) Du kan registrera spelarrelaterade callbacks inuti den här hooken. Utlöses i slutet av PlayerAvatar.OnStartServer, efter att grundstats har applicerats. Dräkt, våning och inventariets innehåll kan fortfarande fyllas i senare under samma bildruta eller under följande bildrutor. Använd den för att prenumerera på avatarens egna instanshändelser (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Prenumerationer på avataren försvinner med avataren, så de behöver inte avprenumereras.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Anropas när en spelaravatar spawnar. (klientsidan) Utlöses en gång per avatar i slutet av dess OnStartClient; avfyras även för fjärrspelare. Kontrollera avatar.isLocalPlayer / avatar.isOwned för att skilja ut den lokala spelaren. SyncVars är redan applicerade, men våning och inventariets innehåll kan komma senare.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Avfyras när en enhet (spelare, fiende, åkallad varelse eller följeslagare) spawnar på servern. Endast server (värd).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Avfyras direkt innan en enhet tas bort från servern. Endast server (värd).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Avfyras när en spelare undviker en attack. (serversidan)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Låter dig byta ut debuff-prefaben direkt innan en debuff appliceras på en enhet. Hanteraren tar emot (aktuell prefab, kastare) och returnerar en ersättningsprefab; returneras null lämnas den oförändrad.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Avfyras när servern tar skrivbehörighet för inventariet och börjar ta bort alla föremålseffekter.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Avfyras direkt efter att servern lämnar tillbaka skrivbehörigheten och applicerar föremålseffekterna igen.

### Metoder

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registrerar ett objekt (prefab) som kan spawnas över nätverket. Objektet måste ha en NetworkIdentity-komponent.

Servern (värden) och klienterna måste var och en hålla en lista över objekt som kan använda nätverket, och den listan måste vara identisk för alla.

Exempel:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Returnerar: true om registreringen lyckades


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registrerar en prefab som skapats vid körning (till exempel en klon av en inbyggd mall) som ett nätverksspawnbart objekt, med ett fast assetId beräknat från `stableKey`. Använd moddens enhetsnamn (`<modId>:<id>`) som nyckel.

Returnerar: true om registreringen lyckades


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registrerar flera prefaber som nätverksspawnbara objekt på en gång.

Returnerar: antalet prefaber som registrerades utan fel


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Avregistrerar en registrerad nätverksprefab.

Returnerar: true om den togs bort


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registrerar en specialstat-kategori i stat-UI:t.

Returnerar: true om den registrerades


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registrerar en StatusEntity som en post i en specialstat-kategori i stat-UI:t. Om du anger `reader` läses värdet via den funktionen i stället för via asset-inställningarna.

Returnerar: true om den registrerades


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Hämtar de enhetsspecifika alternativen som är bundna till modId. Workshop-moddar kan använda `Context.Options` i stället.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Öppnar en föremålsinteraktionspanel som används tillsammans med inventariet. Returnerar false om UI:t är upptaget.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Hämtar en fabrik för UI-widgetar i spelets stil. Fäst dem under en befintlig UI-Canvas.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Öppnar en dialogruta i världen. Returnerar false om UI:t är upptaget; visa nästa sida med `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Öppnar en dialogruta med en förskjutning i världskoordinater. Om null används prefabens standardförskjutning.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Öppnar en dialogruta med separata förskjutningar för dialogen och kameran. Kamerafokus gäller bara lokalt.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

En hjälpklass för att registrera lokaliseringstext för moddar.

### Metoder

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registrerar text för ett specifikt språk.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Registrerar text för flera språk på en gång
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
