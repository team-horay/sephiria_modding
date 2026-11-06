# DLL 모드

DLL 모듈을 통해 C#으로 작성한 코드를 게임에서 구동할 수 있습니다. `HorayModAPI`를 구독해 플레이어, 아이템, UI 등에 개입하세요.
이 문서는 프로젝트를 만들고 첫 DLL이 게임 안에서 돌아가는 것까지를 다룹니다. 이벤트와 등록 함수의 전체 목록은 [API 레퍼런스](api/README.md)를 보세요.


## 준비물

- [.NET SDK](https://dotnet.microsoft.com/download) 8 이상
- C# 편집기


## 프로젝트 만들기

[`examples/dll-sample`](../../examples/dll-sample) 예제를 참조하세요. 문서는 이 예제를 기준으로 설명합니다.

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
    <!-- 게임 설치 폴더. 환경에 맞게 바꾸거나 dotnet build -p:GameDir=... 로 넘깁니다. -->
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
    <!-- PlayerAvatar 같은 네트워크 오브젝트를 다루면 Mirror, UI를 만들면 UnityEngine.UI, 패치하면 0Harmony를 같은 방식으로 추가 -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **타깃은 `netstandard2.1`**, 언어 버전은 C# 9로 설정하세요.
2. **게임 DLL을 참조하되 복사하지 않습니다.** 참조는 `<게임 설치 폴더>/Sephiria_Data/Managed/`에 있는 파일을 가리키고, 전부 `Private=false`로 둡니다. 게임 DLL이 모드 폴더에 복사되면 같은 어셈블리가 두 벌 로드되어 알 수 없는 오류의 원인이 되고, 검수 대상 파일도 불필요하게 늘어납니다.
3. `Assembly-CSharp.dll`에 게임 구동 관련 대부분의 코드가 포함되어 있습니다.


## 코드 구조


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // 프로세스당 한 번 호출됩니다. 이벤트 구독은 여기서 한 번만 하세요.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] 로드됨 v{metadata.modVersion} | 폴더={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // 호스트(서버)에서만 호출됩니다. 게임 상태를 바꾸는 일은 여기서 합니다.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // 모든 참가자에서 호출됩니다. 서버가 더한 값이 동기화되어 들어옵니다.
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

베이스 클래스로부터 상속받는 내용은 다음과 같습니다.

| 멤버 | 내용 |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. `metadata.json`의 값이 들어옵니다. |
| `Context` | `PackageRoot`(모드 폴더 절대 경로), `FileId`(창작마당 번호, 로컬 패키지는 Steam ID와 겹치지 않는 별도 값), `Definition`(검증된 매니페스트), `ResolvePath(상대경로)`. 모드 폴더 안의 파일을 찾을 때 씁니다. 설치 경로는 사용자마다 다르니 절대 하드코딩하지 마세요. |
| `GetDataPackages()` | 이 모드를 `consumer`로 지정한 `data` 모듈 패키지 목록. 활성화가 끝난 뒤(`OnLoad*Database` 핸들러 안이나 `OnAllDatabasesReady` 이후)에 부르세요. |

`Context`는 `OnModLoaded`가 불릴 때부터 유효합니다. 생성자에서는 아직 null입니다.


## 생명주기

DLL 모드는 **게임을 시작해 월드에 입장할 때** 활성화됩니다. 타이틀 화면에서는 아직 코드가 실행되지 않습니다. 다음은 구동 순서입니다.

1. 월드 씬이 로드되고 `GameDataLoader`가 시작됩니다.
2. 로딩 계획의 DLL 모듈이 순서대로 로드되고, 진입 클래스가 인스턴스화되어 `OnModLoaded`가 불립니다.
3. 데이터베이스들이 초기화되면서 `OnLoadItemDatabase`, `OnLoadUnitDatabase` 같은 이벤트가 차례로 발생합니다.
4. 전부 끝나면 `OnAllDatabasesReady`를 호출합니다.

## 구독

`HorayModAPI` 클래스에는 여러 상황에 개입할 수 있는 사전 정의된 이벤트들이 존재합니다.

- 이벤트 구독은 `OnModLoaded`에서 **한 번만** 하세요. 데이터베이스 이벤트는 로드마다 다시 발생하므로, 핸들러 안에서 "매번 해야 하는 일"(아이템 등록 등)을 합니다.
- 구독과 구독 해제를 잘 관리하세요. `OnModUnloaded`에서 구독한 이벤트를 해제(-=) 해주세요.

전체 이벤트 목록은 [API 레퍼런스](api/README.md)에 있습니다. 어느 이벤트를 통해 하고 싶은 일을 작업해야 할 지 권장 이벤트 목록은 다음과 같습니다.

| 하고 싶은 일 | 이벤트 |
|---|---|
| 아이템·아티팩트 추가 | `OnLoadItemDatabase` |
| 무기·기적·패시브·코스튬·소품 추가 | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| 적·NPC 유닛 추가 | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| 여행자(마을 NPC) 추가 | `OnLoadSocialIDDatabase` |
| 스탯 표시 추가 | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| 번역 텍스트를 코드로 등록 | `OnLocalizationReady` (`AddText`) |
| 타이틀 메뉴에 버튼 | `OnTitleMenuReady` |
| 게임·세션 시작 | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| 층이 만들어질 때 | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| 플레이어·유닛 스폰 | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| 전투 판정 개입 | `OnPlayerEvade`, `OnGetDebuff` |
| 인벤토리 조작 권한 | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## 충돌 방지 규칙

모드가 추가하는 엔티티 이름은 `<modId>:<id>` 형식입니다. 예: `yourname.artifact-pack:flame-ring`. 이 규칙이 다른 모드와의 충돌을 막고, 전적·통계에서 모드 콘텐츠를 구분하는 기준이 됩니다.


## Server-side, Client-side

- 세피리아는 멀티플레이어를 기반으로 한 게임이므로, 여러 이벤트에 Server-side와 Client-side가 구분되어 있습니다.
- 호스트에서는 둘 다, 참가자에서는 Clientside만 발생합니다. 
- 주로 게임 상태를 바꾸는 일(스폰, 드롭, 피해)은 Serverside에서만 하고, 표시만 바꾸는 일은 Clientside에서 합니다.


## Harmony 패치

이벤트로 닿지 않는 곳을 바꿔야 할 때 Harmony를 사용할 수 있습니다.
1.1.0 패치 이후 게임에는 **Harmony 2.4.3이 내장**되어 있으니 참조를 추가하세요.

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

- **`0Harmony.dll`을 모드에 동봉하지 마세요.** 동봉한 파일은 무시되고, 다른 버전을 넣어도 그 버전으로 실행되지 않습니다.
- Harmony ID는 `modId`를 그대로 쓰면 겹치지 않습니다.
- 패치는 공식 API보다 게임 업데이트에 훨씬 쉽게 깨집니다. 이벤트로 할 수 있는 일은 이벤트로 하고, 패치는 마지막 수단으로 두세요.
- BepInEx, MelonLoader 등 외부 로더는 지원하지 않습니다. 외부 로더처럼 동작하는 모드(창작마당 밖의 플러그인 DLL을 찾아 실행해 주는 모드)는 경고가 붙으며, 사용자가 동의해야 켜집니다. 자세한 내용은 [창작마당 밖 코드를 실행하는 모드](#창작마당-밖-코드를-실행하는-모드)를 보세요.


## 보조 DLL

모드가 다른 라이브러리(예: Newtonsoft.Json이 아닌 서드파티 패키지)를 쓰면 그 DLL을 모드 폴더에 넣고 `metadata.json`의 `libraries`에 선언합니다. 선언하지 않은 DLL은 참조 해석에 쓰이지 않습니다.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- 게임에 이미 들어 있는 DLL(Unity, Mirror, Newtonsoft.Json, 0Harmony 등)은 넣지 마세요. 게임 쪽이 먼저 로드되어 모드가 넣은 사본은 무시되거나 충돌합니다.
- 네이티브(C/C++) DLL은 쓸 수 없습니다. 관리 어셈블리만 로드됩니다.
- 같은 어셈블리 이름이 다른 모드에서 이미 로드되어 있으면, `modules`의 DLL은 게임이 둘 중 하나를 고르지 않고 **실행을 보류**합니다. `libraries`의 DLL은 이미 로드된 쪽을 대신 쓰고 동봉본은 무시하므로, 버전이 다르면 오작동할 수 있습니다. 공용 라이브러리는 별도 모드로 분리하고 `dependencies`로 연결하는 것이 안전합니다.

### 다른 모드의 라이브러리 쓰기

여러 모드가 같은 코드를 쓴다면, 그 코드를 상위 모드 하나에 넣고 하위 모드가 `dependencies`로 연결하세요.

1. 상위 모드는 공용 코드를 `modules` 또는 `libraries`에 선언하고, `tags`에 `Library`를 넣습니다.
2. 하위 모드는 `dependencies`에 상위 모드의 `modId`를 적고, 상위 모드의 DLL을 `Private=false`로 참조해 빌드합니다. **상위 모드의 DLL을 하위 모드에 복사해 넣지 마세요.**
3. 게임은 상위 모드를 먼저 불러오므로, 하위 모드의 참조는 이미 로드된 상위 모드의 어셈블리로 해석됩니다.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- 하위 모드도 자기 `dll` 모듈과 `entryClass`를 가져야 합니다. 상위 모드가 하위 모드의 DLL을 직접 찾아 불러오는 구조는 지원하지 않습니다. 하위 모드가 자기 `OnModLoaded`에서 상위 모드의 등록 함수를 부르는 방식으로 만드세요.
- 상위 모드가 업데이트로 공개 형식이나 멤버를 바꾸면 하위 모드가 깨질 수 있습니다. 공용 라이브러리는 기존 공개 API를 지우거나 바꾸지 않도록 주의하세요.

### 실행 중에 DLL을 생성하지 마세요!

모드가 실행하는 코드는 모두 검수를 거친 창작마당 아이템 안에 있어야 합니다. dll 파일의 명세가 모드 실행 도중 변경되면 해당 모드 로드는 제한됩니다. 모든 dll은 창작마당에 어떠한 압축 / 스트리밍 없이 포함시키세요. 즉, 아래의 동작은 검수 시스템에 저촉될 수 있습니다. 필요한 dll은 업로드할 때 전부 넣어두세요.

- 모드가 실행 도중 외부에서 dll을 다운로드 받아 assembly load 실행
- 모드가 실행 도중 자기만의 dll을 생성 및 실행 (Mono.Cecil 등으로 다른 DLL을 고쳐 쓴 뒤 실행하는 것도 포함)
- 본인 모드 안에 본인만의 압축 방식으로 넣어둔 dll을 압축 해제하여 사용

### 창작마당 밖 코드를 실행하는 모드

게임 설치 폴더, `AppData` 등 **모드 폴더 밖**에 사용자가 직접 넣은 DLL을 찾아 실행해 주는 모드(예: 다른 로더용 플러그인 호환 모드)는 반려하지 않지만, 검수 시스템이 **경고**를 붙입니다.

- 사용자가 그 모드를 켤 때 경고 창이 뜨고, 동의해야만 켜집니다. 모드가 업데이트되면 다시 묻습니다.
- 그렇게 실행되는 외부 DLL은 검수를 거치지 않으며, 그로 인해 생긴 문제는 지원하지 않습니다.
- 모드가 그런 DLL을 실행 중에 직접 내려받는 것은 위 금지 항목에 해당합니다.

다른 모드의 코드를 쓰려면 그 모드의 폴더에서 DLL을 직접 불러오지 말고, [다른 모드의 라이브러리 쓰기](#다른-모드의-라이브러리-쓰기)처럼 `dependencies`로 연결하세요. 게임이 직접 불러오지 않은 DLL은 로드 순서·이름 충돌·의존성 검사를 거치지 않으므로 지원하지 않습니다.

DLL을 고쳐 써야 한다면(예: 다른 Harmony API에 맞춘 IL 변환) 업로드하기 전에 미리 변환하고, 변환된 DLL을 모드에 넣어 올리세요.


## 빌드하고 확인하기

```bash
dotnet build -c Release
```

- 출력이 성공하여 `WorkshopLocal/<모드>/bin/`에 dll 파일이 생성되면 게임을 실행하고, 타이틀 창작마당 패널에서 모드가 "내가 만든 모드"에 보이는지 확인한 뒤 게임을 시작합니다.
- 빌드 결과에서 `.pdb`를 함께 두면 로그의 스택 트레이스에 줄 번호가 나옵니다. 게시할 때는 빼고 업로드하세요.


## 게임 업데이트와 호환

게임이 업데이트되면 `Assembly-CSharp.dll`이 변경됩니다. 공식 이벤트만 쓴 모드는 대체로 그대로 돌지만, 내부 클래스를 직접 건드리거나 Harmony로 패치한 모드는 깨질 수 있습니다.

- 업데이트 뒤에는 새 `Managed/` 폴더를 참조해 다시 빌드하고 테스트하세요.
- 창작마당 페이지의 설명에 **지원하는 게임 버전**을 적어 두면 사용자가 원인을 빨리 압니다. 게시할 때 게임 버전이 창작마당 항목 메타데이터에 자동으로 기록되기도 합니다.
- 코드 변경 시 다시 검수 시스템에 접수해야 합니다. [창작마당에 올리기](publishing.md#코드-검수) 참고.


## 멀티플레이에서 주의할 것

- 코드 모드는 대부분 `multiplayer.installation`이 `everyone`일 것입니다.
- 게임 상태를 `Serverside` 이벤트에서만 바꾸고, 바꾼 값이 이미 동기화되는 게임 상태(예: `customStats`)라서 참가자 쪽에 코드나 등록이 필요 없는 모드라면 `host_only`로 선언할 수 있습니다. 그러면 참가자는 모드를 다운로드 받지 않습니다. 새 아이템을 등록하거나 `RegisterNetworkPrefab`을 쓰거나 `Clientside` 이벤트에서 무언가를 바꾸는 모드는 `host_only`가 될 수 없습니다.
- 네트워크로 스폰되는 오브젝트(유닛, 발사체 등)는 `RegisterNetworkPrefab`으로 등록해야 참가자 쪽에서도 생성됩니다. `stableKey`를 주면 모드 버전이 바뀌어도 같은 ID를 유지합니다.
- 호스트와 참가자의 모드 **로드 순서**까지 같아야 합니다. 순서에 따라 결과가 달라지는 코드는 쓰지 않는 것이 좋습니다.


## 네트워크 동기화 (SyncVar, RPC, Command)

세피리아의 멀티플레이 기능은 오픈 소스 네트워크 라이브러리 'Mirror'로 구현되었습니다. Mirror에서는 다음 애트리뷰트를 선언하여 통신 기능을 쉽게 이용할 수 있습니다.

- `[SyncVar]`: 클래스 멤버 변수에 붙이는 애트리뷰트입니다. 서버와 클라이언트 간 변수 값을 동기화합니다.
- `[ClientRpc]`, `[TargetRpc]`: 함수에 붙이는 애트리뷰트입니다. 서버에서 클라이언트에게 함수를 실행하도록 요청합니다.
- `[Command]`: 함수에 붙이는 애트리뷰트입니다. 클라이언트에서 서버에게 함수를 실행하도록 요청합니다.

모드 환경에서는 위 애트리뷰트를 정상적으로 사용할 수 없습니다. 코드가 컴파일될 때 Mirror의 Weaver가 각 애트리뷰트에 대응하는 전송 코드를 넣어줘야 동작하는데, 외부에서 생성한 DLL은 Weaver를 거치지 않기 때문에 빌드가 성공적으로 되더라도 위 애트리뷰트는 아무런 기능을 하지 않습니다. 이러한 버그는 호스트 화면에서는 정상으로 보이고 참가자 화면에서만 문제가 생기므로, 혼자 테스트해서는 발견하기 어렵습니다. (만약 Weaver를 강제로 거쳤다 하더라도, 함수 해시 충돌 등의 버그가 있을 수 있습니다.) 


### `SyncVar<T>`, `ModRpc`

이를 해결하기 위해, 세피리아에서는 위 애트리뷰트를 대체할 모드용 통신 코드를 제공합니다. `SyncVar<T>`와 `ModRpc`입니다.

| 하려는 일 | 사용할 것 |
|---|---|
| 값을 계속 맞춰 두기 (늦게 들어온 참가자 포함) | `SyncVar<T>` |
| 서버 → 전체 클라이언트 호출 | `ModRpc.Send` |
| 서버 → 특정 클라이언트 호출 | `ModRpc.SendTo` |
| 클라이언트 → 서버 호출 | `ModRpc.SendCommand` |
| 게임이 이미 동기화하는 값 바꾸기 | `Network필드명` 속성 |

- 이 기능은 게임 버전 1.1.2 부터 사용 가능합니다. 
- 이 기능을 이용하는 모드는 `multiplayer.installation`을 `everyone`으로 선언하세요.


### 컴포넌트 준비

`SyncVar<T>`와 `ModRpc`는 `NetworkBehaviour`를 상속한 컴포넌트에서만 사용 가능합니다. 이 컴포넌트는 호스트와 참가자 양쪽에서 같은 오브젝트에 **같은 순서로** 붙어 있어야 합니다. 이미 스폰된 오브젝트에 `AddComponent`로 붙이면 동기화 대상에 들어가지 않으니, 네트워크 프리팹으로 등록하기 전에 이미 붙어 있어야 합니다.

```csharp
private static GameObject prefab;

// OnModLoaded에서 HorayModAPI.OnAllDatabasesReady에 구독합니다.
private static void RegisterPrefab()
{
    if (prefab) return;

    // 비활성 부모 아래에 둡니다. 프리팹의 Awake는 돌지 않고, Instantiate한 복제본은 활성으로 시작합니다.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// 서버에서 스폰합니다. 두 번째 인자를 주면 그 플레이어가 소유자가 됩니다.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: 값 동기화

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Weaver가 없으므로 생성자에서 직접 등록합니다. 필드 순서는 항상 같아야 합니다.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- 값은 **서버에서** `stack.Value = 3`처럼 바꿉니다. 참가자가 바꾼 값은 서버로 전달되지 않습니다.
- 늦게 들어온 참가자도 입장할 때 현재 값을 받습니다.
- `Callback`은 호스트에서는 값을 넣을 때, 참가자에서는 값을 받을 때 호출됩니다. 입장할 때 받은 값이 기본값과 다르면 그때도 호출됩니다. 콜백에서 난 예외는 로그에 남고 동기화는 계속됩니다.
- `int`, `float`, `string`, `Vector3` 같은 기본 자료형을 쓸 수 있습니다. 모드가 만든 struct는 직렬화 코드가 자동으로 만들어지지 않으므로 필드를 나누어 쓰세요.
- 다른 오브젝트를 가리킬 때는 `SyncVar<GameObject>` 대신 `netId`를 `SyncVar<uint>`로 보내세요. 받는 쪽에 그 오브젝트가 아직 생성되지 않았을 수 있습니다.
- 한 컴포넌트에 등록할 수 있는 수는 상속받은 것까지 합쳐 64개입니다.

### `ModRpc`: 함수 호출

모드를 로드할 때 함수를 한 번 등록합니다. 호출할 때의 이름은 `모드ID:함수명`입니다.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// 서버 → 이 오브젝트가 보이는 모든 클라이언트
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 서버 → 한 클라이언트. 연결에 null을 주면 오브젝트 소유자에게 보냅니다.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 클라이언트 → 서버
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- 쓴 순서대로 읽으세요. 쓰기와 읽기의 순서·자료형이 다르면 값이 틀어지거나 예외가 납니다.
- `RegisterCommand`는 기본적으로 **오브젝트 소유자만** 호출할 수 있습니다. 소유자가 없는 오브젝트에서 받으려면 `requiresAuthority: false`를 입력하세요. 이 경우 어느 참가자든 호출할 수 있습니다. 치트 등에 유의하세요.
- 등록한 이름과 다른 방향으로 보내거나, 등록하지 않은 이름을 보내면 오류 로그만 남고 전송되지 않습니다.
- 받는 쪽에 함수가 없거나 컴포넌트 타입이 다르면 경고 로그만 남기고 무시합니다. 핸들러에서 난 예외도 로그에 남고 게임은 계속 진행됩니다.
- 기본 채널은 신뢰 전송입니다. 자주 보내는 연출이면 `channelId: Channels.Unreliable` 인자를 줄 수 있습니다.

### 꼭 지켜야 할 것

- 클라이언트는 Command를 통해 임의의 값을 보낼 수 있습니다. 슬롯 번호, 수량, 대상 등을 서버 핸들러에서 검증하세요.
- 늦게 들어온 참가자는 입장 전의 RPC를 받지 못합니다. 유지해야 하는 상태는 `SyncVar<T>`로 보내고, RPC는 소리·이펙트처럼 지나가는 일에 쓰세요.
- **RPC가 SyncVar보다 먼저 도착할 수 있습니다.** RPC 핸들러에서 `stack.Value`를 읽으면 이전 값일 수 있습니다. 새 값이 필요하면 RPC 인자로 함께 보내거나 `Callback`에서 처리하세요.

### 세피리아 본편 코드의 수치 변경

게임 클래스의 `[SyncVar]` 필드에 직접 대입하면 참가자에게 전달되지 않습니다. 앞에 `Network`를 붙인 속성을 쓰세요. 대소문자는 필드 이름 그대로입니다.

```csharp
unit.maxHp = 500;         // 호스트 값만 바뀝니다.
unit.NetworkmaxHp = 500;  // 참가자에게도 전달됩니다.
```

오브젝트가 스폰되기 전(프리팹이나 `Instantiate` 직후)에는 직접 대입해도 스폰할 때 값이 함께 전달됩니다.

### 테스트

호스트 혼자서는 위 문제가 드러나지 않습니다. 게시 전에 두 PC로 호스트와 참가자 양쪽 화면을 확인하고, 값을 바꾼 뒤 늦게 입장하는 경우까지 시험해 보세요. 로컬 패키지(`WorkshopLocal`)를 켜면 다른 사람이 방에 들어올 수 없으므로, 멀티 테스트는 창작마당에 Unlisted로 올린 항목을 구독해서 합니다. 처음 올린 항목은 자동으로 Unlisted입니다.


## 문제해결

| 증상 | 확인할 것 |
|---|---|
| 상태 요약에 진입 클래스를 찾지 못했다고 나옴 | `entryClass`의 네임스페이스·클래스 이름이 코드와 다름. 중첩 클래스는 `Outer+Inner`. |
| 상태 요약에 진입 클래스 형식 오류가 나옴 | 진입 클래스가 `WorkshopModBase`(또는 `HorayModBase`)를 상속하지 않았거나, public이 아니거나, abstract이거나, 매개변수 없는 public 생성자가 없음. |
| 상태 요약에 어셈블리 이름 충돌로 보류됐다고 나옴 | 다른 모드(AddOns 포함)가 같은 `AssemblyName`을 씀. 이름을 바꾸세요. |
| 상태 요약에 라이브러리 적재 실패가 나옴 | `libraries`에 적은 DLL이 없거나 관리 어셈블리가 아님. |
| 로그에 `FileNotFoundException` | 참조한 DLL이 모드 폴더에 없거나 `libraries`에 선언되지 않음. 또는 게임 DLL을 `Private=true`로 복사해 둘이 충돌. |
| 월드에 들어가면 바로 예외 | 데이터베이스 이벤트 핸들러의 예외. 로그의 스택 트레이스에서 모드 네임스페이스를 찾으세요. |
| 타이틀로 나갔다 다시 들어온 뒤부터 효과가 중복됨 | 핸들러 안에서 다시 구독하고 있음. 구독은 `OnModLoaded`에서 한 번만. |


## 샘플

[`examples/dll-sample`](../../examples/dll-sample)은 플레이어가 스폰될 때 물리 피해를 5 올리는 최소 모드입니다. `package/` 폴더에 빌드된 DLL까지 들어 있어서, 그 폴더를 `WorkshopLocal/`에 복사하면 빌드 없이 바로 동작합니다. 자기 모드로 만들 때는 `AssemblyName`, `modId`, `entryClass`, 출력 경로만 바꾸면 됩니다. 게임을 시작해 스탯 창을 열면 물리 피해 +5로 동작을 확인할 수 있습니다.
