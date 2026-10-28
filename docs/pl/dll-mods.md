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
- Zewnętrzne loadery, takie jak BepInEx i MelonLoader, nie są obsługiwane.


## Pomocnicze pliki DLL

Jeśli twój mod korzysta z innych bibliotek (np. pakietów zewnętrznych innych niż Newtonsoft.Json), umieść ich pliki DLL w folderze moda i zadeklaruj je w `libraries` w `metadata.json`. Niezadeklarowane pliki DLL nie są używane do rozwiązywania odwołań.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Nie dołączaj plików DLL, które są już dostarczane z grą (Unity, Mirror, Newtonsoft.Json, 0Harmony itp.). Kopia gry ładuje się jako pierwsza, więc kopia dołączona do moda jest ignorowana albo powoduje konflikt.
- Natywnych (C/C++) plików DLL nie można używać. Ładowane są tylko zestawy zarządzane.
- Jeśli zestaw o tej samej nazwie został już załadowany przez innego moda, gra nie wybiera jednego z dwóch, lecz **wstrzymuje uruchomienie**. Bezpieczniej jest wydzielić współdzielone biblioteki do osobnego moda i powiązać go przez `dependencies`.


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
- Jeśli zmienisz kod, musi on ponownie przejść weryfikację, zanim będzie można go opublikować. Zobacz [Publikowanie w Warsztacie](publishing.md).


## Uwagi dotyczące trybu wieloosobowego

- Większość modów z kodem będzie miała `multiplayer.installation` ustawione na `everyone`.
- Jeśli twój mod zmienia stan gry wyłącznie w zdarzeniach `Serverside`, a zmieniane wartości to stan gry, który już jest synchronizowany (np. `customStats`), więc uczestnicy nie potrzebują żadnego kodu ani rejestracji, możesz zadeklarować go jako `host_only`. Uczestnicy nie pobierają wtedy moda. Mod, który rejestruje nowe przedmioty, używa `RegisterNetworkPrefab` lub zmienia cokolwiek w zdarzeniach `Clientside`, nie może być `host_only`.
- Obiekty spawnowane przez sieć (jednostki, pociski itp.) muszą być zarejestrowane przez `RegisterNetworkPrefab`, aby były tworzone także po stronie uczestników. Jeśli przekażesz `stableKey`, to samo ID zostaje zachowane nawet po zmianie wersji moda.
- Host i uczestnicy muszą mieć nawet tę samą **kolejność ładowania** modów. Unikaj kodu, którego wyniki zależą od tej kolejności.


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
