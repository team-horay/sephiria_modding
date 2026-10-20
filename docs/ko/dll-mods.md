# DLL 모드

C#으로 작성한 코드를 게임에 붙이는 모드입니다. 게임이 공개하는 이벤트(`HorayModAPI`)를 구독해 아이템·유닛·NPC·UI 등에 개입하고, 필요하면 Harmony로 게임 함수를 패치합니다.

이 문서는 프로젝트를 만들고 첫 DLL이 게임 안에서 돌아가는 것까지를 다룹니다. 이벤트와 등록 함수의 전체 목록은 [API 레퍼런스](https://teamhoray.com/mod-api)를 보세요.


## 준비물

- [.NET SDK](https://dotnet.microsoft.com/download) 8 이상. `dotnet --version`으로 확인합니다.
- C# 편집기. Visual Studio, Rider, VS Code(C# Dev Kit) 중 아무거나.
- 게임 설치 폴더의 경로. [첫 모드 만들기](getting-started.md)에서 찾는 법을 설명합니다.

DLL 모드는 **Mono 빌드에서만** 동작합니다. 현재 Steam PC 빌드(Windows·macOS·Steam Deck)는 Mono입니다.


## 프로젝트 만들기

[`template/`](../../template)에 바로 복사해서 쓰는 프로젝트가 있습니다. 직접 만들 때 핵심은 다음 세 가지입니다.

1. **타깃은 `netstandard2.1`**, 언어 버전은 C# 9. 게임이 Unity 6의 .NET Standard 2.1 프로필이라 그 위로는 올리지 마세요.
2. **게임 DLL을 참조하되 복사하지 않습니다.** 참조는 `<게임 설치 폴더>/Sephiria_Data/Managed/`에 있는 파일을 가리키고, 전부 `Private=false`로 둡니다. 게임 DLL이 모드 폴더에 복사되면 같은 어셈블리가 두 벌 로드되어 알 수 없는 오류의 원인이 되고, 검수 대상 파일도 불필요하게 늘어납니다.
3. **출력은 모드 폴더의 `bin/`으로.** 빌드 한 번에 `WorkshopLocal/<모드>/bin/<이름>.dll`이 나오게 하면 게임을 실행하는 것만으로 테스트가 됩니다.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>MyFirstMod</AssemblyName>
    <RootNamespace>MyFirstMod</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- 게임 설치 폴더. 환경에 맞게 바꾸거나 dotnet build -p:GameDir=... 로 넘깁니다. -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\my-first-mod\bin\</OutDir>
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
    <!-- UI를 만들면 UnityEngine.UI, 네트워크 오브젝트를 다루면 Mirror, 패치하면 0Harmony를 같은 방식으로 추가 -->
  </ItemGroup>
</Project>
```

`Assembly-CSharp.dll`이 게임 코드 본체입니다. 어떤 클래스와 함수가 있는지는 ILSpy나 dnSpy 같은 디컴파일러로 열어 보면 됩니다. 모드 제작 목적의 디컴파일은 허용됩니다([규칙 8번](../../README.md#규칙)).


## 진입점

`metadata.json`의 `entryClass`가 가리키는 클래스가 진입점입니다. `WorkshopModBase`를 상속하고, public이며, 매개변수 없는 public 생성자가 있어야 합니다. 게임은 DLL 안을 뒤져서 찾지 않으므로 이름을 정확히 적어야 합니다.

```csharp
using UnityEngine;

namespace MyFirstMod
{
    public class Entry : WorkshopModBase
    {
        protected override void OnModLoaded()
        {
            Debug.Log($"[MyFirstMod] 로드됨 v{metadata.modVersion} | 폴더={Context.PackageRoot}");

            HorayModAPI.OnLoadItemDatabase += HandleItemDatabase;
            HorayModAPI.OnAllDatabasesReady += HandleAllReady;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnLoadItemDatabase -= HandleItemDatabase;
            HorayModAPI.OnAllDatabasesReady -= HandleAllReady;
        }

        private void HandleItemDatabase()
        {
            // 여기서 아이템을 등록합니다.
        }

        private void HandleAllReady()
        {
            Debug.Log("[MyFirstMod] 모든 데이터베이스 준비 완료");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/MyFirstMod.dll", "entryClass": "MyFirstMod.Entry" }
]
```

베이스 클래스가 주는 것은 셋입니다.

| 멤버 | 내용 |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. `metadata.json`의 값이 들어옵니다. |
| `Context` | `PackageRoot`(모드 폴더 절대 경로), `FileId`(창작마당 번호, 로컬이면 0), `Definition`(검증된 매니페스트), `ResolvePath(상대경로)`. 모드 폴더 안의 파일을 찾을 때 씁니다. 설치 경로는 사용자마다 다르니 절대 하드코딩하지 마세요. |
| `GetDataPackages()` | 이 모드를 `consumer`로 지정한 `data` 모듈 패키지 목록. 활성화가 끝난 뒤(`OnAllDatabasesReady` 이후)에 부르세요. |

`Context`는 `OnModLoaded`가 불릴 때부터 유효합니다. 생성자에서는 아직 null입니다.


## 생명주기

DLL 모드는 **게임을 시작해 월드에 들어갈 때** 활성화됩니다. 타이틀 화면에서는 아직 코드가 실행되지 않습니다. 순서는 이렇습니다.

1. 월드 씬이 로드되고 `GameDataLoader`가 깨어납니다.
2. 로딩 계획의 DLL 모듈이 순서대로 로드되고, 진입 클래스가 인스턴스화되어 `OnModLoaded`가 불립니다.
3. 데이터베이스들이 초기화되면서 `OnLoadItemDatabase`, `OnLoadUnitDatabase` 같은 이벤트가 차례로 발생합니다.
4. 전부 끝나면 `OnAllDatabasesReady`.

여기서 꼭 알아야 할 것이 하나 있습니다. **월드 씬은 런을 다시 시작할 때마다 다시 로드되지만, 모드 인스턴스는 프로세스당 한 번만 만들어집니다.** 두 번째 로드부터 게임은 이미 활성화된 모드를 건너뛰고 데이터베이스만 다시 초기화합니다. 그래서

- 이벤트 구독은 `OnModLoaded`에서 **한 번만** 합니다. 데이터베이스 이벤트는 로드마다 다시 발생하므로, 핸들러 안에서 "매번 해야 하는 일"(아이템 등록 등)을 합니다.
- 핸들러 안에서 다시 구독하지 마세요. 로드마다 핸들러가 한 겹씩 쌓입니다.
- `OnModUnloaded`는 모드가 꺼질 때를 위한 자리이지만, 켜진 모드를 끄려면 재시작이 필요하므로 실제로는 거의 불리지 않습니다. 그래도 구독 해제를 짝 맞춰 두세요.

타이틀 화면에 개입하려면 `OnTitleMenuReady`를 구독합니다. 타이틀 씬이 만들어질 때마다 불리고, 돌려받은 버튼은 그 씬의 것이라 다른 씬까지 들고 가면 안 됩니다. 단, 모드가 월드 진입 때 활성화되므로 이 이벤트는 **한 번 월드에 들어갔다가 타이틀로 돌아온 뒤**부터 받습니다.


## 자주 쓰는 이벤트

전체 목록과 시그니처는 [API 레퍼런스](https://teamhoray.com/mod-api)에 있습니다. 여기서는 어떤 일에 어떤 이벤트를 쓰는지만 적습니다.

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

모드가 추가하는 엔티티 이름은 `<modId>:<id>` 형식입니다. 예: `yourname.artifact-pack:flame-ring`. 이 규칙이 다른 모드와의 충돌을 막고, 전적·통계에서 모드 콘텐츠를 구분하는 기준이 됩니다.

`Serverside`와 `Clientside`가 나뉜 이벤트는 멀티플레이 때문입니다. 호스트에서는 둘 다, 참가자에서는 Clientside만 발생합니다. 게임 상태를 바꾸는 일(스폰, 드롭, 피해)은 Serverside에서만 하고, 표시만 바꾸는 일은 Clientside에서 합니다.


## Harmony 패치

이벤트로 닿지 않는 곳을 바꿔야 할 때 Harmony를 씁니다. 게임에 **Harmony 2.4.3이 내장**되어 있으니 참조만 추가하면 됩니다.

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

- **`0Harmony.dll`을 모드에 동봉하지 마세요.** 게임이 자기 것을 먼저 올리기 때문에 동봉한 파일은 무시되고, 다른 버전을 넣어도 그 버전으로 실행되지 않습니다. 동작이 "왜 안 바뀌는지" 헤매는 원인 1순위입니다.
- Harmony ID는 `modId`를 그대로 쓰면 겹치지 않습니다.
- 패치는 공식 API보다 게임 업데이트에 훨씬 쉽게 깨집니다. 이벤트로 할 수 있는 일은 이벤트로 하고, 패치는 마지막 수단으로 두세요.
- BepInEx, MelonLoader 같은 외부 로더는 지원하지 않습니다. 창작마당 검수와 멀티플레이 모드 집합 밖에서 동작하므로, 그 환경에서 생긴 문제는 지원 대상이 아닙니다.


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

출력이 `WorkshopLocal/<모드>/bin/`에 떨어지면 게임을 실행하고, 타이틀 창작마당 패널에서 모드가 "내가 만든 모드"에 보이는지 확인한 뒤 게임을 시작합니다. `Player.log`에서 `OnModLoaded`의 로그가 찍히면 성공입니다.

| 증상 | 확인할 것 |
|---|---|
| 상태 요약에 진입 클래스를 찾지 못했다고 나옴 | `entryClass`의 네임스페이스·클래스 이름이 코드와 다름. 중첩 클래스는 `Outer+Inner`. |
| 상태 요약에 진입 클래스 형식 오류가 나옴 | 진입 클래스가 `WorkshopModBase`(또는 `HorayModBase`)를 상속하지 않았거나, public이 아니거나, abstract이거나, 매개변수 없는 public 생성자가 없음. |
| 상태 요약에 어셈블리 이름 충돌로 보류됐다고 나옴 | 다른 모드(AddOns 포함)가 같은 `AssemblyName`을 씀. 이름을 바꾸세요. |
| 상태 요약에 라이브러리 적재 실패가 나옴 | `libraries`에 적은 DLL이 없거나 관리 어셈블리가 아님. |
| 로그에 `FileNotFoundException` | 참조한 DLL이 모드 폴더에 없거나 `libraries`에 선언되지 않음. 또는 게임 DLL을 `Private=true`로 복사해 둘이 충돌. |
| 월드에 들어가면 바로 예외 | 데이터베이스 이벤트 핸들러의 예외. 로그의 스택 트레이스에서 모드 네임스페이스를 찾으세요. |
| 두 번째 런부터 효과가 중복됨 | 핸들러 안에서 다시 구독하고 있음. 구독은 `OnModLoaded`에서 한 번만. |

빌드 결과에서 `.pdb`를 함께 두면 로그의 스택 트레이스에 줄 번호가 나옵니다. 게시할 때는 빼도 됩니다.


## 게임 업데이트와 호환

게임이 업데이트되면 `Assembly-CSharp.dll`이 바뀝니다. 공식 이벤트만 쓴 모드는 대체로 그대로 돌지만, 내부 클래스를 직접 건드리거나 Harmony로 패치한 모드는 깨질 수 있습니다.

- 업데이트 뒤에는 새 `Managed/` 폴더를 참조해 다시 빌드하고 테스트하세요.
- 창작마당 페이지의 설명에 **지원하는 게임 버전**을 적어 두면 사용자가 원인을 빨리 압니다. 게시할 때 게임 버전이 창작마당 항목 메타데이터에 자동으로 기록되기도 합니다.
- 코드를 바꾸면 검수를 다시 받아야 게시할 수 있습니다. [게시와 멀티플레이](publishing.md) 참고.


## 멀티플레이에서 주의할 것

- 코드 모드는 거의 항상 `multiplayer.installation`이 `everyone`입니다. 방 참가자 전원이 같은 모드를 켜야 합니다.
- 네트워크로 스폰되는 오브젝트(유닛, 발사체 등)는 `RegisterNetworkPrefab`으로 등록해야 참가자 쪽에서도 생성됩니다. `stableKey`를 주면 모드 버전이 바뀌어도 같은 ID를 유지합니다.
- 호스트와 참가자의 모드 **로드 순서**까지 같아야 합니다. 게임이 자동으로 맞추지만, 순서에 따라 결과가 달라지는 코드는 쓰지 않는 것이 좋습니다.
- `WorkshopLocal/`의 로컬 모드가 로드된 실행에서는 방을 만들 수도, 모드 방에 들어갈 수도 없습니다. 다른 사람이 같은 파일을 받을 방법이 없기 때문입니다. 멀티플레이 테스트는 창작마당에 올린 뒤(새 항목은 Unlisted로 만들어져 링크를 아는 사람만 구독할 수 있습니다) 로컬 폴더를 치우고 구독본으로 하세요.


## 샘플

[`template/`](../../template)은 타이틀 메뉴에 버튼 하나를 추가하고 로그를 남기는 최소 모드입니다. 복사해서 `AssemblyName`, `modId`, `entryClass`, 출력 경로만 바꾸면 자기 모드가 됩니다.
