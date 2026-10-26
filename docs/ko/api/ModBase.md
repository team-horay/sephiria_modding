<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 진입점과 컨텍스트

모드 진입 클래스가 상속하는 베이스 클래스와, 로더가 붙여 주는 컨텍스트·매니페스트 타입입니다. 새 모드는 `WorkshopModBase`를 상속하세요.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | 창작마당 모드의 진입 베이스. |
| [`WorkshopModContext`](#workshopmodcontext) | 창작마당 DLL 모듈에 주어지는 읽기 전용 컨텍스트. |
| [`HorayModBase`](#horaymodbase) | DLL 모드 진입 클래스의 베이스입니다. |
| [`WorkshopDataPackage`](#workshopdatapackage) | consumer 모드가 읽을 data 모듈 패키지 하나입니다. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | 검증을 통과한 실행 명세. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | 매니페스트 `modules[]` 항목 하나입니다. |
| [`WorkshopDependency`](#workshopdependency) | 매니페스트 `dependencies[]` 항목 하나입니다. |
| [`WorkshopModuleType`](#workshopmoduletype) | 매니페스트 `modules[].type` 값입니다. |
| [`WorkshopInstallation`](#workshopinstallation) | 다른 참여자에게 설치를 요구하는지에 대한 제작자 선언입니다(매니페스트 `multiplayer.installation`). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

창작마당 모드의 진입 베이스.

### 프로퍼티

#### Context

```csharp
public WorkshopModContext Context { get; }
```

OnModLoaded 호출 시점부터 유효

### 메서드

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

이 모드를 consumer로 지정한 data 모듈 패키지(로드 순서). 활성화가 끝난 뒤(데이터베이스 이벤트 이후)에 호출합니다. 컨텍스트가 없으면(AddOns 경로 등) 빈 목록.


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

창작마당 DLL 모듈에 주어지는 읽기 전용 컨텍스트. 설치 경로가 유동적이므로, 모드는 이 값으로 자기 파일을 찾습니다.

### 프로퍼티

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

패키지 루트 절대 경로(설치 폴더)


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Workshop 파일 ID입니다. 로컬 패키지는 경로에서 만든 고유 ID를 받습니다.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

검증된 실행 명세


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

이 인스턴스를 만든 dll 모듈 항목


#### Options

```csharp
public ModOptions Options { get; }
```

이 modId의 기기별 옵션. 같은 패키지의 DLL 모듈과 로컬/구독 사본이 공유합니다.

### 생성자

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*설명이 아직 없습니다.*

### 메서드

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

패키지 루트 기준 상대 경로를 절대 경로로. 루트 이탈·절대 경로는 거부(예외).


## HorayModBase

```csharp
public class HorayModBase
```

DLL 모드 진입 클래스의 베이스입니다.

### 상수와 필드

#### metadata

```csharp
public Metadata metadata
```

`LoadMod`에서 받은 메타데이터입니다.

### 메서드

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

로더가 호출합니다. 메타데이터를 저장한 뒤 `OnModLoaded`를 부릅니다.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

모드 초기화 지점입니다. 데이터베이스 초기화 전에 호출되므로 `HorayModAPI` 이벤트 구독은 여기서 합니다.


#### UnloadMod

```csharp
public void UnloadMod()
```

`OnModUnloaded`를 부릅니다.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

모드 해제 지점입니다. 이벤트 구독 해제 같은 정리를 둡니다.

### 중첩 타입

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

로더가 `LoadMod`에 넘기는 모드 식별 정보입니다.

##### 상수와 필드

###### modName

```csharp
public string modName
```

표시 이름


###### modVersion

```csharp
public string modVersion
```

버전


###### modAuthor

```csharp
public string modAuthor
```

제작자


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

consumer 모드가 읽을 data 모듈 패키지 하나입니다. `WorkshopModBase.GetDataPackages()`로 얻습니다.

### 프로퍼티

#### ModId

```csharp
public string ModId { get; }
```

패키지의 modId


#### Version

```csharp
public string Version { get; }
```

패키지의 version


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Workshop 파일 ID


#### DisplayName

```csharp
public string DisplayName { get; }
```

표시 이름(info.title, 없으면 modId)


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

패키지 루트 절대 경로


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

이 consumer를 가리키는 data 모듈(선언 순서)


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

모듈 폴더 절대 경로(선언 순서). Modules[i].FullPath와 같습니다.

### 메서드

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

활성화 기록(로드 순서)에서 `consumerModId`용 data 모듈을 가진 패키지를 고릅니다. 의존 충족 상태(Active·AlreadyActive·NoDllModules)만 포함합니다. 실패·부분 적용 패키지의 데이터는 넘기지 않습니다.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

검증을 통과한 실행 명세. 불변.

### 프로퍼티

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

패키지 루트 절대 경로


#### ModId

```csharp
public string ModId { get; }
```

매니페스트 modId


#### Version

```csharp
public string Version { get; }
```

매니페스트 version


#### Author

```csharp
public string Author { get; }
```

매니페스트 author. 없으면 null.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, 없으면 modId


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

멀티플레이 설치 요구


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

multiplayer 객체가 명시돼 있었는지. false면 Installation은 정규화 기본값.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

모듈 목록(선언 순서)


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

보조 관리형 DLL 절대 경로. 시작 콜백 대상이 아니다.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

의존 모드 목록


#### HasDll

```csharp
public bool HasDll { get; }
```

modules에 dll 타입이 있는지로 파생. 별도 플래그를 두지 않습니다.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules에 localization 타입이 있는지


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules에 costume 타입이 있는지


#### HasData

```csharp
public bool HasData { get; }
```

modules에 data 타입이 있는지

### 생성자

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*설명이 아직 없습니다.*

### 메서드

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

`consumerModId`가 받을 data 모듈(선언 순서). 없으면 빈 목록. consumer를 명시한 모듈은 그 모드에게만, 생략한 모듈은 이 패키지 자신과 직접 의존 모드 전부에게 갑니다.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

매니페스트 `modules[]` 항목 하나입니다.

### 프로퍼티

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

모듈 종류


#### RelativePath

```csharp
public string RelativePath { get; }
```

패키지 루트 기준 상대 경로(구분자 '/'로 정규화)


#### FullPath

```csharp
public string FullPath { get; }
```

검증된 절대 경로


#### EntryClass

```csharp
public string EntryClass { get; }
```

dll 모듈의 진입 클래스 전체 이름. localization이면 null.


#### Consumer

```csharp
public string Consumer { get; }
```

data 모듈을 읽는 모드의 modId. 생략(null)이면 패키지 자신 + 직접 의존 모드 전부. data 외에는 항상 null.

### 생성자

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

검증기가 만듭니다.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

검증기가 만듭니다.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

매니페스트 `dependencies[]` 항목 하나입니다.

### 프로퍼티

#### ModId

```csharp
public string ModId { get; }
```

의존하는 모드의 modId


#### Version

```csharp
public string Version { get; }
```

null이면 버전 무관. 있으면 정확 일치만 요구(v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

다운로드 출처 힌트. 설치 후 실제 modId/version과 대조해야 하며 사칭 대체 근거가 아니다.

### 생성자

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

검증기가 만듭니다.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

매니페스트 `modules[].type` 값입니다.

| 값 | 설명 |
|---|---|
| `Dll` | 관리형 DLL 모듈 |
| `Localization` | 번역 폴더 모듈 |
| `Costume` | 코드 없는 코스튬 패키지 폴더(costume.json/skin.json + PNG) |
| `Data` | 다른 모드(consumer)가 읽는 범용 데이터 폴더(게임은 내용을 해석하지 않음) |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

다른 참여자에게 설치를 요구하는지에 대한 제작자 선언입니다(매니페스트 `multiplayer.installation`).

| 값 | 설명 |
|---|---|
| `Unknown` | 선언 없음(로비 구성 비교에는 Everyone처럼 포함) |
| `LocalOnly` | 자기 기기에서만 동작하며 로비 구성 비교에서 제외 |
| `Everyone` | 방 참여자 전원이 같은 패키지를 설치해야 함 |
| `HostOnly` | 호스트만 설치하면 됨 |
