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
- Externa laddare som BepInEx och MelonLoader stöds inte.


## Hjälp-DLL:er

Om din mod använder andra bibliotek (t.ex. tredjepartspaket utöver Newtonsoft.Json) lägger du deras DLL-filer i din modmapp och deklarerar dem i `libraries` i `metadata.json`. DLL-filer som inte deklareras används inte vid referensupplösning.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Ta inte med DLL-filer som redan följer med spelet (Unity, Mirror, Newtonsoft.Json, 0Harmony och så vidare). Spelets kopia läses in först, så kopian i din mod ignoreras antingen eller orsakar konflikter.
- Native-DLL:er (C/C++) kan inte användas. Endast hanterade assemblies läses in.
- Om en assembly med samma namn redan har lästs in av en annan mod väljer spelet inte en av de två; det **avvaktar med att köra** den. Det är säkrare att bryta ut delade bibliotek till en separat mod och koppla den via `dependencies`.


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
- Om du ändrar din kod måste den godkännas i granskningen igen innan du kan publicera. Se [Publicera på Workshop](publishing.md).


## Att tänka på i flerspelarläge

- De flesta kodmoddar kommer att ha `multiplayer.installation` satt till `everyone`.
- Om din mod bara ändrar spelets tillstånd i `Serverside`-händelser, och värdena den ändrar är speltillstånd som redan synkroniseras (t.ex. `customStats`) så att deltagarna inte behöver någon kod eller registrering, kan du deklarera den som `host_only`. Deltagarna laddar då inte ner modden. En mod som registrerar nya föremål, använder `RegisterNetworkPrefab` eller ändrar något i `Clientside`-händelser kan inte vara `host_only`.
- Objekt som spawnas över nätverket (enheter, projektiler och så vidare) måste registreras med `RegisterNetworkPrefab` för att skapas även på deltagarnas sida. Om du anger en `stableKey` behålls samma ID även när din modversion ändras.
- Värden och deltagarna måste till och med ha samma **laddningsordning** för moddar. Undvik kod vars resultat beror på den ordningen.


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
