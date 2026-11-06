# DLL-moddar

DLL-moduler låter dig köra kod skriven i C# inuti spelet. Prenumerera på `HorayModAPI` för att koppla in dig på spelare, föremål, UI med mera.
Det här dokumentet täcker allt från att skapa ett projekt till att få din första DLL att köra i spelet. Den fullständiga listan över händelser och registreringsfunktioner finns i [API-referensen](api/README.md).


## Krav

- [.NET SDK](https://dotnet.microsoft.com/download) 8 eller senare
- En C#-redigerare


## Skapa ett projekt

Utgå från exemplet [`examples/dll-sample`](../../examples/dll-sample). Det här dokumentet använder det som referens.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- Spelets installationsmapp. Ändra den så att den matchar din miljö, eller skicka in den med dotnet build -p:GameDir=... -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- Lägg på samma sätt till Mirror om du arbetar med nätverksobjekt som PlayerAvatar, UnityEngine.UI om du bygger UI och 0Harmony om du patchar -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Använd `netstandard2.1` som målramverk** och sätt språkversionen till C# 9.
2. **Referera till spelets DLL-filer, men kopiera dem inte.** Peka referenserna på filerna i `<spelets installationsmapp>/Sephiria_Data/Managed/` och sätt alla till `Private=false`. Om spelets DLL-filer kopieras in i din modmapp läses samma assembly in två gånger, vilket orsakar svårtydda fel, och dessutom läggs filer i onödan till i granskningen.
3. `Assembly-CSharp.dll` innehåller det mesta av koden som driver spelet.


## Kodstruktur


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Anropas en gång per process. Prenumerera på händelser här, och bara en gång.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // Anropas bara på värden (servern). Ändra spelets tillstånd här.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Anropas hos alla deltagare. Värdet som servern lade till kommer via synkronisering.
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

Din klass ärver följande från basklassen.

| Medlem | Beskrivning |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Fylls med värdena från `metadata.json`. |
| `Context` | `PackageRoot` (modmappens absoluta sökväg), `FileId` (Steam Workshop-objektets ID; för lokala paket ett separat värde som aldrig krockar med ett Steam-ID), `Definition` (det validerade manifestet), `ResolvePath(relativePath)`. Använd den för att hitta filer i din modmapp. Installationssökvägen är olika för varje användare, så hårdkoda den aldrig. |
| `GetDataPackages()` | Listan över `data`-modulpaket som anger den här modden som sin `consumer`. Anropa den när aktiveringen är klar (inuti en `OnLoad*Database`-hanterare eller efter `OnAllDatabasesReady`). |

`Context` är giltig från det ögonblick `OnModLoaded` anropas. I konstruktorn är den fortfarande null.


## Livscykel

DLL-moddar aktiveras **när du startar ett spel och går in i världen**. Ingen kod körs ännu på titelskärmen. Startordningen är följande.

1. Världsscenen läses in och `GameDataLoader` startar.
2. DLL-modulerna i laddningsplanen läses in i ordning, varje startpunktsklass instansieras och `OnModLoaded` anropas.
3. När databaserna initieras avfyras händelser som `OnLoadItemDatabase` och `OnLoadUnitDatabase` en efter en.
4. När allt är klart anropas `OnAllDatabasesReady`.

## Prenumerera

Klassen `HorayModAPI` tillhandahåller fördefinierade händelser som låter dig koppla in dig i många situationer.

- Prenumerera på händelser **bara en gång**, i `OnModLoaded`. Databashändelser avfyras igen vid varje inläsning, så gör det som "måste ske varje gång" (registrera föremål och så vidare) inuti hanterarna.
- Hantera prenumeration och avprenumeration noggrant. Avprenumerera (-=) de händelser du prenumererat på i `OnModUnloaded`.

Den fullständiga listan över händelser finns i [API-referensen](api/README.md). Tabellen nedan listar den rekommenderade händelsen för varje sak du kan vilja göra.

| Det du vill göra | Händelse |
|---|---|
| Lägga till föremål eller artefakter | `OnLoadItemDatabase` |
| Lägga till vapen, mirakel, passiva förmågor, dräkter eller rekvisita | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Lägga till fiende- eller NPC-enheter | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Lägga till resenärer (NPC:er i byn) | `OnLoadSocialIDDatabase` |
| Lägga till en statvisning | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Registrera lokaliserad text från kod | `OnLocalizationReady` (`AddText`) |
| Lägga till en knapp i titelmenyn | `OnTitleMenuReady` |
| Spel- eller sessionsstart | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| När en våning genereras | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Spelare eller enhet spawnar | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Koppla in dig i stridskontroller | `OnPlayerEvade`, `OnGetDebuff` |
| Behörigheter för inventariemanipulation | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Undvika konflikter

Entiteter som din mod lägger till namnges i formatet `<modId>:<id>`, t.ex. `yourname.artifact-pack:flame-ring`. Den här regeln förhindrar konflikter med andra moddar och är så modinnehåll särskiljs i spelhistorik och statistik.


## Serversida, klientsida

- Sephiria är byggt kring flerspelarläge, så många händelser finns i separata versioner för serversidan och klientsidan.
- Värden avfyrar båda; övriga deltagare avfyrar bara Clientside-varianten.
- Som regel: gör sådant som ändrar spelets tillstånd (spawn, drops, skada) bara i Serverside, och sådant som bara ändrar det som visas i Clientside.


## Harmony-patchar

När du behöver ändra något som händelserna inte når kan du använda Harmony.
Sedan patch 1.1.0 är **Harmony 2.4.3 inbyggt** i spelet, så du behöver bara lägga till en referens till det.

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **Packa inte med `0Harmony.dll` i din mod.** En medföljande kopia ignoreras, och även om du tar med en annan version körs din mod inte på den versionen.
- Om du använder ditt `modId` som det är som Harmony-ID krockar det inte med andras.
- Patchar går sönder vid speluppdateringar betydligt lättare än det officiella API:et. Gör vad du kan med händelser och spara patchar som en sista utväg.
- Externa laddare som BepInEx och MelonLoader stöds inte. En mod som fungerar som en extern laddare (en som hittar och kör plugin-DLL-filer utanför Workshop) får en varning och aktiveras först när användaren godkänner den. Se [Moddar som kör kod utanför Workshop](#moddar-som-kör-kod-utanför-workshop) för mer information.


## Hjälp-DLL:er

Om din mod använder andra bibliotek (t.ex. tredjepartspaket utöver Newtonsoft.Json) lägger du deras DLL-filer i din modmapp och deklarerar dem i `libraries` i `metadata.json`. DLL-filer som inte deklareras används inte vid referensupplösning.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Ta inte med DLL-filer som redan följer med spelet (Unity, Mirror, Newtonsoft.Json, 0Harmony och så vidare). Spelets kopia läses in först, så kopian i din mod ignoreras antingen eller orsakar konflikter.
- Native-DLL:er (C/C++) kan inte användas. Endast hanterade assemblies läses in.
- Om en assembly med samma namn redan har lästs in av en annan mod väljer spelet, för en DLL i `modules`, inte en av de två; det **avvaktar med att köra** den. För en DLL i `libraries` använder spelet den som redan är inläst och ignorerar din medföljande kopia, vilket kan ge fel om versionerna skiljer sig åt. Det är säkrare att bryta ut delade bibliotek till en separat mod och koppla den via `dependencies`.

### Använda en annan mods bibliotek

Om flera moddar använder samma kod, lägg koden i en överordnad mod och koppla de underordnade moddarna till den via `dependencies`.

1. Den överordnade modden deklarerar den delade koden i `modules` eller `libraries` och lägger till `Library` i sina `tags`.
2. Den underordnade modden anger den överordnade moddens `modId` i `dependencies` och byggs mot den överordnade moddens DLL med `Private=false`. **Kopiera inte den överordnade moddens DLL in i den underordnade modden.**
3. Spelet läser in den överordnade modden först, så den underordnade moddens referenser löses mot den överordnade moddens redan inlästa assembly.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Den underordnade modden behöver fortfarande en egen `dll`-modul och `entryClass`. Att den överordnade modden själv letar upp och läser in den underordnade moddens DLL-filer stöds inte. Låt i stället den underordnade modden anropa den överordnade moddens registreringsfunktion från sin egen `OnModLoaded`.
- Om den överordnade modden ändrar sina publika typer eller medlemmar i en uppdatering kan underordnade moddar gå sönder. Undvik att ta bort eller ändra befintliga publika API:er i ett delat bibliotek.

### Skapa inte DLL-filer under körning!

All kod som en mod kör måste finnas i ett granskat Workshop-objekt. Om DLL-filerna i en mod ändras medan modden körs begränsas inläsningen av modden. Ta med alla DLL-filer i Workshop-objektet som de är, utan någon komprimering eller streaming. Det innebär att följande kan bryta mot granskningssystemets regler. Lägg alla DLL-filer du behöver i modden när du laddar upp den.

- Modden laddar ner en DLL utifrån medan den körs och läser in assemblyn
- Modden skapar och kör en egen DLL medan den körs (inklusive att skriva om en annan DLL med Mono.Cecil eller liknande och sedan köra den)
- Modden packar upp en DLL som är packad i den med ett eget komprimeringsformat och använder den

### Moddar som kör kod utanför Workshop

En mod som hittar och kör DLL-filer som användaren lägger **utanför moddens mapp**, till exempel i spelets installationsmapp eller i `AppData` (till exempel en kompatibilitetsmod för en annan laddares plugin), avvisas inte, men granskningssystemet lägger till en **varning** på den.

- När användaren aktiverar modden visas en varning, och modden aktiveras bara om användaren godkänner den. När modden uppdateras frågas användaren igen.
- Externa DLL-filer som körs på det här sättet granskas inte, och problem som de orsakar stöds inte.
- En mod som själv laddar ner sådana DLL-filer medan den körs omfattas av de förbjudna fallen ovan.

Om du vill använda en annan mods kod ska du inte läsa in DLL-filer direkt från den moddens mapp. Koppla i stället till den via `dependencies`, som beskrivs i [Använda en annan mods bibliotek](#använda-en-annan-mods-bibliotek). DLL-filer som spelet inte själv har läst in hoppar över laddningsordningen samt kontrollerna av namnkonflikter och beroenden, så de stöds inte.

Om du behöver skriva om en DLL (till exempel konvertera IL för ett annat Harmony-API) ska du göra det före uppladdningen och lägga den konverterade DLL-filen i din mod.


## Bygga och testa

```bash
dotnet build -c Release
```

- När bygget lyckas och DLL-filen dyker upp i `WorkshopLocal/<mod>/bin/` startar du spelet, kontrollerar att din mod visas under "Mina moddar" i Steam Workshop-panelen på titelskärmen och startar sedan ett spel.
- Om du låter `.pdb`-filen ligga kvar bredvid byggresultatet innehåller stackspårningarna i loggen radnummer. Ta bort den när du publicerar.


## Speluppdateringar och kompatibilitet

När spelet uppdateras ändras `Assembly-CSharp.dll`. Moddar som bara använder officiella händelser brukar fortsätta fungera, men moddar som rör interna klasser direkt eller patchar med Harmony kan gå sönder.

- Efter en uppdatering: bygg om mot den nya `Managed/`-mappen och testa.
- Om du anger **vilken spelversion som stöds** i beskrivningen på din Workshop-sida kan användarna snabbt hitta orsaken. Spelversionen kan också registreras automatiskt i Workshop-objektets metadata när du publicerar.
- När du ändrar din kod måste den skickas in till granskningssystemet igen. Se [Publicera på Workshop](publishing.md#kodgranskning).


## Att tänka på i flerspelarläge

- De flesta kodmoddar kommer att ha `multiplayer.installation` satt till `everyone`.
- Om din mod bara ändrar spelets tillstånd i `Serverside`-händelser, och värdena den ändrar är speltillstånd som redan synkroniseras (t.ex. `customStats`) så att deltagarna inte behöver någon kod eller registrering, kan du deklarera den som `host_only`. Deltagarna laddar då inte ner modden. En mod som registrerar nya föremål, använder `RegisterNetworkPrefab` eller ändrar något i `Clientside`-händelser kan inte vara `host_only`.
- Objekt som spawnas över nätverket (enheter, projektiler och så vidare) måste registreras med `RegisterNetworkPrefab` för att skapas även på deltagarnas sida. Om du anger en `stableKey` behålls samma ID även när din modversion ändras.
- Värden och deltagarna måste till och med ha samma **laddningsordning** för moddar. Undvik kod vars resultat beror på den ordningen.


## Nätverkssynkronisering (SyncVar, RPC, Command)

Sephirias flerspelarläge är byggt på nätverksbiblioteket med öppen källkod "Mirror". I Mirror kan du enkelt använda nätverksfunktioner genom att deklarera följande attribut.

- `[SyncVar]`: Ett attribut du sätter på en medlemsvariabel i en klass. Det synkroniserar variabelns värde mellan servern och klienterna.
- `[ClientRpc]`, `[TargetRpc]`: Attribut du sätter på en funktion. De ber klienter köra funktionen från servern.
- `[Command]`: Ett attribut du sätter på en funktion. Det ber servern köra funktionen från en klient.

I en modmiljö går det inte att använda attributen ovan på vanligt sätt. De fungerar bara när Mirrors Weaver lägger in motsvarande överföringskod vid kompilering, och en DLL som skapats utanför spelet går inte genom Weaver. Även om bygget lyckas gör attributen ovan därför ingenting. Den här sortens bugg ser normal ut på värdens skärm och ställer bara till problem på deltagarnas skärmar, så den är svår att upptäcka om du testar ensam. (Även om man tvingade koden genom Weaver kan det finnas buggar som kollisioner mellan funktionshashar.)


### `SyncVar<T>`, `ModRpc`

För att lösa detta tillhandahåller Sephiria nätverkskod för moddar som ersätter attributen ovan: `SyncVar<T>` och `ModRpc`.

| Det du vill göra | Det du använder |
|---|---|
| Hålla ett värde synkroniserat (inklusive deltagare som ansluter senare) | `SyncVar<T>` |
| Server → anrop hos alla klienter | `ModRpc.Send` |
| Server → anrop hos en specifik klient | `ModRpc.SendTo` |
| Klient → anrop hos servern | `ModRpc.SendCommand` |
| Ändra ett värde som spelet redan synkroniserar | Egenskapen `Network<FieldName>` |

- Den här funktionen finns från spelversion 1.1.2.
- En mod som använder den här funktionen ska deklarera `multiplayer.installation` som `everyone`.


### Förbereda en komponent

`SyncVar<T>` och `ModRpc` kan bara användas i en komponent som ärver från `NetworkBehaviour`. Komponenten måste sitta på samma objekt hos både värden och deltagaren, **i samma ordning**. Om du lägger till den med `AddComponent` på ett objekt som redan har spawnats ingår den inte i synkroniseringen, så den måste redan sitta på objektet innan du registrerar det som ett nätverksprefab.

```csharp
private static GameObject prefab;

// Prenumerera på HorayModAPI.OnAllDatabasesReady i OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Placera det under en inaktiv förälder. Prefabens Awake körs inte, och en Instantiate-kopia startar aktiv.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Spawna på servern. Om du anger det andra argumentet blir den spelaren ägare.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: synkronisera värden

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Det finns ingen Weaver, så registrera direkt i konstruktorn. Fältens ordning måste alltid vara densamma.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Ändra värdet **på servern**, som `stack.Value = 3`. Ett värde som en deltagare ändrar skickas inte till servern.
- En deltagare som ansluter senare får också det aktuella värdet när hen går in.
- `Callback` anropas hos värden när du tilldelar ett värde och hos deltagaren när den tar emot ett värde. Den anropas också om värdet som togs emot vid inträdet skiljer sig från standardvärdet. Ett undantag i en callback skrivs till loggen och synkroniseringen fortsätter.
- Du kan använda grundläggande typer som `int`, `float`, `string` och `Vector3`. För en struct som din mod själv definierar skapas ingen serialiseringskod automatiskt, så dela upp den i separata fält.
- För att peka på ett annat objekt, skicka dess `netId` som en `SyncVar<uint>` i stället för att använda `SyncVar<GameObject>`. Objektet kanske ännu inte har skapats hos mottagaren.
- En enskild komponent kan registrera högst 64 SyncVar, ärvda inräknade.

### `ModRpc`: anropa funktioner

Registrera en funktion en gång när modden läses in. Namnet du använder vid anrop är `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Server → alla klienter som ser det här objektet
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Server → en klient. Om du anger null som anslutning skickas det till objektets ägare.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Klient → server
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Läs i samma ordning som du skrev. Om ordningen eller typerna för skrivning och läsning skiljer sig blir värdena förvanskade eller ett undantag uppstår.
- `RegisterCommand` kan som standard anropas **endast av objektets ägare**. För att ta emot det på ett objekt som saknar ägare anger du `requiresAuthority: false`. I så fall kan vilken deltagare som helst anropa det. Se upp för fusk och liknande.
- Om du skickar i en annan riktning än den du registrerade, eller skickar ett namn som inte registrerats, loggas bara ett fel och ingenting skickas.
- Om funktionen saknas hos mottagaren eller komponenttypen är en annan loggas bara en varning och anropet ignoreras. Ett undantag i en hanterare loggas också och spelet fortsätter.
- Standardkanalen är tillförlitlig överföring. För effekter som skickas ofta kan du ange argumentet `channelId: Channels.Unreliable`.

### Det här måste du följa

- En klient kan skicka godtyckliga värden via ett Command. Validera platsnummer, antal, mål och liknande i serverns hanterare.
- En deltagare som ansluter senare får inte RPC:er som skickades före inträdet. Skicka tillstånd som måste bestå med `SyncVar<T>` och använd RPC:er bara för flyktiga händelser som ljud och effekter.
- **Ett RPC kan komma fram före SyncVar.** Om du läser `stack.Value` i en RPC-hanterare kan det fortfarande vara det tidigare värdet. Om du behöver det nya värdet, skicka med det som RPC-argument eller hantera det i `Callback`.

### Ändra värden i Sephirias baskod

Om du tilldelar direkt till ett `[SyncVar]`-fält i en spelklass skickas det inte till deltagarna. Använd egenskapen med `Network` framför. Stor och liten bokstav är exakt som fältnamnet.

```csharp
unit.maxHp = 500;         // Bara värdens värde ändras.
unit.NetworkmaxHp = 500;  // Skickas också till deltagarna.
```

Innan ett objekt spawnas (på en prefab eller direkt efter `Instantiate`) följer en direkt tilldelning också med när objektet spawnas.

### Testa

Med bara värden syns inte problemen ovan. Innan du publicerar, kontrollera både värdens och deltagarens skärm på två datorer, och testa också fallet där någon ansluter senare efter att du ändrat ett värde. Om du aktiverar ett lokalt paket (`WorkshopLocal`) kan andra inte ansluta till rummet, så gör flerspelartester genom att prenumerera på ett objekt du laddat upp till Workshop som Unlisted. Ett objekt du laddar upp för första gången blir automatiskt Unlisted.


## Felsökning

| Symptom | Det här bör du kontrollera |
|---|---|
| Statussammanfattningen säger att startpunktsklassen inte kunde hittas | Namnrymden eller klassnamnet i `entryClass` matchar inte din kod. Nästlade klasser skrivs `Outer+Inner`. |
| Statussammanfattningen visar ett typfel för startpunktsklassen | Startpunktsklassen ärver inte från `WorkshopModBase` (eller `HorayModBase`), är inte public, är abstract eller saknar en public parameterlös konstruktor. |
| Statussammanfattningen säger att modden hölls tillbaka på grund av en assemblynamnskonflikt | En annan mod (inklusive AddOns) använder samma `AssemblyName`. Byt namn på din. |
| Statussammanfattningen visar ett fel vid inläsning av bibliotek | En DLL som listas i `libraries` saknas eller är inte en hanterad assembly. |
| `FileNotFoundException` i loggen | En refererad DLL finns inte i din modmapp eller är inte deklarerad i `libraries`. Eller så kopierades en spel-DLL med `Private=true` och de två står i konflikt. |
| Undantag direkt efter att du gått in i världen | Ett undantag i en databashändelsehanterare. Leta efter din mods namnrymd i loggens stackspårning. |
| Effekter staplas när du går tillbaka till titelskärmen och in igen | Du prenumererar igen inuti en hanterare. Prenumerera bara en gång, i `OnModLoaded`. |


## Exempel

[`examples/dll-sample`](../../examples/dll-sample) är en minimal mod som höjer Fysisk skada med 5 när spelaren spawnar. Dess `package/`-mapp innehåller en färdigbyggd DLL, så om du kopierar den mappen till `WorkshopLocal/` fungerar den direkt utan att du behöver bygga. För att göra den till din egen mod behöver du bara ändra `AssemblyName`, `modId`, `entryClass` och utdatasökvägen. Starta ett spel och öppna statfönstret för att se att det fungerar som Fysisk skada +5.
