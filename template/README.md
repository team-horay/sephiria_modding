# DLL 모드 템플릿

타이틀 메뉴에 버튼 하나를 추가하는 최소 모드입니다. 복사해서 이름만 바꾸면 자기 모드가 됩니다.

## 사용법

1. 이 폴더를 통째로 복사합니다.
2. `MyFirstMod.csproj`에서 `GameDir`를 게임 설치 폴더로, `AssemblyName`·`RootNamespace`·`ModFolderName`을 자기 모드 이름으로 바꿉니다.
3. `Entry.cs`의 네임스페이스를 `RootNamespace`와 같게 바꿉니다.
4. `package/metadata.json`의 `modId`, `path`(`bin/<AssemblyName>.dll`), `entryClass`(`<네임스페이스>.Entry`)를 맞춥니다.
5. `package/thumbnail.png`를 자기 모드의 미리보기 이미지로 바꿉니다(1MB 미만).
6. 빌드합니다.

```bash
dotnet build -c Release
```

빌드하면 `<게임 설치 폴더>/WorkshopLocal/<ModFolderName>/`에 `bin/`과 `package/`의 내용이 들어갑니다. 게임을 실행해 타이틀 창작마당 패널에서 모드가 보이는지 확인하고, 게임을 시작한 뒤 타이틀로 돌아오면 왼쪽 아래에 버튼이 생깁니다.

자세한 설명은 [DLL 모드 가이드](../docs/ko/dll-mods.md)를 보세요.
