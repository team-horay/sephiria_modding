# DLL-Mods

Mit DLL-Modulen kannst du in C# geschriebenen Code im Spiel ausführen. Abonniere `HorayModAPI`, um dich in Spieler, Items, UI und mehr einzuklinken.
Dieses Dokument behandelt alles vom Erstellen eines Projekts bis zum ersten Lauf deiner DLL im Spiel. Die vollständige Liste der Events und Registrierungsfunktionen findest du in der [API-Referenz](api/README.md).


## Voraussetzungen

- [.NET SDK](https://dotnet.microsoft.com/download) 8 oder neuer
- Ein C#-Editor


## Ein Projekt erstellen

Orientiere dich am Beispiel [`examples/dll-sample`](../../examples/dll-sample). Dieses Dokument verwendet es als Referenz.

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
    <!-- Installationsordner des Spiels. Passe ihn an deine Umgebung an oder übergib ihn mit dotnet build -p:GameDir=... -->
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
    <!-- Füge auf dieselbe Weise Mirror hinzu, wenn du mit Netzwerkobjekten wie PlayerAvatar arbeitest, UnityEngine.UI, wenn du UI baust, und 0Harmony, wenn du patchst -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Ziele auf `netstandard2.1`** und setze die Sprachversion auf C# 9.
2. **Verweise auf die Spiel-DLLs, aber kopiere sie nicht.** Richte die Verweise auf die Dateien in `<Installationsordner des Spiels>/Sephiria_Data/Managed/` und setze sie alle auf `Private=false`. Werden Spiel-DLLs in deinen Mod-Ordner kopiert, wird dieselbe Assembly zweimal geladen, was schwer nachvollziehbare Fehler verursacht, und außerdem landen unnötig Dateien in der Prüfung.
3. `Assembly-CSharp.dll` enthält den Großteil des Codes, der das Spiel ausführt.


## Code-Struktur


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Wird einmal pro Prozess aufgerufen. Abonniere Events hier, und nur einmal.
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

        // Wird nur auf dem Host (Server) aufgerufen. Ändere den Spielzustand hier.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Wird bei jedem Teilnehmer aufgerufen. Der vom Server hinzugefügte Wert kommt über die Synchronisierung an.
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

Deine Klasse erbt Folgendes von der Basisklasse.

| Member | Beschreibung |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Wird mit den Werten aus `metadata.json` gefüllt. |
| `Context` | `PackageRoot` (absoluter Pfad des Mod-Ordners), `FileId` (Steam-Workshop-Beitrags-ID; bei lokalen Paketen ein separater Wert, der nie mit einer Steam-ID kollidiert), `Definition` (das validierte Manifest), `ResolvePath(relativePath)`. Verwende es, um Dateien in deinem Mod-Ordner zu finden. Der Installationspfad ist bei jedem Nutzer anders, also codiere ihn niemals fest. |
| `GetDataPackages()` | Die Liste der `data`-Modulpakete, die diese Mod als ihren `consumer` angeben. Rufe sie nach Abschluss der Aktivierung auf (in einem `OnLoad*Database`-Handler oder nach `OnAllDatabasesReady`). |

`Context` ist ab dem Aufruf von `OnModLoaded` gültig. Im Konstruktor ist es noch null.


## Lebenszyklus

DLL-Mods werden aktiviert, **wenn du ein Spiel startest und die Welt betrittst**. Auf dem Titelbildschirm läuft noch kein Code. Die Startreihenfolge ist wie folgt.

1. Die Weltszene wird geladen und `GameDataLoader` startet.
2. Die DLL-Module im Ladeplan werden der Reihe nach geladen, jede Einstiegsklasse wird instanziiert und `OnModLoaded` aufgerufen.
3. Während die Datenbanken initialisiert werden, feuern nacheinander Events wie `OnLoadItemDatabase` und `OnLoadUnitDatabase`.
4. Wenn alles erledigt ist, wird `OnAllDatabasesReady` aufgerufen.

## Abonnieren

Die Klasse `HorayModAPI` stellt vordefinierte Events bereit, mit denen du dich in viele Situationen einklinken kannst.

- Abonniere Events **nur einmal**, in `OnModLoaded`. Datenbank-Events feuern bei jedem Laden erneut, erledige also die Dinge, die „jedes Mal passieren müssen“ (Items registrieren usw.), innerhalb der Handler.
- Verwalte Abonnieren und Abbestellen sorgfältig. Bestelle die abonnierten Events in `OnModUnloaded` wieder ab (-=).

Die vollständige Liste der Events steht in der [API-Referenz](api/README.md). Die folgende Tabelle nennt das empfohlene Event für jedes Vorhaben.

| Was du tun willst | Event |
|---|---|
| Items oder Artefakte hinzufügen | `OnLoadItemDatabase` |
| Waffen, Wunder, Passive, Kostüme oder Props hinzufügen | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Gegner- oder NPC-Einheiten hinzufügen | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Reisende (Dorf-NPCs) hinzufügen | `OnLoadSocialIDDatabase` |
| Eine Wertanzeige hinzufügen | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Lokalisierten Text aus dem Code registrieren | `OnLocalizationReady` (`AddText`) |
| Eine Schaltfläche zum Titelmenü hinzufügen | `OnTitleMenuReady` |
| Spiel- oder Sitzungsstart | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Wenn eine Etage generiert wird | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Spieler oder Einheit spawnt | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| In Kampfprüfungen einklinken | `OnPlayerEvade`, `OnGetDebuff` |
| Berechtigungen zur Inventarmanipulation | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Konflikte vermeiden

Entitäten, die deine Mod hinzufügt, werden im Format `<modId>:<id>` benannt, z. B. `yourname.artifact-pack:flame-ring`. Diese Regel verhindert Konflikte mit anderen Mods, und so werden Mod-Inhalte in Durchlauf-Historie und Statistiken auseinandergehalten.


## Serverseitig, clientseitig

- Sephiria ist auf den Mehrspielermodus ausgelegt, daher gibt es viele Events getrennt in einer serverseitigen und einer clientseitigen Version.
- Der Host feuert beide; andere Teilnehmer feuern nur die Clientside-Version.
- Grundsätzlich gilt: Erledige Dinge, die den Spielzustand ändern (Spawnen, Drops, Schaden), nur in Serverside, und Dinge, die nur die Anzeige ändern, in Clientside.


## Harmony-Patches

Wenn du etwas ändern musst, das Events nicht erreichen, kannst du Harmony verwenden.
Seit Patch 1.1.0 ist **Harmony 2.4.3 im Spiel eingebaut**, füge also einfach einen Verweis darauf hinzu.

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

- **Lege `0Harmony.dll` nicht in deine Mod.** Eine mitgelieferte Kopie wird ignoriert, und selbst wenn du eine andere Version beilegst, läuft deine Mod nicht mit dieser Version.
- Verwendest du deine `modId` unverändert als Harmony-ID, kollidiert sie nicht mit anderen.
- Patches gehen bei Spiel-Updates deutlich leichter kaputt als die offizielle API. Erledige, was du kannst, mit Events, und halte Patches als letztes Mittel bereit.
- Externe Loader wie BepInEx und MelonLoader werden nicht unterstützt. Eine Mod, die wie ein externer Loader arbeitet (also Plugin-DLLs von außerhalb des Workshops findet und ausführt), erhält eine Warnung und wird erst aktiviert, nachdem der Nutzer zugestimmt hat. Details findest du unter [Mods, die Code von außerhalb des Workshops ausführen](#mods-die-code-von-außerhalb-des-workshops-ausführen).


## Hilfs-DLLs

Wenn deine Mod andere Bibliotheken verwendet (z. B. Drittanbieter-Pakete außer Newtonsoft.Json), lege deren DLLs in deinen Mod-Ordner und deklariere sie in `libraries` in `metadata.json`. Nicht deklarierte DLLs werden nicht zur Verweisauflösung verwendet.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Lege keine DLLs bei, die das Spiel bereits mitbringt (Unity, Mirror, Newtonsoft.Json, 0Harmony usw.). Die Kopie des Spiels wird zuerst geladen, die Kopie deiner Mod wird also entweder ignoriert oder sorgt für Konflikte.
- Native (C/C++) DLLs können nicht verwendet werden. Es werden nur verwaltete Assemblys geladen.
- Wurde eine Assembly mit demselben Namen bereits von einer anderen Mod geladen, wählt das Spiel bei einer DLL in `modules` keine der beiden aus, sondern **hält die Ausführung zurück**. Bei einer DLL in `libraries` verwendet das Spiel die bereits geladene und ignoriert deine mitgelieferte Kopie, was bei abweichenden Versionen zu Fehlfunktionen führen kann. Es ist sicherer, gemeinsam genutzte Bibliotheken in eine separate Mod auszulagern und sie über `dependencies` zu verknüpfen.

### Die Bibliothek einer anderen Mod verwenden

Wenn mehrere Mods denselben Code verwenden, lege diesen Code in eine übergeordnete Mod und verknüpfe die untergeordneten Mods über `dependencies` damit.

1. Die übergeordnete Mod deklariert den gemeinsamen Code in `modules` oder `libraries` und trägt `Library` in ihre `tags` ein.
2. Die untergeordnete Mod trägt die `modId` der übergeordneten Mod in `dependencies` ein und wird mit `Private=false` gegen deren DLL gebaut. **Kopiere die DLL der übergeordneten Mod nicht in die untergeordnete Mod.**
3. Das Spiel lädt die übergeordnete Mod zuerst, daher werden die Verweise der untergeordneten Mod auf die bereits geladene Assembly der übergeordneten Mod aufgelöst.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Auch die untergeordnete Mod braucht ein eigenes `dll`-Modul und eine eigene `entryClass`. Dass die übergeordnete Mod die DLLs der untergeordneten Mod selbst sucht und lädt, wird nicht unterstützt. Lass stattdessen die untergeordnete Mod in ihrem eigenen `OnModLoaded` die Registrierungsfunktion der übergeordneten Mod aufrufen.
- Ändert die übergeordnete Mod in einem Update ihre öffentlichen Typen oder Member, können untergeordnete Mods kaputtgehen. Entferne oder ändere in einer gemeinsam genutzten Bibliothek keine bestehenden öffentlichen APIs.

### Erzeuge keine DLLs zur Laufzeit!

Jeder Code, den eine Mod ausführt, muss in einem geprüften Workshop-Beitrag enthalten sein. Ändern sich die DLL-Dateien einer Mod, während die Mod läuft, wird das Laden dieser Mod eingeschränkt. Lege alle DLLs unverändert in den Workshop-Beitrag, ohne jegliche Komprimierung oder Streaming. Folgendes kann also gegen das Prüfsystem verstoßen. Lege alle benötigten DLLs beim Upload in die Mod.

- Die Mod lädt während der Ausführung eine DLL von außen herunter und lädt die Assembly
- Die Mod erzeugt während der Ausführung eine eigene DLL und führt sie aus (einschließlich des Umschreibens einer anderen DLL mit Mono.Cecil o. Ä. und anschließender Ausführung)
- Die Mod entpackt eine DLL, die mit einem eigenen Komprimierungsverfahren in ihr verpackt ist, und verwendet sie

### Mods, die Code von außerhalb des Workshops ausführen

Eine Mod, die DLLs findet und ausführt, die der Nutzer **außerhalb ihres Mod-Ordners** ablegt, etwa im Spielinstallationsordner oder in `AppData` (zum Beispiel eine Kompatibilitäts-Mod für Plugins eines anderen Loaders), wird nicht abgelehnt, das Prüfsystem versieht sie aber mit einer **Warnung**.

- Beim Aktivieren der Mod erscheint eine Warnung, und die Mod wird nur aktiviert, wenn der Nutzer zustimmt. Wird die Mod aktualisiert, wird erneut gefragt.
- Auf diese Weise ausgeführte externe DLLs werden nicht geprüft, und für dadurch verursachte Probleme gibt es keinen Support.
- Eine Mod, die solche DLLs während der Ausführung selbst herunterlädt, fällt unter die oben genannten verbotenen Fälle.

Wenn du den Code einer anderen Mod verwenden möchtest, lade DLLs nicht direkt aus deren Ordner, sondern verknüpfe sie über `dependencies`, wie unter [Die Bibliothek einer anderen Mod verwenden](#die-bibliothek-einer-anderen-mod-verwenden) beschrieben. DLLs, die das Spiel nicht selbst geladen hat, durchlaufen weder Ladereihenfolge noch Namenskonflikt- oder Abhängigkeitsprüfung und werden daher nicht unterstützt.

Wenn du eine DLL umschreiben musst (zum Beispiel IL für eine andere Harmony-API umwandeln), tu das vor dem Upload und lege die umgewandelte DLL in deine Mod.


## Bauen und Testen

```bash
dotnet build -c Release
```

- Sobald der Build erfolgreich ist und die DLL in `WorkshopLocal/<mod>/bin/` erscheint, starte das Spiel, prüfe, ob deine Mod im Steam-Workshop-Panel auf dem Titelbildschirm unter „Meine Mods“ auftaucht, und starte dann ein Spiel.
- Wenn du die `.pdb` neben der Build-Ausgabe behältst, enthalten Stacktraces im Log Zeilennummern. Lass sie bei der Veröffentlichung weg.


## Spiel-Updates und Kompatibilität

Wenn das Spiel aktualisiert wird, ändert sich `Assembly-CSharp.dll`. Mods, die nur offizielle Events verwenden, funktionieren meist weiter, aber Mods, die interne Klassen direkt anfassen oder mit Harmony patchen, können kaputtgehen.

- Baue nach einem Update gegen den neuen `Managed/`-Ordner neu und teste.
- Wenn du die **unterstützte Spielversion** in der Beschreibung deiner Workshop-Seite vermerkst, finden Nutzer die Ursache schnell. Die Spielversion wird beim Veröffentlichen möglicherweise auch automatisch in den Metadaten des Workshop-Beitrags festgehalten.
- Wenn du deinen Code änderst, muss er erneut beim Prüfsystem eingereicht werden. Siehe [Im Workshop veröffentlichen](publishing.md#code-prüfung).


## Hinweise zum Mehrspielermodus

- Bei den meisten Code-Mods wird `multiplayer.installation` auf `everyone` stehen.
- Wenn deine Mod den Spielzustand nur in `Serverside`-Events ändert und die geänderten Werte bereits synchronisierter Spielzustand sind (z. B. `customStats`), sodass die Teilnehmer weder Code noch Registrierung brauchen, kannst du sie als `host_only` deklarieren. Teilnehmer laden die Mod dann nicht herunter. Eine Mod, die neue Items registriert, `RegisterNetworkPrefab` verwendet oder irgendetwas in `Clientside`-Events ändert, kann nicht `host_only` sein.
- Objekte, die über das Netzwerk gespawnt werden (Einheiten, Projektile usw.), müssen mit `RegisterNetworkPrefab` registriert werden, damit sie auch auf der Seite der Teilnehmer erstellt werden. Wenn du einen `stableKey` übergibst, bleibt dieselbe ID auch bei Änderungen deiner Mod-Version erhalten.
- Host und Teilnehmer müssen sogar dieselbe **Ladereihenfolge** der Mods haben. Vermeide Code, dessen Ergebnisse von dieser Reihenfolge abhängen.


## Netzwerksynchronisierung (SyncVar, RPC, Command)

Der Mehrspielermodus von Sephiria basiert auf der Open-Source-Netzwerkbibliothek „Mirror“. In Mirror kannst du Netzwerkfunktionen einfach nutzen, indem du die folgenden Attribute deklarierst.

- `[SyncVar]`: Ein Attribut, das du an eine Membervariable einer Klasse hängst. Es synchronisiert den Wert der Variable zwischen Server und Clients.
- `[ClientRpc]`, `[TargetRpc]`: Attribute, die du an eine Funktion hängst. Sie fordern vom Server aus Clients auf, die Funktion auszuführen.
- `[Command]`: Ein Attribut, das du an eine Funktion hängst. Es fordert von einem Client aus den Server auf, die Funktion auszuführen.

In einer Mod-Umgebung lassen sich die obigen Attribute nicht normal verwenden. Sie funktionieren nur, wenn der Weaver von Mirror beim Kompilieren den passenden Übertragungscode einfügt, und eine außerhalb erzeugte DLL durchläuft den Weaver nicht. Selbst wenn der Build erfolgreich ist, tun die obigen Attribute daher nichts. Ein solcher Fehler sieht auf dem Bildschirm des Hosts normal aus und tritt nur auf dem Bildschirm der Teilnehmer auf, deshalb ist er beim Testen allein schwer zu finden. (Selbst wenn man den Code zwangsweise durch den Weaver schickt, kann es noch Fehler wie Kollisionen bei Funktions-Hashes geben.)


### `SyncVar<T>`, `ModRpc`

Um das zu lösen, stellt Sephiria Netzwerkcode für Mods bereit, der die obigen Attribute ersetzt: `SyncVar<T>` und `ModRpc`.

| Was du tun willst | Was du verwendest |
|---|---|
| Einen Wert dauerhaft abgleichen (einschließlich Teilnehmern, die später beitreten) | `SyncVar<T>` |
| Server → Aufruf bei allen Clients | `ModRpc.Send` |
| Server → Aufruf bei einem bestimmten Client | `ModRpc.SendTo` |
| Client → Aufruf beim Server | `ModRpc.SendCommand` |
| Einen Wert ändern, den das Spiel bereits synchronisiert | Eigenschaft `Network<FieldName>` |

- Diese Funktion ist ab Spielversion 1.1.2 verfügbar.
- Eine Mod, die diese Funktion nutzt, sollte `multiplayer.installation` als `everyone` deklarieren.


### Komponente vorbereiten

`SyncVar<T>` und `ModRpc` lassen sich nur in einer Komponente verwenden, die von `NetworkBehaviour` erbt. Diese Komponente muss beim Host und beim Teilnehmer am selben Objekt **in derselben Reihenfolge** angehängt sein. Hängst du sie per `AddComponent` an ein bereits gespawntes Objekt an, wird sie nicht in die Synchronisierung aufgenommen. Sie muss also schon angehängt sein, bevor du das Objekt als Netzwerk-Prefab registrierst.

```csharp
private static GameObject prefab;

// Abonniere in OnModLoaded HorayModAPI.OnAllDatabasesReady.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Lege es unter einem inaktiven Parent ab. Das Awake des Prefabs läuft nicht, und eine per Instantiate erzeugte Kopie startet aktiv.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Spawne auf dem Server. Wenn du das zweite Argument übergibst, wird dieser Spieler zum Besitzer.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: Werte synchronisieren

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Es gibt keinen Weaver, also registriere direkt im Konstruktor. Die Reihenfolge der Felder muss immer gleich sein.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Ändere den Wert **auf dem Server**, etwa mit `stack.Value = 3`. Ein von einem Teilnehmer geänderter Wert wird nicht an den Server übertragen.
- Auch ein Teilnehmer, der später beitritt, erhält beim Betreten den aktuellen Wert.
- `Callback` wird beim Host aufgerufen, wenn du einen Wert zuweist, und beim Teilnehmer, wenn er einen Wert empfängt. Er wird auch aufgerufen, wenn der beim Betreten empfangene Wert vom Standardwert abweicht. Eine Exception in einem Callback wird ins Log geschrieben, und die Synchronisierung läuft weiter.
- Du kannst Grundtypen wie `int`, `float`, `string` und `Vector3` verwenden. Für ein von der Mod definiertes struct wird kein Serialisierungscode automatisch erzeugt, teile es daher in einzelne Felder auf.
- Um auf ein anderes Objekt zu verweisen, sende statt `SyncVar<GameObject>` dessen `netId` als `SyncVar<uint>`. Auf der Empfängerseite ist das Objekt möglicherweise noch nicht erzeugt.
- Eine einzelne Komponente kann höchstens 64 SyncVars registrieren, geerbte eingerechnet.

### `ModRpc`: Funktionen aufrufen

Registriere eine Funktion einmal beim Laden der Mod. Der Name beim Aufruf lautet `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Server → alle Clients, die dieses Objekt sehen
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Server → ein Client. Übergibst du für die Verbindung null, geht es an den Besitzer des Objekts.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Client → Server
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Lies in der Reihenfolge, in der du geschrieben hast. Unterscheiden sich Reihenfolge oder Typen von Schreiben und Lesen, werden Werte verfälscht oder es wird eine Exception ausgelöst.
- `RegisterCommand` kann standardmäßig **nur vom Besitzer des Objekts** aufgerufen werden. Um es an einem Objekt ohne Besitzer zu empfangen, übergib `requiresAuthority: false`. In diesem Fall kann jeder Teilnehmer es aufrufen. Achte auf Cheats und Ähnliches.
- Sendest du in eine andere Richtung als die registrierte oder sendest du einen nicht registrierten Namen, wird nur ein Fehler ins Log geschrieben und nichts gesendet.
- Fehlt die Funktion auf der Empfängerseite oder ist der Komponententyp ein anderer, wird nur eine Warnung ins Log geschrieben und der Aufruf ignoriert. Auch eine Exception in einem Handler wird ins Log geschrieben, und das Spiel läuft weiter.
- Der Standardkanal ist zuverlässige Übertragung. Für häufig gesendete Effekte kannst du das Argument `channelId: Channels.Unreliable` übergeben.

### Was du unbedingt beachten musst

- Ein Client kann über ein Command beliebige Werte senden. Prüfe Slot-Nummern, Mengen, Ziele und Ähnliches im Server-Handler.
- Ein Teilnehmer, der später beitritt, erhält RPCs nicht, die vor seinem Betreten gesendet wurden. Sende Zustand, der erhalten bleiben muss, per `SyncVar<T>`, und nutze RPCs nur für flüchtige Ereignisse wie Sounds und Effekte.
- **Ein RPC kann vor der SyncVar eintreffen.** Liest du in einem RPC-Handler `stack.Value`, kann es noch der vorherige Wert sein. Brauchst du den neuen Wert, sende ihn als RPC-Argument mit oder verarbeite ihn in `Callback`.

### Werte im Basisspielcode von Sephiria ändern

Weist du einem `[SyncVar]`-Feld einer Spielklasse direkt etwas zu, wird es nicht an die Teilnehmer übertragen. Verwende die Eigenschaft mit vorangestelltem `Network`. Die Groß- und Kleinschreibung entspricht genau dem Feldnamen.

```csharp
unit.maxHp = 500;         // Nur der Wert des Hosts ändert sich.
unit.NetworkmaxHp = 500;  // Wird auch an die Teilnehmer übertragen.
```

Bevor ein Objekt gespawnt wird (am Prefab oder direkt nach `Instantiate`), wird auch eine direkte Zuweisung beim Spawnen mit übertragen.

### Testen

Mit dem Host allein treten die obigen Probleme nicht zutage. Prüfe vor der Veröffentlichung auf zwei PCs sowohl den Bildschirm des Hosts als auch den der Teilnehmer, und teste auch den Fall, dass jemand nach einer Wertänderung später beitritt. Wenn du ein lokales Paket (`WorkshopLocal`) aktivierst, können andere dem Raum nicht beitreten. Führe Mehrspielertests daher durch, indem du einen im Workshop als Unlisted hochgeladenen Beitrag abonnierst. Ein erstmals hochgeladener Beitrag ist automatisch Unlisted.


## Fehlerbehebung

| Symptom | Was zu prüfen ist |
|---|---|
| Die Statuszusammenfassung sagt, die Einstiegsklasse wurde nicht gefunden | Namespace oder Klassenname in `entryClass` passen nicht zu deinem Code. Verschachtelte Klassen werden als `Outer+Inner` geschrieben. |
| Die Statuszusammenfassung zeigt einen Typfehler der Einstiegsklasse | Die Einstiegsklasse erbt nicht von `WorkshopModBase` (oder `HorayModBase`), ist nicht public, ist abstract oder hat keinen öffentlichen parameterlosen Konstruktor. |
| Die Statuszusammenfassung sagt, die Mod wurde wegen eines Assembly-Namenskonflikts zurückgehalten | Eine andere Mod (einschließlich AddOns) verwendet denselben `AssemblyName`. Benenne deinen um. |
| Die Statuszusammenfassung zeigt einen Fehler beim Laden einer Bibliothek | Eine in `libraries` aufgeführte DLL fehlt oder ist keine verwaltete Assembly. |
| `FileNotFoundException` im Log | Eine referenzierte DLL liegt nicht in deinem Mod-Ordner oder ist nicht in `libraries` deklariert. Oder eine Spiel-DLL wurde mit `Private=true` kopiert und die beiden kollidieren. |
| Exception direkt nach dem Betreten der Welt | Eine Exception in einem Datenbank-Event-Handler. Suche im Stacktrace des Logs nach dem Namespace deiner Mod. |
| Effekte stapeln sich nach Rückkehr zum Titel und erneutem Betreten | Du abonnierst erneut innerhalb eines Handlers. Abonniere nur einmal, in `OnModLoaded`. |


## Beispiel

[`examples/dll-sample`](../../examples/dll-sample) ist eine minimale Mod, die den Physischen Schaden um 5 erhöht, wenn der Spieler spawnt. Ihr Ordner `package/` enthält eine vorgebaute DLL, sodass das Kopieren dieses Ordners nach `WorkshopLocal/` sofort ohne Build funktioniert. Um daraus deine eigene Mod zu machen, musst du nur `AssemblyName`, `modId`, `entryClass` und den Ausgabepfad ändern. Starte ein Spiel und öffne das Wertefenster, um es als Physischer Schaden +5 in Aktion zu sehen.
