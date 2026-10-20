# 첫 모드 만들기

세피리아 모드의 기초 구조를 설명하고, 모드를 만드는 방법을 간략히 설명합니다.



## 준비물

- 고급 텍스트 편집기 (VS Code 등)



## 게임 설치 폴더 찾기

1. 세피리아 설치 폴더에 접근합니다. (`Steam 라이브러리 → 세피리아 우클릭 → 관리 → 로컬 파일 보기`)
2. 맥OS에서는 같은 메뉴로 열리는 `Sephiria.app`이 있는 폴더가 게임 설치 폴더입니다. (.app 파일을 담고 있는 폴더)



## WorkshopLocal 폴더 생성 & 나만의 모드 폴더 만들기

게임 설치 폴더에 `WorkshopLocal/` 폴더를 생성하세요.
그리고 그 아래에 나만의 모드 폴더를 추가로 생성하세요. (세피리아에서는 폴더 하나를 모드 하나로 간주합니다)
이 경로에 생성한 모드는 게임 내에서 테스트가 가능하며, 창작마당으로 업로드도 가능합니다.

이 예제에서는 my-first-mod로 설명합니다.

```
<게임 설치 폴더>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← 모드 하나
    │   ├── metadata.json      
    │   └── ...
    └── another-mod/           ← 또 다른 모드
        └── ...
```



## metadata.json 파일


```
<게임 설치 폴더>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/
    │   ├── metadata.json      ← 모드를 정의하는 핵심 파일!
    │   └── thumbnail.png      ← 모드 미리보기 썸네일
```

폴더를 생성한 이후에는, 모드를 정의하는 핵심 파일인 `metadata.json`을 위 경로에 맞게 생성하고 아래 내용을 붙여넣으세요.
해당 파일에 대한 자세한 내용은 [metadata.json 페이지](docs/ko/metadata.md)를 확인하세요.


```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.super-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
    ]
  }
}
```



## 모듈(modules)

우리는 위 파일에서 'modules' 항목에 주목해야 합니다. 
modules에는 이 모드로 무엇을 바꾸는지 정의합니다.

현재 아래와 같은 모듈이 사용 가능합니다.

| 모듈 | 하는 일 | 관련 예제 |
|---|---|---|
| `localization` | 게임 텍스트를 해당 언어로 변경하는 모듈입니다. 언어별 파일을 폴더에 넣으면 게임 텍스트에 덮어씌워집니다. | [번역모드]() |
| `dll` | C#으로 작성한 코드를 게임에 붙입니다. 공개된 이벤트(`HorayModAPI`)를 구독해 여러 상황에 개입할 수 있습니다. | [dll모드]() |
| `costume` | 간단한 코스튬을 JSON과 스프라이트로 추가합니다. | [코스튬 모드]() |
| `data` | 다른 모드가 읽어 쓸 수 있는 데이터 묶음입니다. | 없음 |

한 모드에 여러 모듈을 함께 넣을 수도 있습니다. 
예를 들어 dll 모드가 자신의 모드와 관련된 텍스트 데이터를 `localization` 모듈로 같이 들고 다닐 수 있습니다.

모듈은 아래와 같이 정의됩니다.

```json
{
  "info": {
    "title": "Super Artifact Pack",
    "description": "Good.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "2.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```



## 창작마당 패널

metadata.json까지 만들었다면 실제 모드가 인식되는지 확인해볼 수 있습니다.
세피리아를 실행하고 타이틀 화면의 **스팀 창작마당** 버튼을 클릭하세요.



## 다음 단계

1. [metadata.json 레퍼런스](metadata.md) 파일을 통해 모드를 정의하세요.
2. [번역 모드](localization-mods.md)와 [코스튬 모드](localization-mods.md)는 상대적으로 제작하기 쉬우니 먼저 확인해보세요.
3. [dll모드]는 코드에 대한 기초적인 지식이 있는 분들께 권합니다.
4. [게시와 멀티플레이](publishing.md)를 통해 창작마당에 배포하세요.
