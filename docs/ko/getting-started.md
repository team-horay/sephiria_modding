# 첫 모드 만들기

세피리아 모드의 기초 구조를 설명하고, 첫 모드를 만들어 게임에서 확인하는 것까지 따라가 봅니다.



## 준비물

- 고급 텍스트 편집기 (VS Code 등)



## 게임 설치 폴더 찾기

- 세피리아 설치 폴더에 접근합니다. (`Steam 라이브러리 → 세피리아 우클릭 → 관리 → 로컬 파일 보기`)
- 맥OS에서는 같은 메뉴로 열리는 `Sephiria.app`이 있는 폴더가 게임 설치 폴더입니다.



## WorkshopLocal 폴더 생성 & 나만의 모드 폴더 만들기

게임 설치 폴더에 `WorkshopLocal/` 폴더를 생성하세요.
그리고 그 아래에 나만의 모드 폴더를 추가로 생성하세요. (세피리아에서는 폴더 하나를 모드 하나로 간주합니다)
이 경로에 생성한 모드는 게임 내에서 테스트가 가능하며, 창작마당으로 업로드도 가능합니다.

이 문서에서는 타이틀 화면의 "게임 시작(Start Game)" 버튼 문구를 "Hello World!"로 바꾸는 모드 `my-first-mod`를 만들어 봅니다. 완성 구조는 아래와 같습니다.

```
<게임 설치 폴더>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← 모드 하나
    │   ├── metadata.json      ← 모드를 정의하는 핵심 파일
    │   ├── thumbnail.png      ← 모드 미리보기 썸네일 (창작마당 업로드 때 필요)
    │   └── Localization/      ← 번역 모듈이 읽는 폴더
    │       └── en-US.json     ← 바꿀 텍스트
    └── another-mod/           ← 또 다른 모드
        └── ...
```



## metadata.json 파일

- 모드 폴더 안에 `metadata.json`을 만들고 아래 내용을 붙여넣으세요. 
- `metadata.json` 파일은 모드를 정의하는 중요한 파일입니다. 
- `yourname` 자리에 본인 닉네임을 넣으세요.
- 파일은 **UTF-8**로 저장하세요. 메모장이라면 "다른 이름으로 저장"에서 인코딩을 UTF-8로 고릅니다.
- 자세한 의미는 [metadata.json 레퍼런스](metadata.md)를 확인하세요.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only", "support": "supported" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## 언어 파일 만들기

모드 폴더 안에 `Localization/` 폴더를 만들고, 그 안에 `en-US.json` 파일을 아래 내용으로 만드세요.
(파일 이름을 본인의 언어 코드로 변경하여 테스트해도 됩니다.)

```json
{
  "Title_GameStart": "Hello World!"
}
```



## 확인하기

여기까지 만들었다면 모드가 동작하는지 확인해 볼 수 있습니다.

1. 세피리아를 실행합니다.
2. 게임 언어를 영어로 두고 타이틀 화면을 보면 "게임 시작(Start Game)" 버튼이 "Hello World!"로 바뀌어 있습니다. `WorkshopLocal/`에 둔 모드는 기본으로 켜져 있어서 따로 켤 필요가 없습니다.
3. 타이틀 화면의 **스팀 창작마당** 버튼을 누르면 창작마당 패널이 열립니다. "내가 만든 모드" 구역에 "My First Mod"가 보여야 합니다.



### 모드가 적용되지 않아요!

- JSON 문법이 맞게 들어갔는지 확인하세요.
- `metadata.json`의 `path`와 실제 폴더 이름은 대소문자까지 일치해야 합니다.
- `metadata.json` 파일이 UTF-8로 인코딩되어 있는지 확인하세요.
- 게임 언어가 파일 이름의 언어와 같은지 확인하세요.



## 다음 단계

1. [metadata.json 레퍼런스](metadata.md)에서 모드가 어떻게 정의되는지 알아보세요.
2. [번역 모드](localization-mods.md)와 [코스튬 모드](costume-mods.md)는 상대적으로 제작하기 쉬우니 먼저 확인해보세요.
3. [DLL 모드](dll-mods.md)는 코드에 대한 기초적인 지식이 있는 분들께 권합니다.
4. [게시와 멀티플레이](publishing.md)를 통해 창작마당에 배포하세요.
