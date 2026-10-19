# 번역 모드

게임에서는 각 언어마다 **키 → 문자열** 테이블을 하나씩 가지고 있습니다. 
게임은 모드의 테이블을 원본 위에 키 단위로 덮어씁니다.
두 모드가 동일한 키를 덮어쓴다면 가장 나중에 로드되는 모드의 내용을 담습니다.


## 1. 폴더 만들기

게임 설치 폴더 내 `WorkshopLocal/` 폴더를 통해 모드를 테스트하고 창작마당에 업로드할 수 있습니다. (폴더가 없으면 직접 생성하시기 바랍니다.)
`WorkshopLocal/` 경로 아래에 자신만의 폴더를 만듭니다. 폴더 이름은 자유롭게 설정 가능하지만 가급적 본인의 모드를 직관적으로 표현하세요. 


```
<게임 설치 폴더>/
└── WorkshopLocal/
    └── my-first-mod/
        ├── metadata.json
        ├── thumbnail.png (1MB 미만)
        └── Localization/
            └── ko-KR.json
```


## 2. metadata.json 쓰기

`metadata.json`은 게임에서 모드를 정의하는 핵심 파일입니다. 파일 구조는 다음과 같습니다.

```json
{
  "info": {
    "title": { "ko-KR": "내 첫 번역 모드", "en-US": "My First Translation Mod" },
    "description": { "ko-KR": "타이틀 문구를 바꿔 봅니다.", "en-US": "Changes the title screen text." }
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.first-mod",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only", "support": "supported" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

- `info`: 모드의 기본 설명입니다. 여러 언어 내용을 담아둘 수 있습니다.
- `modId`: 모드를 구분하는 고유 아이디입니다. 모든 창작마당 모드의 modId는 고유해야 합니다. 본인의 이름이나 닉네임을 앞에 붙이면 다른 사람들과 겹치지 않습니다. (소문자 영문·숫자·점·하이픈·밑줄만 쓸 수 있음)
- `path`에는 언어 파일이 든 **폴더 이름**을 기입합니다.
- 번역만 바꾸는 모드라면 `installation`은 `local_only`로 설정하세요. (다른 참가자가 설치할 필요가 없으므로)
- 파일은 **UTF-8**로 저장하세요. 메모장이라면 "다른 이름으로 저장"에서 인코딩을 UTF-8로 고릅니다.

모든 필드의 의미는 [metadata.json 레퍼런스](metadata.md)를 보세요.


## 3. 언어 파일 생성

바꾸고 싶은 문구의 키는 원본 언어 파일에서 찾을 수 있습니다. 원본 텍스트는 `<게임 설치 폴더>/Sephiria_Data/StreamingAssets/Localization/` 경로에 언어별로 있으니, 텍스트 편집기로 열어 키와 내용을 확인할 수 있습니다. **원본 파일은 수정하지 마세요.** 게임 업데이트와 Steam 파일 무결성 검사에서 되돌아갑니다. 키를 찾아 모드 파일에 옮겨 적는 방식으로만 작업합니다.

`Localization/ko-KR.json`:

```json
{
  "Title_GameStart": "모험 시작",
  "Title_Quit": "그만하기"
}
```

- 키와 값 모두 문자열입니다.
- 중첩 객체나 배열은 쓸 수 없습니다.
- 마지막 항목 뒤에 쉼표를 붙이면 에러가 나니 문법에 유의하세요.


## 4. 테스트

1. 게임을 실행합니다.
2. 타이틀 화면에서 창작마당 패널을 엽니다. 모드 목록에 "내 첫 번역 모드"가 보여야 합니다. `WorkshopLocal/`에 둔 모드는 기본으로 켜져 있습니다.
3. 타이틀의 "게임 시작"이 "모험 시작"으로 바뀌어 있을 것입니다.


## 5. 창작마당에 올리기

테스트가 끝나면 폴더 루트에 `thumbnail.png`(1MB 미만)를 넣고 타이틀 창작마당 패널에서 **검수 요청**을 보냅니다. 승인되면 같은 버튼이 **업로드**로 바뀌고, 그때 창작마당에 게시할 수 있습니다. 절차는 [게시와 멀티플레이](publishing.md)를 확인하세요.


## 추가 정보

### 번역하지 말아야 할 것

게임 텍스트에는 실행 중에 해석되는 표현들이 존재합니다. 아래의 텍스트는 번역하지 말고 영문(대소문자 구분) 그대로 넣으세요.

| 종류 | 예 | 설명 |
|---|---|---|
| 키워드 | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | 실행 중 숫자나 이름으로 바뀝니다. 중괄호 안은 한 글자도 바꾸지 마세요. |
| 게임 태그 | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>` | 키워드 툴팁과 아이콘으로 바뀝니다. 태그 안의 이름은 대소문자까지 고정입니다. |
| 서식 태그 | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>` | 텍스트 서식입니다. 여는 태그와 닫는 태그를 짝 맞춰 두세요. |
| 줄바꿈 | `\n` | JSON 문자열 안에서 줄바꿈입니다. |


### 새 언어 추가

지원하지 않는 언어를 새로 추가할 수도 있습니다. 파일 이름이 새로운 언어 코드라면 게임은 그것을 새 언어로 등록하고 언어 설정 목록에 보여 줍니다.

게임이 이미 지원하는 언어는 `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`입니다. 이 목록에 없는 코드로 파일을 만들면 새 언어가 됩니다. 아래 예시의 `ko-KR` 자리에 새 언어 코드를 넣으세요.

```
Localization/
└── ko-KR.json
```

- **전체 키를 번역**하세요. 모드 파일에 없는 키는 빈 값이 아니라 **영어**(영어도 없으면 한국어)로 표시됩니다. 원본 `en-US.json`을 복사해 시작하는 것이 가장 쉽습니다.
- **`#`로 시작하는 설정 키**를 채우세요. `#LanguageName`은 언어 설정 목록에 보이는 이름, `#FontName`은 사용할 글꼴 이름입니다. `#Colon`, `#Bullet`, `#ParenthesisOpen` 같은 구두점 키도 그 언어 관습에 맞게 바꿉니다.
- `#FontName`은 게임에 내장된 글꼴 중에서 고를 수 있습니다. 게임에는 다음과 같은 문자 체계가 내장되어 있습니다.

| 문자 체계 | `#FontName` | 
|---|---|
| 라틴 문자, 키릴 문자, 한글 | `GalCyrillic` |
| 일본어 | `JF-Dot-MPlus10` |
| 중국어 간체 | `Fusion_Pixel_10_Simple` |
| 중국어 번체 | `BoutiqueBitmap9x9` |
| 태국 문자 | `NotoSansThai` |


### 문제 해결

| 증상 | 확인할 것 |
|---|---|
| 패널에 모드가 안 보임 | 폴더가 `<게임 설치 폴더>/WorkshopLocal/` 바로 아래에 있는지. 한 단계 더 깊으면 못 찾습니다. |
| 패널에 보이지만 "매니페스트 없음" | `metadata.json`에 `mod` 객체가 없음 |
| 패널에 오류가 표시됨 | 오류 문구가 원인입니다. `path` 대소문자, 쉼표 빠짐, 따옴표 짝 같은 JSON 문법을 확인하세요. [metadata.json 레퍼런스](metadata.md#자주-하는-실수)에 흔한 사례가 있습니다. |
| 모드는 켜졌는데 텍스트가 안 바뀜 | 키 이름이 원본과 글자 하나까지 같은지. 파일 이름이 현재 게임 언어와 같은지(`ko-KR.json`은 한국어에서만 보입니다) |
| 일부 키만 안 바뀜 | 다른 번역 모드가 같은 키를 뒤에서 덮어쓰고 있는지. 패널에서 순서를 바꿔 보세요. |
| 텍스트에 `{VALUE}`가 그대로 보임 | 키워드 철자가 원문과 다름. 원문에서 그대로 복사하세요. |
| 텍스트에 `<tag=…>`가 그대로 보임 | 태그 이름 철자나 대소문자가 다름 |
| 한글이 깨져 보임 | 파일이 UTF-8이 아님. UTF-8로 다시 저장하세요. |
| 폰트가 네모로 보임 | 게임에 내장되어 있지 않은 문자입니다. |
| 파일 하나가 통째로 무시됨 | 그 파일의 JSON 문법 오류. 패널 상세 또는 `Player.log`에 파일 이름과 함께 오류가 남습니다. |

로그 파일은 `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`에 있습니다. "모드 내 언어 파일 발견"으로 검색하면 어떤 파일이 몇 개의 키로 읽혔는지 보입니다.


### 예제

[`examples/translation-sample`](../../examples/translation-sample)에 그대로 `WorkshopLocal/`에 복사해 볼 수 있는 완성 예제가 있습니다.
