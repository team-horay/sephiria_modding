# Mody DLL

Moduły DLL pozwalają uruchamiać w grze kod napisany w C#. Subskrybuj `HorayModAPI`, aby podpiąć się do graczy, przedmiotów, interfejsu i więcej.
Ten dokument obejmuje wszystko od utworzenia projektu po uruchomienie pierwszego DLL w grze. Pełną listę zdarzeń i funkcji rejestrujących znajdziesz w [dokumentacji API](api/README.md).


## Wymagania

- [.NET SDK](https://dotnet.microsoft.com/download) 8 lub nowszy
- Edytor C#


## Tworzenie projektu

Skorzystaj z przykładu [`examples/dll-sample`](../../examples/dll-sample). Ten dokument odwołuje się do niego jako do wzorca.

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
    <!-- Folder instalacyjny gry. Zmień go odpowiednio do swojej konfiguracji albo przekaż przez dotnet build -p:GameDir=... -->
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
    <!-- W ten sam sposób dodaj Mirror, jeśli pracujesz z obiektami sieciowymi, np. PlayerAvatar; UnityEngine.UI, jeśli budujesz interfejs; 0Harmony, jeśli stosujesz patche -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Celuj w `netstandard2.1`** i ustaw wersję języka na C# 9.
2. **Odwołuj się do plików DLL gry, ale ich nie kopiuj.** Skieruj odwołania na pliki w `<folder instalacyjny gry>/Sephiria_Data/Managed/` i ustaw dla wszystkich `Private=false`. Jeśli pliki DLL gry zostaną skopiowane do folderu moda, ten sam zestaw załaduje się dwukrotnie, co powoduje trudne do zdiagnozowania błędy, a dodatkowo niepotrzebnie dokłada pliki do weryfikacji.
3. `Assembly-CSharp.dll` zawiera większość kodu, który obsługuje grę.


## Struktura kodu


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Wywoływane raz na proces. Tutaj subskrybuj zdarzenia, i tylko raz.
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

        // Wywoływane tylko u hosta (serwera). Tutaj zmieniaj stan gry.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Wywoływane u każdego uczestnika. Wartość dodana przez serwer dociera przez synchronizację.
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

Twoja klasa dziedziczy po klasie bazowej następujące składowe.

| Składowa | Opis |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Wypełniane wartościami z `metadata.json`. |
| `Context` | `PackageRoot` (bezwzględna ścieżka folderu moda), `FileId` (ID elementu w Warsztacie Steam; dla pakietów lokalnych osobna wartość, która nigdy nie koliduje z ID Steam), `Definition` (zweryfikowany manifest), `ResolvePath(relativePath)`. Używaj go do wyszukiwania plików w folderze moda. Ścieżka instalacji jest inna u każdego użytkownika, więc nigdy nie wpisuj jej na sztywno. |
| `GetDataPackages()` | Lista pakietów modułu `data`, które wskazują tego moda jako swojego `consumer`. Wywołuj po zakończeniu aktywacji (w handlerze `OnLoad*Database` albo po `OnAllDatabasesReady`). |

`Context` jest ważny od momentu wywołania `OnModLoaded`. W konstruktorze jest jeszcze null.


## Cykl życia

Mody DLL są aktywowane **po rozpoczęciu gry i wejściu do świata**. Na ekranie tytułowym żaden kod jeszcze się nie wykonuje. Kolejność uruchamiania jest następująca.

1. Ładuje się scena świata i startuje `GameDataLoader`.
2. Moduły DLL z planu ładowania są ładowane po kolei, każda klasa wejścia jest instancjonowana i wywoływane jest `OnModLoaded`.
3. W miarę inicjalizacji baz danych kolejno odpalają się zdarzenia takie jak `OnLoadItemDatabase` i `OnLoadUnitDatabase`.
4. Gdy wszystko jest gotowe, wywoływane jest `OnAllDatabasesReady`.

## Subskrybowanie

Klasa `HorayModAPI` udostępnia predefiniowane zdarzenia, które pozwalają podpiąć się do wielu sytuacji.

- Subskrybuj zdarzenia **tylko raz**, w `OnModLoaded`. Zdarzenia baz danych odpalają się ponownie przy każdym ładowaniu, więc rzeczy, które „muszą się wykonać za każdym razem” (rejestrowanie przedmiotów itp.), rób wewnątrz handlerów.
- Starannie zarządzaj subskrypcjami i ich anulowaniem. W `OnModUnloaded` anuluj subskrypcję (-=) zdarzeń, które subskrybujesz.

Pełna lista zdarzeń znajduje się w [dokumentacji API](api/README.md). Poniższa tabela zawiera zalecane zdarzenie dla każdej rzeczy, którą możesz chcieć zrobić.

| Co chcesz zrobić | Zdarzenie |
|---|---|
| Dodać przedmioty lub artefakty | `OnLoadItemDatabase` |
| Dodać bronie, cuda, pasywy, stroje lub rekwizyty | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Dodać jednostki wrogów lub NPC | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Dodać wędrowców (NPC w wiosce) | `OnLoadSocialIDDatabase` |
| Dodać wyświetlanie statystyki | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Zarejestrować zlokalizowany tekst z kodu | `OnLocalizationReady` (`AddText`) |
| Dodać przycisk do menu tytułowego | `OnTitleMenuReady` |
| Start gry lub sesji | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Generowanie piętra | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Pojawienie się gracza lub jednostki | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Podpiąć się do sprawdzeń walki | `OnPlayerEvade`, `OnGetDebuff` |
| Uprawnienia do modyfikacji ekwipunku | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Unikanie konfliktów

Encje dodawane przez twojego moda są nazywane w formacie `<modId>:<id>`, np. `yourname.artifact-pack:flame-ring`. Ta zasada zapobiega konfliktom z innymi modami i pozwala odróżniać zawartość modów w historii rozgrywek i statystykach.


## Strona serwera, strona klienta

- Sephiria jest zbudowana wokół trybu wieloosobowego, dlatego wiele zdarzeń ma osobne wersje po stronie serwera i po stronie klienta.
- Host odpala obie; pozostali uczestnicy odpalają tylko wersję Clientside.
- Z zasady rzeczy zmieniające stan gry (spawnowanie, dropy, obrażenia) rób tylko w Serverside, a rzeczy zmieniające wyłącznie to, co jest wyświetlane, w Clientside.


## Patche Harmony

Gdy musisz zmienić coś, do czego zdarzenia nie sięgają, możesz użyć Harmony.
Od patcha 1.1.0 **Harmony 2.4.3 jest wbudowane** w grę, więc wystarczy dodać do niego odwołanie.

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

- **Nie dołączaj `0Harmony.dll` do swojego moda.** Dołączona kopia jest ignorowana, a nawet jeśli dołączysz inną wersję, twój mod nie uruchomi się na tej wersji.
- Użycie swojego `modId` bez zmian jako ID Harmony chroni przed kolizjami z innymi.
- Patche psują się przy aktualizacjach gry znacznie łatwiej niż oficjalne API. Rób, co się da, za pomocą zdarzeń, a patche traktuj jako ostateczność.
- Zewnętrzne loadery, takie jak BepInEx i MelonLoader, nie są obsługiwane. Mod, który działa jak zewnętrzny loader (znajduje i uruchamia pliki DLL wtyczek spoza Warsztatu), otrzymuje ostrzeżenie i zostaje włączony dopiero po zaakceptowaniu go przez użytkownika. Szczegóły znajdziesz w sekcji [Mody uruchamiające kod spoza Warsztatu](#mody-uruchamiające-kod-spoza-warsztatu).


## Pomocnicze pliki DLL

Jeśli twój mod korzysta z innych bibliotek (np. pakietów zewnętrznych innych niż Newtonsoft.Json), umieść ich pliki DLL w folderze moda i zadeklaruj je w `libraries` w `metadata.json`. Niezadeklarowane pliki DLL nie są używane do rozwiązywania odwołań.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Nie dołączaj plików DLL, które są już dostarczane z grą (Unity, Mirror, Newtonsoft.Json, 0Harmony itp.). Kopia gry ładuje się jako pierwsza, więc kopia dołączona do moda jest ignorowana albo powoduje konflikt.
- Natywnych (C/C++) plików DLL nie można używać. Ładowane są tylko zestawy zarządzane.
- Jeśli zestaw o tej samej nazwie został już załadowany przez innego moda, to w przypadku pliku DLL z `modules` gra nie wybiera jednego z dwóch, lecz **wstrzymuje uruchomienie**. W przypadku pliku DLL z `libraries` gra używa tego, który jest już załadowany, i ignoruje dołączoną przez ciebie kopię, co przy różnych wersjach może powodować błędy. Bezpieczniej jest wydzielić współdzielone biblioteki do osobnego moda i powiązać go przez `dependencies`.

### Korzystanie z biblioteki innego moda

Jeśli kilka modów używa tego samego kodu, umieść ten kod w jednym modzie nadrzędnym, a mody podrzędne powiąż z nim przez `dependencies`.

1. Mod nadrzędny deklaruje wspólny kod w `modules` lub `libraries` i dodaje `Library` do swoich `tags`.
2. Mod podrzędny wpisuje `modId` moda nadrzędnego w `dependencies` i jest budowany z odwołaniem do pliku DLL moda nadrzędnego z `Private=false`. **Nie kopiuj pliku DLL moda nadrzędnego do moda podrzędnego.**
3. Gra ładuje najpierw moda nadrzędnego, więc odwołania moda podrzędnego są rozwiązywane do już załadowanego zestawu moda nadrzędnego.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Mod podrzędny nadal musi mieć własny moduł `dll` i `entryClass`. Struktura, w której mod nadrzędny sam wyszukuje i ładuje pliki DLL moda podrzędnego, nie jest obsługiwana. Zamiast tego niech mod podrzędny wywołuje funkcję rejestracji moda nadrzędnego z własnego `OnModLoaded`.
- Jeśli mod nadrzędny zmieni w aktualizacji swoje typy lub składowe publiczne, mody podrzędne mogą przestać działać. Nie usuwaj ani nie zmieniaj istniejących publicznych API we współdzielonej bibliotece.

### Nie twórz plików DLL w czasie działania!

Cały kod uruchamiany przez moda musi znajdować się w sprawdzonym elemencie Warsztatu. Jeśli pliki DLL w modzie zmienią się podczas działania moda, ładowanie tego moda zostaje ograniczone. Umieść wszystkie pliki DLL w elemencie Warsztatu w niezmienionej postaci, bez żadnej kompresji ani strumieniowania. Innymi słowy, następujące działania mogą naruszać zasady systemu weryfikacji. Podczas przesyłania umieść w modzie wszystkie potrzebne pliki DLL.

- Mod podczas działania pobiera plik DLL z zewnątrz i ładuje zestaw
- Mod podczas działania tworzy i uruchamia własny plik DLL (w tym przepisanie innego pliku DLL za pomocą Mono.Cecil lub podobnego narzędzia, a następnie jego uruchomienie)
- Mod wypakowuje i wykorzystuje plik DLL spakowany w nim własną metodą kompresji

### Mody uruchamiające kod spoza Warsztatu

Mod, który wyszukuje i uruchamia pliki DLL umieszczone przez użytkownika **poza folderem moda**, np. w folderze instalacyjnym gry lub w `AppData` (na przykład mod zapewniający zgodność z wtyczkami innego loadera), nie jest odrzucany, ale system weryfikacji dodaje do niego **ostrzeżenie**.

- Gdy użytkownik włącza taki mod, pojawia się ostrzeżenie, a mod zostaje włączony tylko wtedy, gdy użytkownik je zaakceptuje. Po aktualizacji moda pytanie pojawia się ponownie.
- Zewnętrzne pliki DLL uruchamiane w ten sposób nie są sprawdzane, a problemy, które powodują, nie są objęte wsparciem.
- Mod, który sam pobiera takie pliki DLL podczas działania, podlega wymienionym wyżej zakazom.

Aby korzystać z kodu innego moda, nie ładuj plików DLL bezpośrednio z folderu tego moda. Powiąż go przez `dependencies`, jak opisano w sekcji [Korzystanie z biblioteki innego moda](#korzystanie-z-biblioteki-innego-moda). Pliki DLL, których gra nie załadowała samodzielnie, omijają kolejność ładowania oraz kontrole konfliktów nazw i zależności, dlatego nie są obsługiwane.

Jeśli musisz przepisać plik DLL (na przykład przekonwertować IL pod inne API Harmony), zrób to przed przesłaniem i dołącz do moda przekonwertowany plik DLL.


## Kompilacja i testowanie

```bash
dotnet build -c Release
```

- Gdy kompilacja się powiedzie i DLL pojawi się w `WorkshopLocal/<mod>/bin/`, uruchom grę, sprawdź, czy twój mod jest widoczny w sekcji „Moje mody” w panelu Warsztatu Steam na ekranie tytułowym, a następnie rozpocznij grę.
- Jeśli obok wyniku kompilacji zostawisz plik `.pdb`, ślady stosu w logu będą zawierać numery wierszy. Pomiń go przy publikacji.


## Aktualizacje gry i kompatybilność

Gdy gra się aktualizuje, zmienia się `Assembly-CSharp.dll`. Mody korzystające wyłącznie z oficjalnych zdarzeń zwykle działają dalej, ale mody, które bezpośrednio sięgają do wewnętrznych klas lub stosują patche Harmony, mogą przestać działać.

- Po aktualizacji skompiluj ponownie z nowym folderem `Managed/` i przetestuj.
- Jeśli w opisie na stronie Warsztatu zaznaczysz **obsługiwaną wersję gry**, użytkownicy szybko znajdą przyczynę problemu. Wersja gry może też być automatycznie zapisywana w metadanych elementu Warsztatu przy publikacji.
- Gdy zmienisz kod, musi on zostać ponownie zgłoszony do systemu weryfikacji. Zobacz [Publikowanie w Warsztacie](publishing.md#weryfikacja-kodu).


## Uwagi dotyczące trybu wieloosobowego

- Większość modów z kodem będzie miała `multiplayer.installation` ustawione na `everyone`.
- Jeśli twój mod zmienia stan gry wyłącznie w zdarzeniach `Serverside`, a zmieniane wartości to stan gry, który już jest synchronizowany (np. `customStats`), więc uczestnicy nie potrzebują żadnego kodu ani rejestracji, możesz zadeklarować go jako `host_only`. Uczestnicy nie pobierają wtedy moda. Mod, który rejestruje nowe przedmioty, używa `RegisterNetworkPrefab` lub zmienia cokolwiek w zdarzeniach `Clientside`, nie może być `host_only`.
- Obiekty spawnowane przez sieć (jednostki, pociski itp.) muszą być zarejestrowane przez `RegisterNetworkPrefab`, aby były tworzone także po stronie uczestników. Jeśli przekażesz `stableKey`, to samo ID zostaje zachowane nawet po zmianie wersji moda.
- Host i uczestnicy muszą mieć nawet tę samą **kolejność ładowania** modów. Unikaj kodu, którego wyniki zależą od tej kolejności.


## Synchronizacja sieciowa (SyncVar, RPC, Command)

Tryb wieloosobowy w Sephirii jest zaimplementowany na otwartoźródłowej bibliotece sieciowej „Mirror”. W Mirror można łatwo korzystać z komunikacji, deklarując następujące atrybuty.

- `[SyncVar]`: atrybut dla zmiennej składowej klasy. Synchronizuje wartość zmiennej między serwerem a klientami.
- `[ClientRpc]`, `[TargetRpc]`: atrybuty dla funkcji. Serwer prosi klientów o wykonanie funkcji.
- `[Command]`: atrybut dla funkcji. Klient prosi serwer o wykonanie funkcji.

W środowisku modów powyższe atrybuty nie działają poprawnie. Działają one tylko wtedy, gdy podczas kompilacji kodu Weaver z Mirror wstawi kod transportowy odpowiadający każdemu atrybutowi, a pliki DLL zbudowane na zewnątrz nie przechodzą przez Weaver, więc nawet jeśli kompilacja się powiedzie, te atrybuty nie robią nic. Taki błąd na ekranie hosta wygląda normalnie, a problemy pojawiają się tylko na ekranie uczestnika, więc trudno go wykryć, testując samodzielnie. (Nawet jeśli wymusisz przepuszczenie DLL przez Weaver, mogą wystąpić błędy, np. kolizje hashy funkcji.)


### `SyncVar<T>`, `ModRpc`

Aby to rozwiązać, Sephiria udostępnia kod komunikacyjny dla modów, który zastępuje powyższe atrybuty: `SyncVar<T>` i `ModRpc`.

| Co chcesz zrobić | Czego użyć |
|---|---|
| Stale utrzymywać zgodność wartości (także dla uczestników, którzy dołączyli później) | `SyncVar<T>` |
| Wywołanie z serwera do wszystkich klientów | `ModRpc.Send` |
| Wywołanie z serwera do konkretnego klienta | `ModRpc.SendTo` |
| Wywołanie z klienta do serwera | `ModRpc.SendCommand` |
| Zmienić wartość, którą gra już synchronizuje | Właściwość `Network<FieldName>` |

- Ta funkcja jest dostępna od wersji gry 1.1.2.
- Mody korzystające z tej funkcji powinny zadeklarować `multiplayer.installation` jako `everyone`.


### Przygotowanie komponentu

`SyncVar<T>` i `ModRpc` można stosować tylko w komponentach dziedziczących po `NetworkBehaviour`. Ten komponent musi być dołączony do tego samego obiektu zarówno u hosta, jak i u uczestnika, **w tej samej kolejności**. Komponent dodany przez `AddComponent` do obiektu, który został już zespawnowany, nie trafia do synchronizacji, więc musi być dołączony, zanim zarejestrujesz obiekt jako sieciowy prefab.

```csharp
private static GameObject prefab;

// Zasubskrybuj HorayModAPI.OnAllDatabasesReady w OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Umieść pod nieaktywnym rodzicem. Awake prefaba się nie wykonuje, a klon utworzony przez Instantiate startuje jako aktywny.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Spawnowanie na serwerze. Jeśli podasz drugi argument, ten gracz zostanie właścicielem.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: synchronizacja wartości

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Nie ma Weavera, więc zarejestruj ręcznie w konstruktorze. Kolejność pól musi być zawsze taka sama.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Wartość zmieniaj **na serwerze**, np. `stack.Value = 3`. Wartość zmieniona przez uczestnika nie jest przekazywana do serwera.
- Uczestnik, który dołączył później, również otrzymuje bieżącą wartość przy wejściu.
- `Callback` jest wywoływany u hosta przy przypisaniu wartości, a u uczestnika przy jej otrzymaniu. Jeśli wartość otrzymana przy wejściu różni się od domyślnej, jest on wywoływany także wtedy. Wyjątek zgłoszony w callbacku trafia do logu, a synchronizacja trwa dalej.
- Można używać podstawowych typów, takich jak `int`, `float`, `string`, `Vector3`. Kod serializacji dla struct utworzonych przez moda nie jest generowany automatycznie, więc rozbij je na osobne pola.
- Aby wskazać inny obiekt, zamiast `SyncVar<GameObject>` wyślij jego `netId` jako `SyncVar<uint>`. Po stronie odbiorcy ten obiekt może jeszcze nie istnieć.
- Na jednym komponencie można zarejestrować najwyżej 64 takie obiekty, wliczając odziedziczone.

### `ModRpc`: wywoływanie funkcji

Funkcję rejestruje się raz podczas ładowania moda. Przy wywołaniu jej nazwa ma postać `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Serwer → wszyscy klienci, którzy widzą ten obiekt
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Serwer → jeden klient. Jeśli zamiast połączenia podasz null, wysyła do właściciela obiektu.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Klient → serwer
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Czytaj w tej samej kolejności, w jakiej zapisywałeś. Jeśli kolejność lub typy przy zapisie i odczycie się różnią, wartości będą przekłamane albo wystąpi wyjątek.
- `RegisterCommand` domyślnie może wywołać **tylko właściciel obiektu**. Aby przyjmować wywołania na obiekcie bez właściciela, podaj `requiresAuthority: false`. W takim przypadku może go wywołać dowolny uczestnik. Miej na uwadze oszustwa i podobne zagrożenia.
- Wysłanie w innym kierunku niż zarejestrowany albo wysłanie niezarejestrowanej nazwy kończy się tylko wpisem błędu w logu i nic nie zostaje wysłane.
- Jeśli po stronie odbiorcy brakuje funkcji albo typ komponentu jest inny, do logu trafia tylko ostrzeżenie, a wywołanie jest ignorowane. Wyjątek w handlerze również trafia do logu, a gra działa dalej.
- Domyślny kanał używa niezawodnego transportu. Dla często wysyłanych efektów możesz podać argument `channelId: Channels.Unreliable`.

### Zasady, których trzeba bezwzględnie przestrzegać

- Klient może przez Command wysłać dowolne wartości. Weryfikuj numer slotu, ilość, cel itp. w handlerze po stronie serwera.
- Uczestnik, który dołączył później, nie otrzymuje RPC wysłanych przed jego wejściem. Stan, który musi być zachowany, wysyłaj przez `SyncVar<T>`, a RPC stosuj do rzeczy przemijających, takich jak dźwięki i efekty.
- **RPC może dotrzeć przed SyncVar.** Jeśli odczytasz `stack.Value` w handlerze RPC, może to być poprzednia wartość. Jeśli potrzebujesz nowej wartości, wyślij ją razem z argumentami RPC albo obsłuż ją w `Callback`.

### Zmiana wartości w kodzie podstawowym Sephirii

Bezpośrednie przypisanie do pola `[SyncVar]` klasy gry nie jest przekazywane uczestnikom. Użyj właściwości z przedrostkiem `Network`. Wielkość liter jest taka sama jak w nazwie pola.

```csharp
unit.maxHp = 500;         // Zmienia się tylko wartość u hosta.
unit.NetworkmaxHp = 500;  // Jest przekazywana także uczestnikom.
```

Przed zespawnowaniem obiektu (w prefabie lub tuż po `Instantiate`) bezpośrednie przypisanie też działa: wartość jest przekazywana razem z obiektem podczas spawnu.

### Testowanie

Sam host tych problemów nie ujawni. Przed publikacją sprawdź na dwóch komputerach ekran zarówno hosta, jak i uczestnika oraz przetestuj także przypadek, gdy uczestnik dołącza później, po zmianie wartości. Gdy włączony jest pakiet lokalny (`WorkshopLocal`), inne osoby nie mogą wejść do pokoju, więc testy wieloosobowe wykonuj, subskrybując element przesłany do Warsztatu ze statusem Unlisted. Pierwszy przesłany element automatycznie otrzymuje status Unlisted.


## Rozwiązywanie problemów

| Objaw | Co sprawdzić |
|---|---|
| Podsumowanie stanu mówi, że nie znaleziono klasy wejścia | Przestrzeń nazw lub nazwa klasy w `entryClass` nie pasuje do kodu. Klasy zagnieżdżone zapisuje się jako `Outer+Inner`. |
| Podsumowanie stanu pokazuje błąd typu klasy wejścia | Klasa wejścia nie dziedziczy po `WorkshopModBase` (ani `HorayModBase`), nie jest publiczna, jest abstrakcyjna albo nie ma publicznego konstruktora bezparametrowego. |
| Podsumowanie stanu mówi, że mod został wstrzymany z powodu konfliktu nazwy zestawu | Inny mod (łącznie z AddOns) używa tej samej `AssemblyName`. Zmień swoją. |
| Podsumowanie stanu pokazuje błąd ładowania biblioteki | Plik DLL wymieniony w `libraries` nie istnieje albo nie jest zestawem zarządzanym. |
| `FileNotFoundException` w logu | DLL, do którego się odwołujesz, nie znajduje się w folderze moda albo nie jest zadeklarowany w `libraries`. Albo DLL gry został skopiowany z `Private=true` i obie kopie się gryzą. |
| Wyjątek zaraz po wejściu do świata | Wyjątek w handlerze zdarzenia bazy danych. Poszukaj przestrzeni nazw swojego moda w śladzie stosu w logu. |
| Efekty kumulują się po powrocie do ekranu tytułowego i ponownym wejściu | Subskrybujesz ponownie wewnątrz handlera. Subskrybuj tylko raz, w `OnModLoaded`. |


## Przykład

[`examples/dll-sample`](../../examples/dll-sample) to minimalny mod, który zwiększa Obrażenia fizyczne o 5, gdy gracz się pojawia. Jego folder `package/` zawiera gotowy, skompilowany DLL, więc skopiowanie tego folderu do `WorkshopLocal/` sprawia, że działa od razu, bez kompilacji. Aby przerobić go na własnego moda, wystarczy zmienić `AssemblyName`, `modId`, `entryClass` i ścieżkę wyjściową. Rozpocznij grę i otwórz okno statystyk, aby zobaczyć, że działa jako Obrażenia fizyczne +5.
