# DLL 모드 템플릿: 물리 피해 +5

플레이어가 스폰될 때 서버에서 물리 피해 커스텀 스탯을 5 올리는 최소 모드입니다. 복사해서 이름만 바꾸면 자기 모드가 됩니다. [DLL 모드 가이드](../docs/ko/dll-mods.md)가 이 폴더를 기준으로 설명합니다.

## 바로 써 보기 (빌드 없이)

`package/` 폴더에 빌드된 DLL이 들어 있습니다. `package/` 폴더를 통째로 `<게임 설치 폴더>/WorkshopLocal/`에 복사하고 게임을 실행하면 바로 동작합니다. 게임이 업데이트되면 이 DLL이 맞지 않을 수 있으니, 그때는 아래 절차로 다시 빌드하세요.

## 내 모드로 만들기

1. 이 폴더를 통째로 복사합니다.
2. `PhysicalDamagePlus.csproj`에서 `GameDir`를 게임 설치 폴더로, `AssemblyName`·`RootNamespace`·`ModFolderName`을 자기 모드 이름으로 바꿉니다.
3. `Entry.cs`의 네임스페이스를 `RootNamespace`와 같게 바꿉니다.
4. `package/metadata.json`의 `modId`(`yourname` 자리에 닉네임), `path`(`bin/<AssemblyName>.dll`), `entryClass`(`<네임스페이스>.Entry`)를 맞춥니다.
5. `package/thumbnail.png`를 자기 모드의 미리보기 이미지로 바꿉니다(1MB 미만).
6. 빌드합니다.

```bash
dotnet build -c Release
```

빌드하면 DLL이 `package/bin/`에 들어가고, `package/`의 내용이 `<게임 설치 폴더>/WorkshopLocal/<ModFolderName>/`로 복사됩니다. 게임을 실행해 타이틀 창작마당 패널에서 모드가 보이는지 확인하고, 게임을 시작해 스탯 창을 열면 물리 피해가 +5 되어 있습니다.

## 구성

```
template/
├── PhysicalDamagePlus.csproj   ← 프로젝트. GameDir·이름만 바꿔 씀
├── Entry.cs                    ← 진입점. 스폰 이벤트 구독과 스탯 추가
└── package/                    ← 이 폴더가 모드 그 자체. WorkshopLocal에 복사하는 단위
    ├── metadata.json           ← 모드 정의
    ├── thumbnail.png           ← 미리보기
    └── bin/
        └── PhysicalDamagePlus.dll ← 빌드 결과
```
