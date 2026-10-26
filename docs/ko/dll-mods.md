# DLL 모드

DLL 모듈을 통해 C#으로 작성한 코드를 게임에서 구동할 수 있습니다. `HorayModAPI`를 구독해 플레이어, 아이템, UI 등에 개입하세요.
이 문서는 프로젝트를 만들고 첫 DLL이 게임 안에서 돌아가는 것까지를 다룹니다. 이벤트와 등록 함수의 전체 목록은 [API 레퍼런스](api/README.md)를 보세요.


## 준비물

- [.NET SDK](https://dotnet.microsoft.com/download) 8 이상
- C# 편집기


## 프로젝트 만들기

[`template/`](../../template)의 예제를 참조하세요. 문서는 이 템플릿을 기준으로 설명합니다.

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
| `Context` | `PackageRoot`(모드 폴더 절대 경로), `FileId`(창작마당 번호, 로컬이면 0), `Definition`(검증된 매니페스트), `ResolvePath(상대경로)`. 모드 폴더 안의 파일을 찾을 때 씁니다. 설치 경로는 사용자마다 다르니 절대 하드코딩하지 마세요. |
| `GetDataPackages()` | 이 모드를 `consumer`로 지정한 `data` 모듈 패키지 목록. 활성화가 끝난 뒤(`OnAllDatabasesReady` 이후)에 부르세요. |

`Context`는 `OnModLoaded`가 불릴 때부터 유효합니다. 생성자에서는 아직 null입니다.


## 생명주기

DLL 모드는 **게임을 시작해 월드에 입장할 때** 활성화됩니다. 타이틀 화면에서는 아직 코드가 실행되지 않습니다. 다음은 구동 순서입니다.

1. 월드 씬이 로드되고 `GameDataLoader`이 게임의 주요 데이터들을 로드합니다.
2. 로딩 계획의 DLL 모듈이 순서대로 로드되고, 진입 클래스가 인스턴스화되어 `OnModLoaded`가 불립니다.
3. 데이터베이스들이 초기화되면서 `OnLoadItemDatabase`, `OnLoadUnitDatabase` 같은 이벤트가 차례로 발생합니다.
4. 전부 끝나면 `OnAllDatabasesReady`를 호출합니다.

## 구독

`HorayModAPI` 클래스에는 여러 상황에 개입할 수 있는 사전 정의된 이벤트들이 존재합니다.

- 이벤트 구독은 `OnModLoaded`에서 **한 번만** 하세요. 데이터베이스 이벤트는 로드마다 다시 발생하므로, 핸들러 안에서 "매번 해야 하는 일"(아이템 등록 등)을 합니다.
- 구독과 구독 해제를 잘 관리하세요. `OnModUnloaded`에서 구독한 이벤트를 해제(-=) 해주세요. 현재는 켜진 모드를 끄려면 재시작이 필요하므로 실제로는 제한적으로 호출됩니다만, 그래도 구독 해제를 짝 맞춰 두세요.

전체 이벤트 목록은  [API 레퍼런스](api/README.md)에 있습니다. 어느 이벤트를 통해 하고 싶은 일을 작업해야 할 지 권장 이벤트 목록은 다음과 같습니다.

| 하고 싶은 일 | 이벤트 |
|---|---|
| 아이템·아티팩트 추가 | `OnLoadItemDatabase` |
| 무기·기적·패시브·코스튬·소품 추가 | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| 적·NPC 유닛 추가 | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| 여행자(마을 NPC) 추가 | `OnLoadSocialIDDatabase` |
| 스탯 표시 추가 | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| 번역 텍스트를 코드로 등록 | `OnLocalizationReady` (`AddText`) |
| 타이틀 메뉴에 버튼 | `OnTitleMenuReady` |
| 세션 시작·종료 | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
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
- BepInEx, MelonLoader 등 외부 로더는 지원하지 않습니다.


## 보조 DLL

모드가 다른 라이브러리(예: Newtonsoft.Json이 아닌 서드파티 패키지)를 쓰면 그 DLL을 모드 폴더에 넣고 `metadata.json`의 `libraries`에 선언합니다. 선언하지 않은 DLL은 참조 해석에 쓰이지 않습니다.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- 게임에 이미 들어 있는 DLL(Unity, Mirror, Newtonsoft.Json, 0Harmony 등)은 넣지 마세요. 게임 쪽이 먼저 로드되어 모드가 넣은 사본은 무시되거나 충돌합니다.
- 네이티브(C/C++) DLL은 쓸 수 없습니다. 관리 어셈블리만 로드됩니다.
- 같은 어셈블리 이름이 다른 모드에서 이미 로드되어 있으면 게임은 둘 중 하나를 고르지 않고 **실행을 보류**합니다. 공용 라이브러리는 별도 모드로 분리하고 `dependencies`로 연결하는 것이 안전합니다.


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
- 코드를 바꾸면 검수를 다시 받아야 게시할 수 있습니다. [게시와 멀티플레이](publishing.md) 참고.


## 멀티플레이에서 주의할 것

- 코드 모드는 대부분 `multiplayer.installation`이 `everyone`일 것입니다.
- 게임 상태를 `Serverside` 이벤트에서만 바꾸고, 바꾼 값이 이미 동기화되는 게임 상태(예: `customStats`)라서 참가자 쪽에 코드나 등록이 0 필요 없는 모드라면 `host_only`로 선언할 수 있습니다. 그러면 참가자는 모드를 다운로드 받지 않습니다. 새 아이템을 등록하거나 `RegisterNetworkPrefab`을 쓰거나 `Clientside` 이벤트에서 무언가를 바꾸는 모드는 `host_only`가 될 수 없습니다.
- 네트워크로 스폰되는 오브젝트(유닛, 발사체 등)는 `RegisterNetworkPrefab`으로 등록해야 참가자 쪽에서도 생성됩니다. `stableKey`를 주면 모드 버전이 바뀌어도 같은 ID를 유지합니다.
- 호스트와 참가자의 모드 **로드 순서**까지 같아야 합니다. 순서에 따라 결과가 달라지는 코드는 쓰지 않는 것이 좋습니다.


## 문제해결

| 증상 | 확인할 것 |
|---|---|
| 상태 요약에 진입 클래스를 찾지 못했다고 나옴 | `entryClass`의 네임스페이스·클래스 이름이 코드와 다름. 중첩 클래스는 `Outer+Inner`. |
| 상태 요약에 진입 클래스 형식 오류가 나옴 | 진입 클래스가 `WorkshopModBase`(또는 `HorayModBase`)를 상속하지 않았거나, public이 아니거나, abstract이거나, 매개변수 없는 public 생성자가 없음. |
| 상태 요약에 어셈블리 이름 충돌로 보류됐다고 나옴 | 다른 모드(AddOns 포함)가 같은 `AssemblyName`을 씀. 이름을 바꾸세요. |
| 상태 요약에 라이브러리 적재 실패가 나옴 | `libraries`에 적은 DLL이 없거나 관리 어셈블리가 아님. |
| 로그에 `FileNotFoundException` | 참조한 DLL이 모드 폴더에 없거나 `libraries`에 선언되지 않음. 또는 게임 DLL을 `Private=true`로 복사해 둘이 충돌. |
| 월드에 들어가면 바로 예외 | 데이터베이스 이벤트 핸들러의 예외. 로그의 스택 트레이스에서 모드 네임스페이스를 찾으세요. |
| 두 번째 런부터 효과가 중복됨 | 핸들러 안에서 다시 구독하고 있음. 구독은 `OnModLoaded`에서 한 번만. |


## 샘플

[`template/`](../../template)은 플레이어가 스폰될 때 물리 피해를 5 올리는 최소 모드입니다. `package/` 폴더에 빌드된 DLL까지 들어 있어서, 그 폴더를 `WorkshopLocal/`에 복사하면 빌드 없이 바로 동작합니다. 자기 모드로 만들 때는 `AssemblyName`, `modId`, `entryClass`, 출력 경로만 바꾸면 됩니다. 게임을 시작해 스탯 창을 열면 물리 피해 +5로 동작을 확인할 수 있습니다.
